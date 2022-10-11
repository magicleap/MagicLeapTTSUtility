# Magic Leap 2 Text-to-Speech Utility

This project demonstrates how to use Android's Text-to-Speech functionality on the Magic Leap 2. The repository contains the Android Project used to create the plugin file, a sample Unity project and the Magic Leap TTS Utility Package.

## Repository Structure

- **TTSAndroidProject**
  - Contains the Android Studio project and Java source code to build the AAR plugin that makes the Android TTS calls.
  - Developers can modify the `ttsutil` module as needed.
  - After building, the AAR files will be output under `TTSAndroidProject/ttsutil/build/outputs/aar/`.

- **UnityTTSUtilPkg**
  - Directory for the Unity Package that includes the prebuilt plugin and the entry point for the TTS API calls (`TextToSpeechUtility`).
  - The package can be imported into existing Unity projects.
  - The sample can be imported via the Unity Package Manager.

- **UnityTTSProject**
  - Unity Project directory configured for Magic Leap 2.
  - Contains a sample scene that demonstrates the use of the Text-to-Speech Utility through a simple `TextToSpeechSample` script.

### Features

- Initialize and dispose of the TTS engine.
- Set the language of the TTS engine.
- Speak text with options to queue or flush the current speech (`QUEUE_FLUSH` and `QUEUE_ADD`). 
- Check if the TTS engine is currently speaking.
- Stop the current speech.

## Package Installation

This section includes instructions on importing the Magic Leap TTS Utility into an existing project. 

### Import the Package

1. **Import the Package using git URL**
   - Open your Unity project.
   - Navigate to `Window` > `Package Manager`.
   - Click the `+` button, select `Add package from git URL...`, and enter the URL for this package repository.

```xml
https://github.com/magicleap/MagicLeapTTSUtility.git?path=/UnityTTSUtilPkg
```

#### Importing the Sample Scene

1. **Open Package Manager**:
   - Open your Unity project.
   - Navigate to the Unity Editor menu, select `Window` > `Package Manager`.

2. **Locate the Magic Leap TTS Utility Package**:
   - In the Package Manager window, find the `Magic Leap TTS Utility` package.

3. **Import the Sample Scene**:
   - Click on the `Magic Leap TTS Utility` package in the list to view its details.
   - In the details panel, navigate to the `Samples` tab.
   - Select import next to `Sample Scene`.
   - Once the import process is complete, the sample scene files will be added to your project's `Assets/Samples/` directory.

#### Basic API Usage

```csharp
// Initialize the Text-to-Speech system
TextToSpeechUtility.Initialize();

// Check if the system is speaking
bool isSpeaking = TextToSpeechUtility.IsSpeaking();

// Speak a text
TextToSpeechUtility.Speak("Hello, world!", TextToSpeechUtility.TTSQueueMode.QUEUE_MODE_FLUSH);

// Change language (optional)
TextToSpeechUtility.SetLanguage(TextToSpeechUtility.SupportedLanguage.English_US);

// Stop speaking
TextToSpeechUtility.StopSpeaking();

// Dispose when done
TextToSpeechUtility.DisposeTts();
```

## Building the AAR Plugin

1. **Open the Android Project:**
   - Navigate to `TTSAndroidProject` and open the project in Android Studio.

2. **Build the AAR:**
   - Build the project to generate the AAR file.
   - The output will be located at `TTSAndroidProject/ttsutil/build/outputs/aar/`.

3. **Import the AAR:**
   - Rename the generated AAR file to `ttsutil.aar`
   - Replace the existing plugin located inside the package's `UnityTTSUtilPkg/Plugins/Android/` directory.

## Copyright

Copyright (c) 2023-present Magic Leap, Inc. All Rights Reserved. Use of this file is governed by the Developer Agreement, located here: https://www.magicleap.com/software-license-agreement-ml2
