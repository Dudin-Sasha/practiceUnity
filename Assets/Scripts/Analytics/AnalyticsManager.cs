using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;


[System.Serializable]
public class AnalyticsEvent
{
    public string eventName;
    public string time;
    public string data; // JSON строка

    [SerializeField] private sceneID sceneID;

    public AnalyticsEvent(string eventName, float time, string data = "")
    {
        this.eventName = eventName;
        this.time = time.ToString();
        this.data = data;
    }
}

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }
    [SerializeField] private Save save;
    [SerializeField] private sceneID sceneID;
    private Queue<AnalyticsEvent> eventQueue = new Queue<AnalyticsEvent>();
    private string saveFilePath;
    private bool hasInternet = true;
    float timer = 0f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;
        DontDestroyOnLoad(gameObject);

        saveFilePath = Application.persistentDataPath + "/analytics.json";

        // Загружаем несохранённые события
        LoadEventsFromFile();

        InvokeRepeating(nameof(CheckInternetAndSend), 2f, 5f);
    }



    public void LogEvent(string eventName,float time ,string data = "")
    {
        AnalyticsEvent evt = new AnalyticsEvent(eventName, time ,data);
        eventQueue.Enqueue(evt);

        Debug.Log($"Event logged: {eventName} | Data: {data}");


        TrySendEvent(evt);
    }


    private void TrySendEvent(AnalyticsEvent evt)
    {
        // int id = save.currentId;
        save.SaveGame(sceneID.sceneId,timer);
        //тут в сохранение закину
        if (hasInternet)
        {
            SendToServer(evt);
        }
        else
        {
            SaveEventToFile(evt);
        }
    }


    private void SendToServer(AnalyticsEvent evt)
    {
        string json = JsonUtility.ToJson(evt, true);
        Debug.Log($"[Server] Sending analytics:\n{json}");
    }

    private void SaveEventToFile(AnalyticsEvent evt)
    {
        string json = JsonUtility.ToJson(evt);
        string line = json + System.Environment.NewLine;

        try
        {
            File.AppendAllText(saveFilePath, line);
            Debug.Log($"[Analytics] Event saved to file: {saveFilePath}");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[Analytics] Failed to save event: {ex.Message}");
        }
    }


    private void LoadEventsFromFile()
    {
        if (!File.Exists(saveFilePath)) return;

        try
        {
            string[] lines = File.ReadAllLines(saveFilePath);
            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    AnalyticsEvent evt = JsonUtility.FromJson<AnalyticsEvent>(line);
                    if (evt != null)
                    {
                        eventQueue.Enqueue(evt);
                    }
                }
            }

            Debug.Log($"[Analytics] Loaded {eventQueue.Count} events from file");
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"[Analytics] Failed to load events: {ex.Message}");
        }
    }

    private void CheckInternetAndSend()
    {
        hasInternet = Application.internetReachability != NetworkReachability.NotReachable;

        if (!hasInternet) return;

        while (eventQueue.Count > 0)
        {
            AnalyticsEvent evt = eventQueue.Dequeue();
            SendToServer(evt);
        }

        // Если все события отправлены, удаляем файл
        if (eventQueue.Count == 0 && File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("[Analytics] All events sent, file cleared");
        }
    }

    private void OnApplicationQuit()
    {
        LogEvent("game_closed",timer, "");
    }

    void Update()
    {
        timer += Time.deltaTime;
    }
}
