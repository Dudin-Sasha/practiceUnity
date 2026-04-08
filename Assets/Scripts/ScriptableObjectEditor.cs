using System.Xml.Serialization;
using UnityEngine;
// using UnityEngine.UI;
using TMPro;

public class ScriptableObjectEditor : MonoBehaviour
{
    [SerializeField] private DroneConfig config;
    
    [SerializeField] private TMP_InputField batteryField; 
    [SerializeField] private TMP_InputField accelerationField;
    [SerializeField] private TMP_InputField rotationField; 
    [SerializeField] private TMP_InputField maxSpeedField; 
    [SerializeField] private TMP_InputField obstanceField; 

    private void Start()
    {
        // Загружаем текущие значения в InputField'ы
        LoadValuesToUI();
        
        // // Подписываемся на события изменения
        batteryField.onEndEdit.AddListener(OnbatteryLifeChanged);
        accelerationField.onEndEdit.AddListener(OnaccelerationChanged);
        rotationField.onEndEdit.AddListener(OnRotationSpeedChanged);
        maxSpeedField.onEndEdit.AddListener(OnMaxSpeedChanged);
        obstanceField.onEndEdit.AddListener(OnObstaclePenaltyChanged);
    }

    private void LoadValuesToUI()
    {
        batteryField.text = config.batteryLife.ToString();
        accelerationField.text = config.acceleration.ToString();
        rotationField.text = config.rotationSpeed.ToString();
        maxSpeedField.text = config.maxSpeed.ToString();
        obstanceField.text = config.obstaclePenalty.ToString();
    }

    private void OnbatteryLifeChanged(string value)
    {
        float.TryParse(value, out float res);
        config.batteryLife = res;
    }

    private void OnaccelerationChanged(string value)
    {
        float.TryParse(value, out float res);

        config.acceleration = res;
    }

    private void OnRotationSpeedChanged(string value)
    {
        float.TryParse(value, out float res);
        config.rotationSpeed = res;
    }

    private void OnMaxSpeedChanged(string value)
    {
        float.TryParse(value, out float res);
        config.maxSpeed = res;

    }

    private void OnObstaclePenaltyChanged(string value)
    {
        float.TryParse(value, out float res);
        config.obstaclePenalty = res;
    }



    private void SaveObject()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorUtility.SetDirty(config);
        UnityEditor.AssetDatabase.SaveAssets();
        #endif
    }
    public void SaveBtn()
    {
        SaveObject();
    }

    public void DefaultBtn()
    {
        config.obstaclePenalty = 15;
        config.maxSpeed = 80;
        config.rotationSpeed = 2000;
        config.acceleration = 50;
        config.batteryLife = 150;
        LoadValuesToUI();
        SaveObject();
    }
}
