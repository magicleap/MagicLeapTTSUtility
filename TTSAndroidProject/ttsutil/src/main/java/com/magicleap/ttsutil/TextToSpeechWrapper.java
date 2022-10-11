package com.magicleap.ttsutil;

import android.app.Activity;
import android.content.Context;
import android.os.Bundle;
import android.speech.tts.TextToSpeech;
import android.speech.tts.UtteranceProgressListener;
import android.speech.tts.Voice;
import android.util.Log;

import org.w3c.dom.Text;

import java.util.Locale;
import java.util.Objects;

/**
 * Simple wrapper of the Android TextToSpeech core API
 * functions supported on the Magic Leap platform.
 */
public final class TextToSpeechWrapper implements AutoCloseable {
    private static final String LOG_TAG = TextToSpeechWrapper.class.getName();

    private final Activity mActivity;
    private TextToSpeech mTtsEngine;
    private boolean mInitialized = false;
    private boolean isCurrentlySpeaking = false;
    // True when a speak request was made but not started
    private boolean isWaiting = false;

    public TextToSpeechWrapper(final Activity activity) {
        mActivity = Objects.requireNonNull(activity);
    }

    @Override
    public void close() throws Exception {
        Log.v(LOG_TAG, "TextToSpeechWrapper.close()");
        if (mTtsEngine != null) {
            destroyEngine();
        }
    }

    /**
     * Initializes the TTS engine. Must be called before any TTS operations.
     */
    public void createEngine() {
        Log.v(LOG_TAG, "TextToSpeechWrapper.createEngine()");
        if (mTtsEngine != null) {
            Log.e(LOG_TAG, "TTS engine is already created.");
            return;
        }

        mTtsEngine = new TextToSpeech(mActivity.getApplicationContext(), this::onEngineInitialized);
    }

    /**
     * Shuts down the TTS engine and releases resources.
     */
    public void destroyEngine() {
        Log.v(LOG_TAG, "TextToSpeechWrapper.destroyEngine()");
        if (mTtsEngine == null) {
            Log.e(LOG_TAG, "No TTS engine to destroy.");
            return;
        }

        mTtsEngine.stop();
        mTtsEngine.shutdown();
        mTtsEngine = null;
        mInitialized = false;
        isWaiting = false;
    }

    public boolean isEngineReady() {
        return mTtsEngine != null && mInitialized;
    }

    /**
     * Sets the language of the TTS engine.
     *
     * @param locale the locale to be set.
     * @return true if the language is supported and set, false otherwise.
     */
    public synchronized boolean setLocale(final Locale locale) {
        Log.v(LOG_TAG, "Setting locale to " + locale);
        if (!isEngineReady()) {
            Log.e(LOG_TAG, "TTS engine is not ready.");
            return false;
        }

        Voice currentVoice = mTtsEngine.getVoice();
        if (currentVoice != null && locale.equals(currentVoice.getLocale())) {
            Log.d(LOG_TAG, "Locale is already set to " + locale);
            return true;
        }

        int result = mTtsEngine.setLanguage(locale);
        if (result == TextToSpeech.LANG_NOT_SUPPORTED || result == TextToSpeech.LANG_MISSING_DATA) {
            Log.e(LOG_TAG, "Locale not supported: " + locale);
            return false;
        }

        Log.v(LOG_TAG, "Locale set successfully.");
        return true;
    }

    public boolean isSpeaking() {
        return isEngineReady() && (isCurrentlySpeaking || isWaiting);
    }

    /**
     * Stops all current speaking activities.
     */
    public synchronized boolean stopSpeaking() {
        Log.v(LOG_TAG, "TextToSpeechWrapper.stopSpeaking()");
        if (!isEngineReady()) {
            Log.e(LOG_TAG, "TTS engine is not ready.");
            return false;
        }

        int result = mTtsEngine.stop();
        if (result == TextToSpeech.ERROR) {
            Log.e(LOG_TAG, "Failed to stop speaking.");
            return false;
        }
        isWaiting = false;
        return true;
    }

    /**
     * Speaks the given text using specified queue mode.
     *
     * @param text the text to be spoken.
     * @param queueMode the mode to queue the text in TTS engine.
     */
    public synchronized void speak(final String text, int queueMode) {
        Log.v(LOG_TAG, "Speaking text: " + text);
        if (text == null || text.isEmpty()) {
            Log.e(LOG_TAG, "Text is empty or null.");
            return;
        }

        if (!isEngineReady()) {
            Log.e(LOG_TAG, "TTS engine is not ready.");
            return;
        }
        // You can generate unique IDs per call
        String utteranceId = "UniqueID";
        mTtsEngine.speak(text, queueMode, null, utteranceId);
        isWaiting = true;

    }

    private void onEngineInitialized(final int status) {
        Log.d(LOG_TAG, "TTS engine initialization status: " + status);
        if (status == TextToSpeech.ERROR) {
            Log.e(LOG_TAG, "Failed to initialize TTS engine.");
            destroyEngine();
        } else {
            mInitialized = true;
            mTtsEngine.setOnUtteranceProgressListener(new UtteranceProgressListener() {
                @Override
                public void onStart(String s) {
                    isCurrentlySpeaking = true;
                    isWaiting = false;
                }

                @Override
                public void onDone(String s) {
                    isCurrentlySpeaking = false;
                    isWaiting = false;
                }

                @Override
                public void onError(String s) {
                    isCurrentlySpeaking = false;
                    isWaiting = false;
                }
            });
        }
    }
}
