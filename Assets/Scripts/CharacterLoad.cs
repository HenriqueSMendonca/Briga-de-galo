using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLoad : MonoBehaviour
{
    public GameObject[] characters;
    public Transform spawn1, spawn2;
    void Start()
    {
        Instantiate(characters[PlayerPrefs.GetInt("selectedChar1")], spawn1.position,Quaternion.identity);
        Instantiate(characters[PlayerPrefs.GetInt("selectedChar2")], spawn2.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
