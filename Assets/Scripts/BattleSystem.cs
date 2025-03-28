using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum BattleState { START, PlayerTurn, CarRace, Comandos ,WIN }

public class BattleSystem : MonoBehaviour
{
    Carro carro1, carro2;
    CursorControls cursor1, cursor2;
    public PlayerInputManager playerManager;
    public TextMeshProUGUI dialogueText;
    public Canvas cnvs;
    public GameObject[] players;
    public GameObject[] panels;
    public GameObject[] characters;
    public GameObject[] pistas;
    public Transform spawn1, spawn2;
    public BattleState state;
    Galo p1Galo, p2Galo;
    public bool p1Decided = false, p2Decided = false;
    private bool roomFull = false;
    public bool whoWonRace;
    private int moveCount;
    private GameObject pistache;
    public GameObject menu1, menu2;
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

        dialogueText.text = p1Galo.nomeGalo + " e " + p2Galo.nomeGalo + " est�o prontos pra brigar!";
        p1HUD.SetHUD(p1Galo);
        p1Galo.battleHud = p1HUD;
        p2HUD.SetHUD(p2Galo);
        p2Galo.battleHud = p2HUD;
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
        p1Galo.OnAfterTurn();
        p2Galo.OnAfterTurn();
        for (int i = 0; p1Galo.Status?.Count > i; i++)
        {
            Debug.Log(p1Galo.Status[i].Name);
        }
        for (int i = 0; p2Galo.Status?.Count > i; i++)
        {
            Debug.Log(p2Galo.Status[i].Name);
        }
        carro1.acceleration = p1Galo.carSpeed;
        carro2.acceleration = p2Galo.carSpeed;

