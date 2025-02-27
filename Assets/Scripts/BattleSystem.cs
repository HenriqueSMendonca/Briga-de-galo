using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, P1TURN, P2TURN, P1WIN, P2WIN }

public class BattleSystem : MonoBehaviour
{
    public TextMeshProUGUI  dialogueText;
    public GameObject[] characters;
    public Transform spawn1, spawn2;
    public BattleState state;
    Galo p1Galo, p2Galo;

    public BattleHud p1HUD, p2HUD; 

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetupBattle()
    {
        GameObject p1GO = Instantiate(characters[PlayerPrefs.GetInt("selectedChar1")], spawn1.position, Quaternion.identity);
        p1Galo = p1GO.GetComponent<Galo>();
         GameObject p2GO = Instantiate(characters[PlayerPrefs.GetInt("selectedChar2")], spawn2.position, new Quaternion(0, 180, 0, 0));
        p2Galo = p2GO.GetComponent<Galo>();

        dialogueText.text = p1Galo.nomeGalo + " e " + p2Galo.nomeGalo + " estão prontos pra brigar";
        p1HUD.SetHUD(p1Galo);
        p2HUD.SetHUD(p2Galo);
    }
}
