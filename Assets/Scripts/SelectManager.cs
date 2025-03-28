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
    public Sprite[] characters;
    public SpriteRenderer p1, p2;
    private bool roomFull = false;
    
    void Start()
    { 
        PlayerPrefs.SetInt("selectedChar1", 4);
        PlayerPrefs.SetInt("selectedChar2", 4);
        startBtn.interactable = false;
        p1Selected = false;
        p2Selected = false;
        p1.gameObject.SetActive(false);
        p2.gameObject.SetActive(false);
    }

    
    void Update()
    {

        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
        }      
        p1.sprite = characters[PlayerPrefs.GetInt("selectedChar1")];
        p2.sprite = characters[PlayerPrefs.GetInt("selectedChar2")];
    }

    public void CharacterChoice1(int choice)
    {
        PlayerPrefs.SetInt("selectedChar1", choice);
        p1Selected = true;
        if (p1.gameObject.activeSelf == false)
        {
            p1.gameObject.SetActive(true);
        }
        if (p1Selected && p2Selected)
        {
            startBtn.interactable = true;
        }
    }
    public void CharacterChoice2(int choice)
    {
        PlayerPrefs.SetInt("selectedChar2", choice);
        p2Selected = true;
        if (p2.gameObject.activeSelf == false)
        {
            p2.gameObject.SetActive(true);
        }
        if (p1Selected && p2Selected)
        {
            startBtn.interactable = true;
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
