using System.Collections;
using System.Collections.Generic;
using MagicLeap.TTSUtil;
using UnityEngine;

public class TextToSpeechSample : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RunTextToSpeechTests());
    }

    private IEnumerator RunTextToSpeechTests()
    {
        // Initialize the Text to Speech API
        TextToSpeechUtility.Initialize();
        yield return new WaitForSeconds(1f);  // Wait a bit after initialization

        // Test 1: Simple greeting and introduction
        TextToSpeechUtility.Speak("Hello, and welcome to the text to speech test.");
        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }

        // Test 2: Queue multiple phrases
        TextToSpeechUtility.Speak("You can queue up multiple phrases at once, like counting to 3.", TTSQueueMode.QUEUE_MODE_FLUSH);
        TextToSpeechUtility.Speak("1 Mississippi.", TTSQueueMode.QUEUE_MODE_ADD);
        TextToSpeechUtility.Speak("2 Mississippi.", TTSQueueMode.QUEUE_MODE_ADD);
        TextToSpeechUtility.Speak("3 Mississippi.", TTSQueueMode.QUEUE_MODE_ADD);
        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }

        // Test 3: Start text and interrupt it midway
        TextToSpeechUtility.Speak("Now, let's interrupt a sentence by not using a queue.", TTSQueueMode.QUEUE_MODE_FLUSH);
        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }

        TextToSpeechUtility.Speak("“Alice was beginning to get very tired of sitting by her sister on the bank, and of having nothing to do.", TTSQueueMode.QUEUE_MODE_FLUSH);
        yield return new WaitForSeconds(2);

        TextToSpeechUtility.Speak("Okay, now for the next test.", TTSQueueMode.QUEUE_MODE_FLUSH);

        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }

        // Test 4: Speak in a different language
        TextToSpeechUtility.Speak("I can also speak multiple languages, such as Spanish.", TTSQueueMode.QUEUE_MODE_FLUSH);
        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }
        TextToSpeechUtility.SetLanguage(Languages.SupportedLanguage.Spanish);
        TextToSpeechUtility.Speak("Hola, esto es una prueba de voz en Español.");
        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }

        TextToSpeechUtility.SetLanguage(Languages.SupportedLanguage.English);

        // Test 5: Conclusion
        TextToSpeechUtility.Speak("And that's the end of our test. Goodbye.");
        while (TextToSpeechUtility.IsSpeaking())
        {
            yield return null;
        }

        // Dispose of Text to Speech API resources
        TextToSpeechUtility.DisposeTts();
    }


}