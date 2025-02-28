using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum BattleState { START, PlayerTurn, CarRace, Comandos ,P1WIN, P2WIN }

public class BattleSystem : MonoBehaviour
{
    public PlayerInputManager playerManager;
    public TextMeshProUGUI  dialogueText;   
    public GameObject[] panels;
    public GameObject[] characters;
    public Transform spawn1, spawn2;
    public BattleState state;
    Galo p1Galo, p2Galo;
    public bool p1Decided = false , p2Decided = false;
    private bool roomFull = false;

    public BattleHud p1HUD, p2HUD; 

    // Start is called before the first frame update
    void Start()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        p1HUD.gameObject.SetActive(false);
        p2HUD.gameObject.SetActive(false);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.playerCount == 2 && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
        }
    }
    IEnumerator SetupBattle()
    {
        GameObject p1GO = Instantiate(characters[PlayerPrefs.GetInt("selectedChar1")], spawn1.position, Quaternion.identity);
        p1Galo = p1GO.GetComponent<Galo>();
         GameObject p2GO = Instantiate(characters[PlayerPrefs.GetInt("selectedChar2")], spawn2.position, new Quaternion(0, 180, 0, 0));
        p2Galo = p2GO.GetComponent<Galo>();

        dialogueText.text = p1Galo.nomeGalo + " e " + p2Galo.nomeGalo + " estão prontos pra brigar!";
        p1HUD.SetHUD(p1Galo);
        p2HUD.SetHUD(p2Galo);
        yield return new WaitForSeconds(3);
         
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        p1HUD.gameObject.SetActive(true);
        p2HUD.gameObject.SetActive(true);
        dialogueText.text = "Selecione uma ação!";
    }

    public void Action1(int actionNum)
    {
        p1Decided = true;

    }
    public void Action2(int actionNum)
    {
        p2Decided = true;
    }
   
}
