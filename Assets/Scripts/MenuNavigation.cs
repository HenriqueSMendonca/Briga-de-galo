using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public PlayerInput PlayerInput;
    // Start is called before the first frame update
    void Start()
    {
        
        if (GameObject.FindGameObjectsWithTag("Player").Length == 1)
        {
            gameObject.name = "P1";
  
        }
        else
        {
            gameObject.name = "P2";
        }
        if (gameObject.name == "P1")
        {
            PlayerInput.uiInputModule = GameObject.FindGameObjectWithTag("Input1").GetComponent<InputSystemUIInputModule>();
        }
        else
        {
            PlayerInput.uiInputModule = GameObject.FindGameObjectWithTag("Input2").GetComponent<InputSystemUIInputModule>();
        }
    }

        
    
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (gameObject.name == "P1")
        {
            PlayerInput.uiInputModule = GameObject.FindGameObjectWithTag("Input1").GetComponent<InputSystemUIInputModule>();
        }
        else
        {
            PlayerInput.uiInputModule = GameObject.FindGameObjectWithTag("Input2").GetComponent<InputSystemUIInputModule>();
        }
    }
    
    
}
