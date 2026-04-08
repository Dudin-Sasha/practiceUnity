using UnityEngine;

public class LanguageButtons : MonoBehaviour
{
    public void SetRussian()
    {
        LocalizationManager.Instance.SetLanguage("Russian");
        Debug.Log("выбран русский");
    }
    
    public void SetEnglish()
    {
        LocalizationManager.Instance.SetLanguage("English");
        Debug.Log("Selected English");
    }
    public void ChangeLanguage()
    {
        switch (LocalizationManager.Instance.GetCurrentLanguage())
        {
            case ("Russian"):
                {
                    SetEnglish();
                    break;
                }
            case ("English"):
                {
                    SetRussian();
                    break;
                }
            default:
                {
                    Debug.LogWarning("error");
                    break;
                }
        }
        
    }
}