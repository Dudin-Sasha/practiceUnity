
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance { get; private set; }
    
    public LanguageData languageData;
    private string currentLanguage = "Russian";
    
    public System.Action OnLanguageChanged;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void SetLanguage(string language)
    {
        if (languageData.languages.Contains(language))
        {
            currentLanguage = language;
            OnLanguageChanged?.Invoke();
        }
    }
    
    public string GetText(string key)
    {
        return languageData.GetText(key, currentLanguage);
    }
    
    public string GetCurrentLanguage()
    {
        return currentLanguage;
    }
}

