using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChangePlayer : MonoBehaviour
{
    [Header("References")]
    public PlayerInput droneInput;
    public MonoBehaviour droneController; // скрипт движения дрона
    public GameObject droneDisplay;

    public PlayerInput playerInput;
    public MonoBehaviour playerController; // скрипт движения игрока
    public GameObject playerDisplay;

    void Start() {
        // по умолчанию контролируем игрока
        ActivatePlayer();
    }

    private void Update() {
        if (Input.GetKeyUp(KeyCode.F5)) {
            Toggle();
        }
    }

    public void ActivateDrone() {
        droneInput.enabled = true;
        if (droneController != null)
            droneController.enabled = true;
        if (droneDisplay != null)
            droneDisplay.SetActive(true);

        playerInput.enabled = false;
        if (playerController != null)
            playerController.enabled = false;
        if (playerDisplay != null)
            playerDisplay.SetActive(false);
    }

    public void ActivatePlayer() {
        playerInput.enabled = true;
        if (playerController != null)
            playerController.enabled = true;
        if (playerDisplay != null)
            playerDisplay.SetActive(true);

        droneInput.enabled = false;
        if (droneController != null)
            droneController.enabled = false;
        if (droneDisplay != null)
            droneDisplay.SetActive(false);
    }

    // переключение (например по клавише)
    public void Toggle() {
        Debug.LogWarning("pressed");
        if (playerInput.enabled)
            ActivateDrone();
        else
            ActivatePlayer();
    }
}

