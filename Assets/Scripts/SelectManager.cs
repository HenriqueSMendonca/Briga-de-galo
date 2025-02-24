using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public PlayerInputManager playerManager;
    public Button startBtn;
    private bool p1Selected, p2Selected;
    
    void Start()
    {
        startBtn.interactable = false;
        p1Selected = false;
        p2Selected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.playerCount == 2)
        {
            playerManager.DisableJoining();
        }
        if (p1Selected && p2Selected)
        {
            startBtn.interactable = true;
        }
    }

    public void CharacterChoice1(int choice)
    {
        PlayerPrefs.SetInt("selectedChar1", choice);   
        p1Selected = true;
    }
    public void CharacterChoice2(int choice)
    {
        PlayerPrefs.SetInt("selectedChar2", choice);
        p2Selected = true;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
