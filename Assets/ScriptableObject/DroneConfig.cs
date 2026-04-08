using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DroneConfig", menuName = "Drone Config")]

public class DroneConfig : ScriptableObject
{
// maxSpeed — максимальная скорость дрона
// acceleration — ускорение
// rotationSpeed — скорость поворота
// batteryLife — время полёта (в секундах)
// obstaclePenalty — штраф за столкновение

    public float maxSpeed = 50f;
    public float acceleration = 50f;
    public float rotationSpeed = 50f;
    public float batteryLife = 5f;
    public float obstaclePenalty = 50f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
