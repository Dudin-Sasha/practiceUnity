using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System;
using System.Text;
using TMPro;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData
{
    public string version = "1.0";
    public float bestTime = 0f;
    public int totalScore = 0;
    public List<int> unlockedLevels = new List<int> { 1 }; // По умолчанию открыт уровень 1

    public SaveData()
    {
        unlockedLevels = new List<int> { 1 };
    }
}



public class Save : MonoBehaviour
{
    private readonly string version = "1.0";
    public SaveData data = new SaveData {version = "1.0", bestTime = 1000, totalScore = 0, unlockedLevels = {0,1,3}};

    public TextMeshProUGUI[] slots;


    public static Save Instance;

    [SerializeField] private sceneID sceneID;



    private string GetSavePath(int slot){
        string path = Application.persistentDataPath + $"/save{slot}.json";
        Debug.Log(Application.persistentDataPath);
        return path;
    }


    #region Base64
    private static string EncryptBase64(string plainText)
    {
        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
        return Convert.ToBase64String(plainBytes);
    }

    private static string DecryptBase64(string encryptedText)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
        return Encoding.UTF8.GetString(encryptedBytes);
    }
#endregion

#region SaveLoad
    public void SaveGame(int slot, float time = 1000)
    {
        data.bestTime = time;//тут бы что-то типа ввода какой у меня там бест тайм, но игры то самой нету...
        data.totalScore = 0;
        data.version = "1.0";
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(GetSavePath(slot), EncryptBase64(json));
        Debug.Log($"Игра сохранена в слот {slot}");
    }

    public void LoadGame(int slot)
    {
        Debug.Log(slot);
        sceneID.sceneId = slot;
        string path = GetSavePath(slot);
        if (File.Exists(path))
        {
            string json = DecryptBase64(File.ReadAllText(path));
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            if (version != data.version) {
                Debug.LogError($"Бля версия то не та! у тебя {data.version}, a надо {version}");
            }
            else{
                // SaveInfo(slot);
                SceneManager.LoadScene("GameField");
            //вообще надо будет тут загружать сцену с нужными данными но сцены то нет
            }

        }
        else
        {
            SaveGame(slot);
            LoadGame(slot);
        }
    }
#endregion

public void SaveInfo(int slot)
    {
        try
        {
            string json = DecryptBase64(File.ReadAllText(GetSavePath(slot)));
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            string Text = $"Версия: {data.version}\nЛучшее время: {data.bestTime} сек\nОчки: {data.totalScore}\nОткрытые уровни: {string.Join(", ", data.unlockedLevels)}";
            Debug.Log(Text);
            slots[slot].text = Text;
        }
        catch (System.Exception)
        {
            // Debug.Log(e.Message);
        }
    }

    void Start()
    {
        SaveInfo(0);
        SaveInfo(1);
        SaveInfo(2);
    }

    // void FixedUpdate()
    // {
    //     if (Input.GetKeyDown(KeyCode.F5))
    //     {
    //         SaveGame();
    //         Debug.Log("Quick Save (F5)");
    //     }

    //     if (Input.GetKeyDown(KeyCode.F9))
    //     {
    //         LoadGame();
    //         Debug.Log("Quick Load (F9)");
    //     }     
    // }


}