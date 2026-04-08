using TMPro;
using UnityEngine;
using UnityEngine.UI;

// [RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{
    public string localizationKey;
    private TextMeshProUGUI textComponent;
    
    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        UpdateText();
        
        LocalizationManager.Instance.OnLanguageChanged += UpdateText;
    }
    
    private void UpdateText()
    {
        if (textComponent != null)
        {
            textComponent.text = LocalizationManager.Instance.GetText(localizationKey);
        }
    }
    
    private void OnDestroy()
    {
        if (LocalizationManager.Instance != null)
        {
            LocalizationManager.Instance.OnLanguageChanged -= UpdateText;
        }
    }
}