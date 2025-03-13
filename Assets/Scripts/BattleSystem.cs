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
    Carro carro1, carro2;
    CursorControls cursor1, cursor2;
    public PlayerInputManager playerManager;
    public TextMeshProUGUI  dialogueText;
    public Canvas cnvs;
    public GameObject[] players;
    public GameObject[] panels;
    public GameObject[] characters;
    public GameObject[] pistas;
    public Transform spawn1, spawn2;
    public BattleState state;
    Galo p1Galo, p2Galo;
    public bool p1Decided = false , p2Decided = false;
    private bool roomFull = false;

    public BattleHud p1HUD, p2HUD; 

    // Start is called before the first frame update
    void Start()
    {
        cnvs.gameObject.SetActive(false);  
    }

    // Update is called once per frame
    void Update()
    {
        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();           
            StartTheGame();
        }
    }
    void StartTheGame()
    {
        cnvs.gameObject.SetActive(true);
       

        panels[0].SetActive(false);
        panels[1].SetActive(false);
        p1HUD.gameObject.SetActive(false);
        p2HUD.gameObject.SetActive(false);
        state = BattleState.START;
        StartCoroutine(SetupBattle());
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

        players[0] = GameObject.Find("P1");

        players[1] = GameObject.Find("Galo1");

        players[2] = GameObject.Find("P2");

        players[3] = GameObject.Find("Galo2");

        cursor1 = players[0].GetComponent<CursorControls>();
        carro1 = players[1].GetComponent<Carro>();
        cursor2 = players[2].GetComponent<CursorControls>();
        carro2 = players[3].GetComponent<Carro>();
        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        p1HUD.gameObject.SetActive(true);
        p2HUD.gameObject.SetActive(true);
        dialogueText.text = "Selecione uma ação!";
    }
    void CarRace()
    {
        p1HUD.gameObject.SetActive(false);
        p2HUD.gameObject.SetActive(false);     
        dialogueText.text = "Chegue até o final";
        TrocaPlayer();
        GameObject pistache = Instantiate(pistas[UnityEngine.Random.Range(0, pistas.Length - 1)]);

    }

    public void Action1(int actionNum)
    {
        p1Decided = true;
        if (p1Decided && p2Decided)
        {
            state = BattleState.CarRace;
            CarRace();
        }

    }
    public void Action2(int actionNum)
    {
        p2Decided = true;
        if (p1Decided && p2Decided)
        {
            state = BattleState.CarRace;
            CarRace();
        }
    }
   private void TrocaPlayer()
    {
        players[0].GetComponent<SpriteRenderer>().enabled = !players[0].GetComponent<SpriteRenderer>().enabled;
        cursor1.inputEnabled = !cursor1.inputEnabled;
        players[1].GetComponent<SpriteRenderer>().enabled = !players[1].GetComponent<SpriteRenderer>().enabled;
        carro1.inputEnabled = !carro1.inputEnabled;
        players[2].GetComponent<SpriteRenderer>().enabled = !players[2].GetComponent<SpriteRenderer>().enabled;
        cursor2.inputEnabled = !cursor2.inputEnabled;
        players[3].GetComponent<SpriteRenderer>().enabled = !players[3].GetComponent<SpriteRenderer>().enabled;
        carro2.inputEnabled = !carro2.inputEnabled;
    }
}
