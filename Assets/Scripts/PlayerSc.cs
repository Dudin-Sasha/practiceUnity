using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSc : MonoBehaviour {
    [SerializeField] private DroneConfig config;
    public float maxSpeed;
    public float acceleration;
    public float rotationSpeed;
    public float batteryLife;
    public float hoverHeight = 5f;
    public float obstaclePenalty;

    public float score = 0;

    private Rigidbody rb;



    void Start() {
        rb = GetComponent<Rigidbody>();
        maxSpeed = config.maxSpeed;
        acceleration = config.acceleration;
        rotationSpeed = config.rotationSpeed;
        batteryLife = config.batteryLife;
        obstaclePenalty = config.obstaclePenalty;

        //rb.useGravity = true;

        StartCoroutine(batteryTimer());
    }

    private Vector3 AccelerationCheck(Vector3 sp) {
        var speed = sp * acceleration;

        //rb.useGravity = (speed.y != 0);

        if (speed.x > maxSpeed)
            speed.x = maxSpeed;
        if (speed.y > maxSpeed)
            speed.y = maxSpeed;


        if (speed.z > maxSpeed)
            speed.z = maxSpeed;


        //бляяя спидометр....
        //Debug.
        return speed;
    }

    void FixedUpdate() {
        //rb.useGravity = false;

        // Подъем / Спуск
        if (Input.GetKey(KeyCode.Space))
            rb.AddRelativeForce(AccelerationCheck(Vector3.up));
        if (Input.GetKey(KeyCode.LeftControl))
            rb.AddRelativeForce(AccelerationCheck(Vector3.down));



        //rb.AddRelativeForce(0,9.8f,0);
        // Вперед / Назад
        float forward = Input.GetAxis("Vertical");
        rb.AddRelativeForce(AccelerationCheck(Vector3.forward * forward));

        // Поворот (Yaw)
        float turn = Input.GetAxis("Horizontal");
        rb.AddRelativeTorque(Vector3.up * turn * rotationSpeed);
    }

    IEnumerator batteryTimer() {
        while (batteryLife > 0) {
            // Debug.Log("Осталось: " + batteryLife + " сек.");
            yield return new WaitForSeconds(1.0f); // Ждем 1 секунду
            batteryLife--;
        }
        Debug.Log("Время вышло!");
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "-") {
            //Vector3 = (0, 0, 0);
            rb.AddRelativeForce(AccelerationCheck(Vector3.forward * -0.1f));
            score -= obstaclePenalty;
        }
    }

    void OnTriggerEnter(Collider other) {
        switch (other.tag) {
            case ("+"):
                score += 10;
                break;
            case ("finish"):
                //Time.TimeScale = 0;
                break;
            default:
                break;
        }
    }

}

