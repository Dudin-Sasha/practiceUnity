using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class buttons_sc : MonoBehaviour
{
    [SerializeField] sceneID SceneID;
    private Save saveManager;
    [SerializeField] private GameObject saveMenu;
    private bool saveFlag = false;
    public void Quit()
    {
        saveManager.SaveGame(SceneID.sceneId);

        Application.Quit();
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ToSaveMenu()
    {
        saveFlag = !saveFlag;
        saveMenu.SetActive(saveFlag);
    }
    

}
