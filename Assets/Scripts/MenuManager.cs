using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public PlayerInputManager playerManager;
    public string cenaStart;
    private bool roomFull = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
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
