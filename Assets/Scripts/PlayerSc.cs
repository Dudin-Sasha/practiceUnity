using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSc : MonoBehaviour
{
    [SerializeField] private DroneConfig config;
    public float maxSpeed;
    public float acceleration;
    public float rotationSpeed;
    public float batteryLife;
    public float hoverHeight = 5f;
    
    private Rigidbody rb;
    private Vector3 targetVelocity;
    private float targetYaw;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        maxSpeed = config.maxSpeed;
        acceleration = config.acceleration;
        rotationSpeed = config.rotationSpeed;
        batteryLife = config.batteryLife;
        rb.useGravity = true;
        
        StartCoroutine(batteryTimer());
    }
    
    IEnumerator batteryTimer()
    {
        while (batteryLife > 0)
        {
            // Debug.Log("Осталось: " + batteryLife + " сек.");
            yield return new WaitForSeconds(1.0f); // Ждем 1 секунду
            batteryLife--;
        }
        Debug.Log("Время вышло!");
    }
    void FixedUpdate()
    {

        float horizontal = 0;
        float vertical = 0;
        float ascend = 0.01f;

        if (batteryLife > 0)
        {
           // Управление
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        // ascend = Input.GetAxis("Vertical2"); // Q/E или Space/Ctrl
        }
        else
        {
            Debug.Log("Села батарейка");
            Time.timeScale = 0;
        }
        
        float yaw = Input.GetAxis("Mouse X"); 
        
        // Движение вперед/назад и влево/вправо
        Vector3 moveDirection = transform.forward * vertical + transform.right * horizontal;
        targetVelocity = moveDirection * maxSpeed;
        
        // Применяем силу для плавного ускорения
        Vector3 velocityChange = targetVelocity - rb.velocity;
        velocityChange.y = 0;
        rb.AddForce(velocityChange * acceleration, ForceMode.Acceleration);
        
        // Вертикальное движение
        if (Mathf.Abs(ascend) > 0.1f)
        {
            rb.AddForce(Vector3.up * ascend * acceleration * 2f, ForceMode.Acceleration);
        }
        
        // Автоматическое зависание
        if (Mathf.Abs(ascend) < 0.1f && Mathf.Abs(rb.velocity.y) < 0.5f)
        {
            float heightDifference = hoverHeight - transform.position.y;
            rb.AddForce(Vector3.up * heightDifference * 5f, ForceMode.Acceleration);
        }
        
        // Поворот
        targetYaw += yaw * rotationSpeed * Time.fixedDeltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, targetYaw, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
    }
}

