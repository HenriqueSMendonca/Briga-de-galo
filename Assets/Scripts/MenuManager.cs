using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public PlayerInputManager playerManager;
    public string cenaStart;
    private bool roomFull = false;
    public GameObject roadBlock;
    public EventSystem pe1, pe2;
    public GameObject Buttons;
    // Start is called before the first frame update
    void Start()
    {
        Buttons.SetActive(false);
        roadBlock.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
            roadBlock.SetActive(false);
            Buttons.SetActive(true);
            pe1.SetSelectedGameObject(pe1.firstSelectedGameObject);
            pe2.SetSelectedGameObject(pe2.firstSelectedGameObject);
        }
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(cenaStart);
    }
    public void Close()
    {
        Application.Quit();
    }
    public void Text()
    {
        Debug.Log("alalala");
    }
}
