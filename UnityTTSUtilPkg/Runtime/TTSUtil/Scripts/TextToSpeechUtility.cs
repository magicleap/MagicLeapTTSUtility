using System;
using UnityEngine;

namespace MagicLeap.TTSUtil
{
    using static Languages;

    public enum TTSQueueMode
    {
        /// <summary>
        /// In `speak()`, indicates the queue of text to speak
        /// should be flushed prior to speaking the given text.
        /// </summary>
        QUEUE_MODE_FLUSH = 0,
        /// <summary>
        /// In `speak()`, indicates the text to speak should be
        /// added to the queue.  It therefore will not play till
        /// the text before it has played.
        /// </summary>
        QUEUE_MODE_ADD = 1
    }

    ///<summary>
    // Exposes the TextToSpeechWrapper.java class for Unity.
    // </summary>
    public static class TextToSpeechUtility
    {
         // Tracks if the API has been initialized to prevent reinitialization.
        private static bool isInitialized = false;

        // Java class path for the Text-to-Speech wrapper.
        private const string JavaTtsWrapperClass = "com.magicleap.ttsutil.TextToSpeechWrapper";

        // Timeout for the TTS engine to become ready after initialization.
        private const double TtsEngineReadyTimeoutInS = 5.0;

        // Reference to the Java TTS wrapper object.
        private static AndroidJavaObject _javaTtsWrapper;

        /// <summary>
        /// Indicates whether the TTS API has been disposed of.
        /// </summary>
        public static bool Disposed { get; private set; } = false;

        /// <summary>
        /// Initializes the TTS API if it has not already been initialized.
        /// </summary>
        public static void Initialize()
        {
            if (isInitialized) return;

            if (!IsAndroidPlatform())
            {
                Debug.LogWarning("TextToSpeechUtility can only be initialized on Android platforms.");
                return;
            }

            var currentActivity = GetCurrentActivity();
            _javaTtsWrapper = PrepareJavaTtsWrapper(currentActivity);
            if (_javaTtsWrapper == null)
            {
                Debug.LogError("Failed to create and initialize the Java TTS wrapper.");
                return;
            }

            Disposed = false;
            isInitialized = true;
        }

        /// <summary>
        /// Disposes of the TTS resources and marks the API as disposed.
        /// </summary>
        public static void DisposeTts()
        {
            if (!isInitialized || Disposed)
            {
                Debug.LogWarning("TextToSpeechUtility is either not initialized, already disposed, or in an invalid state.");
                return;
            }

            _javaTtsWrapper.Call("destroyEngine");
            _javaTtsWrapper.Dispose();
            isInitialized = false;
            Disposed = true;
        }

        /// <summary>
        /// Sets the language for speech synthesis.
        /// </summary>
        /// <param name="language">The language to set.</param>
        /// <returns>True if the language was successfully set, otherwise false.</returns>
        public static bool SetLanguage(SupportedLanguage language)
        {
            if (!isInitialized || Disposed)
            {
                Debug.LogError("TextToSpeechUtility must be initialized and not disposed to set language.");
                return false;
            }

            string localeName = ToAbbreviatedName(language);
            AndroidJavaObject locale = new("java.util.Locale", localeName);

            return _javaTtsWrapper.Call<bool>("setLocale", locale);
        }

        /// <summary>
        /// Checks if the TTS is currently speaking.
        /// </summary>
        /// <returns>True if speaking, otherwise false.</returns>
        public static bool IsSpeaking()
        {
            return IsOperational() && _javaTtsWrapper.Call<bool>("isSpeaking");
        }

        /// <summary>
        /// Stops any ongoing speech.
        /// </summary>
        /// <returns>True if successfully stopped, otherwise false.</returns>
        public static bool StopSpeaking()
        {
            return IsOperational() && _javaTtsWrapper.Call<bool>("stopSpeaking");
        }

        /// <summary>
        /// Speaks the provided text using the specified queue mode.
        /// </summary>
        /// <param name="text">Text to speak.</param>
        /// <param name="queueMode">The queue mode for speech.</param>
        public static void Speak(String text, TTSQueueMode queueMode = TTSQueueMode.QUEUE_MODE_FLUSH)
        {
            if (!IsOperational()) return;

            _javaTtsWrapper.Call("speak", text, (int)queueMode);
        }

        /// <summary>
        /// Creates and initializes the Java TTS wrapper object.
        /// </summary>
        /// <param name="currentActivity">The current Android activity.</param>
        /// <returns>The initialized Java TTS wrapper object.</returns>
        private static AndroidJavaObject PrepareJavaTtsWrapper(AndroidJavaObject currentActivity)
        {
            AndroidJavaObject javaTtsWrapper = new(JavaTtsWrapperClass, currentActivity);
            javaTtsWrapper.Call("createEngine");
            if (!WaitTillTtsEngineReady(javaTtsWrapper))
            {
                Debug.LogError($"TTS engine initialization failed after waiting {TtsEngineReadyTimeoutInS} seconds.");
                return null;
            }

            return javaTtsWrapper;
        }

        /// <summary>
        /// Waits until the TTS engine reports that it is ready.
        /// </summary>
        /// <param name="javaTtsWrapper">The Java TTS wrapper object.</param>
        /// <returns>True if the engine is ready before the timeout, otherwise false.</returns>
        private static bool WaitTillTtsEngineReady(AndroidJavaObject javaTtsWrapper)
        {
            var startTime = DateTime.Now;
            do
            {
                if (javaTtsWrapper.Call<bool>("isEngineReady"))
                {
                    Debug.Log($"TTS engine ready after {(DateTime.Now - startTime).TotalMilliseconds} ms.");
                    return true;
                }
            } while ((DateTime.Now - startTime).TotalSeconds < TtsEngineReadyTimeoutInS);

            return false;
        }

        /// <summary>
        /// Checks if the current platform is Android.
        /// </summary>
        /// <returns>True if Android, otherwise false.</returns>
        private static bool IsAndroidPlatform()
        {
#if UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }

        /// <summary>
        /// Gets the current Android activity from Unity.
        /// </summary>
        /// <returns>The current Android activity.</returns>
        private static AndroidJavaObject GetCurrentActivity()
        {
            var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            return unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
        }

        /// <summary>
        /// Checks if the API is in a valid state for operation (initialized and not disposed).
        /// </summary>
        /// <returns>True if the API is operational, otherwise false.</returns>
        private static bool IsOperational()
        {
            if (!isInitialized || Disposed)
            {
                Debug.LogError("TextToSpeechUtility is not in a valid state (either not initialized or disposed).");
                return false;
            }
            return true;
        }
    }
}
