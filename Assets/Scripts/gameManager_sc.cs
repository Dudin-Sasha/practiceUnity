using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager_sc : MonoBehaviour
{

    [SerializeField] private AnalyticsManager AM;
    [SerializeField] private PlayerSc PlaySc;
    [SerializeField] private scoreManager SM;

    public void OnFinish()
    {
        AM.score = PlaySc.score;
        AM.OnLevelCompleted(AM.timer, AM.score);
        SM.menu("Уровень пройден");
        Time.timeScale = 0;
    }

    public void TimeOut()
    {
        AM.score = 0;
        AM.OnBatteryOut();
        SM.menu("Вы проиграли");
        Time.timeScale = 0;
    }
}
