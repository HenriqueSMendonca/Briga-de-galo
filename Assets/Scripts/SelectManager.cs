using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectManager : MonoBehaviour
{
    public int selectNum;
    public string sceneName;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (selectNum <= 0)
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    public void CharacterChoice(int choice)
    {
        PlayerPrefs.SetInt("selectedChar", choice);
        selectNum--;
    }
}
