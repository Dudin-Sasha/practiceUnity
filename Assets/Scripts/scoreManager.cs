using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class scoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedometr;
    [SerializeField] private TextMeshProUGUI xp;
     [SerializeField] private PlayerSc pl;
    [SerializeField] private GameObject finalMenu;
    [SerializeField] private TextMeshProUGUI finalText;
    //[SerializeField] private targetRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void upd(float speed = 0, float score = 0) {
        xp.text = $"your score is {score}";
        speedometr.text = $"{Mathf.RoundToInt(speed)}км/ч";
    }

    public void menu(string text)
    {
        finalMenu.SetActive(true);
        finalText.text = text;
    }

    private void FixedUpdate() {
        upd(pl.speed, pl.score);
    }
}
