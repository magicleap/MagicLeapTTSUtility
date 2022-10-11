# Magic Leap Text-to-Speech Utility Package

## Overview

The Magic Leap Text-to-Speech (TTS) Utility package provides a simple interface for integrating Android's Text-to-Speech functionality into your Magic Leap 2 Unity projects. This package includes a TTS API script (`TextToSpeechUtility`) and a Java plugin to handle TTS calls. A sample script (`TextToSpeechSample`) is also provided to demonstrate how to use the API.

## Features

- Initialize and dispose of the TTS engine.
- Set the language of the TTS engine.
- Speak text with options to queue or flush the current speech.
- Check if the TTS engine is currently speaking.
- Stop the current speech.

## Installation

### Importing via Unity Package Manager

1. Open your Unity project.
2. Open the Package Manager (Window > Package Manager).
3. Click the "+" button in the top-left corner.
4. Select "Add package from git URL..."
5. Enter the url for the package in the repository

```xml
https://github.com/magicleap/MagicLeapTTSUtility.git?path=/UnityTTSUtilPkg
```

6. Click "Add".

### Manual Import

1. Download the package.
2. In the package manage ckilc the "+" button, then select "Add package from disk..."
3. Select the `package.json` file from the `/UnityTTSUtilPkg` directory and click "Import".

## Usage

### Initializing the TTS Engine

Before using any TTS functionality, you must initialize the TTS engine:

```csharp
using MagicLeap.TTSUtil;

void Start()
{
    TextToSpeechAPI.Initialize();
}
```

### Disposing of the TTS Engine

When you no longer need the TTS functionality, make sure to dispose of the TTS engine:

```csharp
void OnDestroy()
{
    TextToSpeechAPI.DisposeTts();
}
```

### Setting the Language

You can set the language of the TTS engine using the `SetLanguage` method:

```csharp
using MagicLeap.TTSUtil;

void SetLanguageToEnglish()
{
    TextToSpeechAPI.SetLanguage(SupportedLanguage.ENGLISH);
}
```

### Speaking Text

To make the TTS engine speak text, use the `Speak` method. You can choose to queue the text or flush the current speech:

```csharp
using MagicLeap.TTSUtil;

void SpeakText(string text)
{
    TextToSpeechAPI.Speak(text, TTSQueueMode.QUEUE_MODE_FLUSH);
}
```

### Checking if the TTS Engine is Speaking

You can check if the TTS engine is currently speaking using the `IsSpeaking` method:

```csharp
using MagicLeap.TTSUtil;

bool isSpeaking = TextToSpeechAPI.IsSpeaking();
```

### Stopping the Current Speech

To stop the current speech, use the `StopSpeaking` method:

```csharp
using MagicLeap.TTSUtil;

void StopSpeech()
{
    TextToSpeechAPI.StopSpeaking();
}
```

## Sample Usage

The package includes a sample script (`TextToSpeechSample`) demonstrating basic usage of the TTS API. To use the sample script:

1. Import the package as described in the installation section.
2. Locate the `TextToSpeechSample` script in the package content.
3. Attach the `TextToSpeechSample` script to a GameObject in your scene.

The sample script initializes the TTS engine, sets the language, and demonstrates speaking text and stopping speech.

## Copyright

Copyright (c) 2023-present Magic Leap, Inc. All Rights Reserved. Use of this file is governed by the Developer Agreement, located here: https://www.magicleap.com/software-license-agreement-ml2