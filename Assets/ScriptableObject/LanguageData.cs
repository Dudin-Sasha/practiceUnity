using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "LanguageData", menuName = "Localization/Language Data")]
public class LanguageData : ScriptableObject
{
    public List<string> languages = new List<string>() { "Russian", "English" };
    
    [System.Serializable]
    public class LocalizationEntry
    {
        public string key;
        public string russianText;
        public string englishText;
    }
    
    public List<LocalizationEntry> entries = new List<LocalizationEntry>();
    
    public string GetText(string key, string language)
    {
        foreach (var entry in entries)
        {
            if (entry.key == key)
            {
                if (language == "Russian")
                    return entry.russianText;
                else if (language == "English")
                    return entry.englishText;
            }
        }
        return $"Missing key: {key}";
    }
}