        p1Galo.tookDamage = false;
        p2Galo.tookDamage = false;
        moveCount = 0;
        panels[0].SetActive(false);
        panels[1].SetActive(false);
        p1HUD.gameObject.SetActive(true);
        p2HUD.gameObject.SetActive(true);
        menu1.SetActive(true);
        menu2.SetActive(true);
        dialogueText.text = "Selecione uma a��o!";
    }
    void CarRace()
    {
        p1Decided = false;
        p2Decided = false;
        p1HUD.gameObject.SetActive(false);
        p2HUD.gameObject.SetActive(false);
        menu1.SetActive(false);
        menu2.SetActive(false);
        dialogueText.text = "Chegue at� o final";
        TrocaPlayer();
        pistache = Instantiate(pistas[UnityEngine.Random.Range(0, pistas.Length - 1)]);

    }
    public void Briga()
    {
        p1HUD.gameObject.SetActive(true);
        p2HUD.gameObject.SetActive(true);
        TrocaPlayer();
        Destroy(pistache);
        if (whoWonRace == false)
        {

            dialogueText.text = "O jogador 1 ganhou a prioridade!";
            StartCoroutine(UseMove(p1Galo, p2Galo));


        }
        else
        {
            dialogueText.text = "O jogador 2 ganhou a prioridade!";
            StartCoroutine(UseMove(p2Galo, p1Galo));
        }
    }

    public void Action1(int actionNum)
    {
        p1Galo.selectedMove = actionNum;
        p1Decided = true;
        carro1.acceleration += p1Galo.moves[p1Galo.selectedMove].Priority;
        if (p1Decided && p2Decided)
        {

            state = BattleState.CarRace;           
            CarRace();
        }

    }
    public void Action2(int actionNum)
    {
        p2Galo.selectedMove = actionNum;
        p2Decided = true;
        carro1.acceleration += p1Galo.moves[p1Galo.selectedMove].Priority;
        if (p1Decided && p2Decided)
        {
            state = BattleState.CarRace;
            carro2.acceleration += p2Galo.moves[actionNum].Priority;
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
    IEnumerator UseMove(Galo galo1, Galo galo2)
    {
        for (int i = 0; p1Galo.Status?.Count > i; i++)
        {
            Debug.Log(i);
            Debug.Log(p1Galo.Status[i].Name);
        }
        for (int i = 0; p2Galo.Status?.Count > i; i++)
        {
            Debug.Log(p2Galo.Status[i].Name);
        }
        bool canRunMove = galo1.OnBeforeMove();
        
        moveCount++;
        if (!canRunMove)
        {
            dialogueText.text = $"{galo1.nomeGalo} est� atordoado e n�o conseguiu atacar!";
            
            yield return new WaitForSeconds(1);
            if (moveCount == 2)
            {                
                state = BattleState.PlayerTurn;
                PlayerTurn();
            }
            else
            {
                StartCoroutine(UseMove(galo2, galo1));

            }
            yield break;
        }
        yield return new WaitForSeconds(2);
        var move = galo1.moves[galo1.selectedMove];
        switch (move.Name)
        {
            case ("Cabecada"): 
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                        StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                        StartCoroutine(CheckHP(galo1, galo1, move.Damage / 2));

                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                        yield return RunMoveEffects(move, galo1, galo2);
                    } else
                    {
                        dialogueText.text = $"{galo1.name} n�o possui f�lego o suficiente!";
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                    }
                    break;
                }
            case ("Mordida"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                            if (galo2.Status.Contains(ConditionDB.Conditions[ConditionID.stn]))
                            {
                            StartCoroutine(CheckHP(galo1, galo2, move.Damage * 3));
                        } else
                        {
                            StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                        }
                        
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                        yield return RunMoveEffects(move, galo1, galo2);
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.name} n�o possui f�lego o suficiente!";
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                    }
                    break;
                }
            case ("Sermao"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                        if ((galo2.currentSP - 30) < 0)
                        {
                            StartCoroutine(CheckHP(galo1, galo2, (galo2.currentSP - 30) * -10));
                        }
                        galo2.RemoveSP(30);
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                        yield return RunMoveEffects(move, galo1, galo2);
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.name} n�o possui f�lego o suficiente!";
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                    }
                    break;
                }
            case ("Punho de pedra"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                        if (galo1.tookDamage)
                        {
                            dialogueText.text = $"{galo1.nomeGalo} perdeu seu foco!";
                        } else
                        {
                            StartCoroutine(CheckHP(galo1, galo2, move.Damage)); 
                        }
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                        yield return RunMoveEffects(move, galo1, galo2);
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.name} n�o possui f�lego o suficiente!";
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                    }     
                    
                    break;
                }
            case ("Manipulacao"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                        if (galo1.Status != null)
                        {
                            StartCoroutine(CheckHP(galo1, galo2, move.Damage * 2));
                        }
                        else
                        {
                            StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                        }
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                        yield return RunMoveEffects(move, galo1, galo2);
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.name} n�o possui f�lego o suficiente!";
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }                     
                    }
                    break;
                }
           default: 
                {

                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        if (move.SpCost < 0)
                        {
                            galo1.HealSP(move.SpCost);
                        }
                        else
                        {
                            galo1.RemoveSP(move.SpCost);
                        }

                        if (move.Damage <= 0)
                        {
                            galo1.Heal(move.Damage);
                            if (moveCount == 2)
                            {
                                state = BattleState.PlayerTurn;
                                PlayerTurn();
                            }
                            else
                            {
                                StartCoroutine(UseMove(galo2, galo1));

                            }
                            yield return RunMoveEffects(move, galo1, galo2);
                        }
                        else
                        {
                            if (galo2.isParry)
                            {
                                StartCoroutine(CheckHP(galo2, galo1, move.Damage));
                                galo2.CureStatus(ConditionID.pry);
                                galo2.isParry = false;
                            }
                            else
                            {
                                StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                                yield return RunMoveEffects(move, galo1, galo2);
                            }
                        }
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.name} n�o possui f�lego o suficiente!";
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                        
                    }
                }
            break;
        } 
              
    }
    IEnumerator CheckHP(Galo galo1, Galo galo2, int dmg)
    {
        galo2.tookDamage = true;
        dmg = Mathf.FloorToInt((dmg * UnityEngine.Random.Range(0.9f, 1.1f) * (galo1.attack / galo2.guard)));
        bool isDead = galo2.TakeDamage(dmg);       
        yield return new WaitForSeconds(1 / Math.Clamp(dmg, 1, 1000));       

        if (isDead)
        {
            EndBattle(galo1);
        }
        else if (moveCount == 2)
        {
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }
        else
        {
            StartCoroutine(UseMove(galo2, galo1));

        }
    }
    void EndBattle(Galo galo1)
    {
        state = BattleState.WIN;
        dialogueText.text = $"{galo1.nomeGalo} ganhou a briga";

        return;
    }
    IEnumerator RunMoveEffects(Moves move, Galo galo1, Galo galo2)
    {
        var effects = move.Effects;
        if (effects.Status != ConditionID.none)
        {
            if (UnityEngine.Random.Range(0, 100) <= ConditionDB.Conditions[effects.Status].Percentage)
        {
            Debug.Log("worked");

            
                if (move.Target == Moves.MoveTarget.Self)
                {
                    galo1.SetStatus(effects.Status);
                    galo1.OnInflicted();
                    yield return ShowStatusChanges(galo1, effects);
                }
                else
                {
                    galo2.SetStatus(effects.Status);
                    yield return ShowStatusChanges(galo2, effects);
                }


            }
            else
            {
                Debug.Log("nah");
                yield return new WaitForSeconds(2);
            }           
        }
    }
    IEnumerator ShowStatusChanges(Galo galo, Moves.MoveEffects effects)
    {
        dialogueText.text = $"{galo.nomeGalo} {ConditionDB.Conditions[effects.Status].StartMessage}";
        yield return new WaitForSeconds(2);
    }
    
}
