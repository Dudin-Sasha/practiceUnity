using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class AnalyticsManagerSc : MonoBehaviour
{
    private string timer;
    private void Start()
    {   
        AnalyticsManager.Instance.LogEvent("game_started", 0 , "");
    }

    public void OnLevelCompleted(int timeSpent, int score)
    {
        // Уровень пройден
        string data = $"{{\"time\": {timeSpent}, \"score\": {score}}}";
        AnalyticsManager.Instance.LogEvent("level_completed", timeSpent ,data);
    }

    public void OnDroneCrashed()
    {
        // Дрон разбился
        AnalyticsManager.Instance.LogEvent("drone_crashed", 0 ,"");
    }
}
