using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MagicLeap.TTSUtil
{
    /// <summary>
    /// Enumerate the languages for which there is TTS support on the ML device.
    /// </summary>
    public static class Languages
    {
        #region Public
        public enum SupportedLanguage
        {
            Arabic,
            English,
            French,
            German,
            Italian,
            Japanese,
            Portuguese,
            Spanish,
        }

        public static string ToAbbreviatedName(SupportedLanguage language)
        {
            int itemIndex = _supportedLanguages.FindIndex(
                (LanguageInfo info) => (language == info.Language));

            Debug.Assert(itemIndex >= 0, "_supportedLanguages must " +
                "contain '" + language + "'.");

            return _supportedLanguages[itemIndex].AbbreviatedName;
        }

        public static bool IsSupportedLanguage(string abbreviatedName)
        {
            int itemIndex = _supportedLanguages.FindIndex(
                (LanguageInfo info) => (abbreviatedName == info.AbbreviatedName));

            return (itemIndex >= 0);
        }

        public static SupportedLanguage ToSupportedLanguage(string abbreviatedName)
        {
            int itemIndex = _supportedLanguages.FindIndex(
                (LanguageInfo info) => (abbreviatedName == info.AbbreviatedName));

            if (itemIndex < 0)
            {
                throw new ArgumentException(
                    $"'{abbreviatedName}' is not a supported language.");
            }

            return _supportedLanguages[itemIndex].Language;
        }
        #endregion

        #region Private
        private record LanguageInfo
        {
            public readonly SupportedLanguage Language;
            public readonly string AbbreviatedName;

            public LanguageInfo(SupportedLanguage language, string abbreviatedName)
            {
                Language = language;
                AbbreviatedName = abbreviatedName;
            }
        }

        private static readonly List<LanguageInfo> _supportedLanguages = new()
        {
            new LanguageInfo(SupportedLanguage.Arabic, "ar"),
            new LanguageInfo(SupportedLanguage.English, "en"),
            new LanguageInfo(SupportedLanguage.French, "fr"),
            new LanguageInfo(SupportedLanguage.German, "de"),
            new LanguageInfo(SupportedLanguage.Italian, "it"),
            new LanguageInfo(SupportedLanguage.Japanese, "ja"),
            new LanguageInfo(SupportedLanguage.Portuguese, "pt"),
            new LanguageInfo(SupportedLanguage.Spanish, "es"),
        };
        #endregion
    }
}
