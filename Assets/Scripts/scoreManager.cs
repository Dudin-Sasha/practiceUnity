using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class scoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI speedometr;
    [SerializeField] private TextMeshProUGUI xp;
    [SerializeField] private PlayerSc pl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void upd(float speed = 0, float score = 0) {
        xp.text = $"your score is {score}";
        speedometr.text = $"{speed}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
