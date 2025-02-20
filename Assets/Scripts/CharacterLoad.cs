using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoad : MonoBehaviour
{
    public GameObject[] characters;
    public Transform spawn;
    void Start()
    {
        Instantiate(characters[PlayerPrefs.GetInt("selectedChar")], spawn.position,Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
