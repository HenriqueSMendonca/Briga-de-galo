using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PlayerTurn, CarRace, Comandos ,WIN }

public class BattleSystem : MonoBehaviour
{
    Carro carro1, carro2;
    CursorControls cursor1, cursor2;
    ActionCommands command1, command2;
    public PlayerInputManager playerManager;
    public TextMeshProUGUI dialogueText, commandInputs1, commandInputs2;
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
    public bool whoWonRace, p1Input, p2Input;
    private int moveCount;
    private GameObject pistache;
    public GameObject menu1, menu2, desc1, desc2;
    public GameObject commandBox1, commandBox2;
    public BattleHud p1HUD, p2HUD;
    public GameObject endScreen;


    private void OnEnable()
    {
        ActionCommands.commandCheck += MoveCheck;
    }
    private void OnDisable()
    {
        ActionCommands.commandCheck -= MoveCheck;
    }
    void Start()
    {      
        endScreen.SetActive(false);
        cnvs.gameObject.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        if (p1Input)
        {
            commandInputs1.text = command1.inputString;
        }
        if (p2Input)
        {
            commandInputs2.text = command2.inputString;
        }
        if (playerManager.playerCount == playerManager.maxPlayerCount && roomFull == false)
        {
            roomFull = true;
            playerManager.DisableJoining();
            StartTheGame();
        }
    }
    void StartTheGame()
    {
        commandBox1.SetActive(false);
        commandBox2.SetActive(false);
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
        p1Galo.battleHud = p1HUD;
        p2HUD.SetHUD(p2Galo);
        p2Galo.battleHud = p2HUD;
        yield return new WaitForSeconds(3);

        players[0] = GameObject.Find("P1");

        players[1] = GameObject.Find("Galo1");

        players[2] = GameObject.Find("P2");

        players[3] = GameObject.Find("Galo2");

        players[4] = GameObject.Find("Text1");

        players[5] = GameObject.Find("Text2");

        cursor1 = players[0].GetComponent<CursorControls>();
        carro1 = players[1].GetComponent<Carro>();
        cursor2 = players[2].GetComponent<CursorControls>();
        carro2 = players[3].GetComponent<Carro>();
        command1 = players[4].GetComponent<ActionCommands>();
        command2 = players[5].GetComponent<ActionCommands>();

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        if (p1Galo.currentHp <= 0)
        {
            EndBattle(p1Galo);
        } else if (p2Galo.currentHp <= 0)
        {
            EndBattle(p2Galo);
        }

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
        desc1.SetActive(true);
        desc2.SetActive(true);
        dialogueText.text = "Selecione uma ação!";
    }
    void CarRace()
    {
        p1Decided = false;
        p2Decided = false;
        p1HUD.gameObject.SetActive(false);
        p2HUD.gameObject.SetActive(false);
        menu1.SetActive(false);
        menu2.SetActive(false);
        desc1.SetActive(false);
        desc2.SetActive(false);
        dialogueText.text = "Chegue até o final";
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
            StartCoroutine(CheckAttack(p1Galo, p2Galo));


        }
        else
        {
            dialogueText.text = "O jogador 2 ganhou a prioridade!";
            StartCoroutine(CheckAttack(p2Galo, p1Galo));
        }
    }

    public void Action1(int actionNum)
    {
        menu1.SetActive (false);
        desc1.SetActive (false);
        p1Galo.selectedMove = actionNum;
        p1Decided = true;
        carro1.acceleration += p1Galo.moves[p1Galo.selectedMove].Priority;
        Debug.Log(carro1.acceleration);
        if (p1Decided && p2Decided)
        {

            state = BattleState.CarRace;
            CarRace();
        }

    }
    public void Action2(int actionNum)
    {
        menu2.SetActive(false);
        desc2.SetActive(false);
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
        players[1].GetComponent<SpriteRenderer>().enabled = !players[1].GetComponent<SpriteRenderer>().enabled;
        carro1.inputEnabled = !carro1.inputEnabled;
        players[3].GetComponent<SpriteRenderer>().enabled = !players[3].GetComponent<SpriteRenderer>().enabled;
        carro2.inputEnabled = !carro2.inputEnabled;
    }

    IEnumerator CheckAttack(Galo galo1, Galo galo2)
    { 
        if (galo1.moves[galo1.selectedMove].Name == "Ataque")
        {

            dialogueText.text = $"{galo1.nomeGalo} está preparando um ataque";
            yield return new WaitForSeconds(1);
            if (galo1 == p1Galo)
            {
                p1Input = true;
                command1.inputEnabled = true;
                commandBox1.SetActive(true);
            }
            else
            {
                p2Input = true;
                command2.inputEnabled = true;
                commandBox2.SetActive(true);
            }

        }
        if (galo2.moves[galo2.selectedMove].Name == "Ataque")
        {

            dialogueText.text = $"{galo2.nomeGalo} está preparando um ataque";
            yield return new WaitForSeconds(1);
            if (galo2 == p1Galo)
            {
                p1Input = true;
                command1.inputEnabled = true;
                commandBox1.SetActive(true);
            }
            else
            {
                p2Input = true;
                command2.inputEnabled = true;
                commandBox2.SetActive(true);
            }
            

        }
        for (int i = 10; i > 0 && (galo1.moves[galo1.selectedMove].Name == "Ataque" || galo2.moves[galo2.selectedMove].Name == "Ataque"); i--)
        {
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        p1Input = false;
        command1.inputEnabled = false;
        commandBox1.SetActive(false);
        command1.inputString = null;
        p2Input = false;
        command2.inputEnabled = false;
        commandBox2.SetActive(false);
        command2.inputString = null;
        StartCoroutine(UseMove(galo1, galo2));
    }
    IEnumerator UseMove(Galo galo1, Galo galo2)
    {
        bool canRunMove = galo1.OnBeforeMove();


        moveCount++;
        if (!canRunMove)
        {
            dialogueText.text = $"{galo1.nomeGalo} está atordoado e não conseguiu atacar!";

            yield return new WaitForSeconds(1);
            if (moveCount == 2)
            {
                state = BattleState.PlayerTurn;
                PlayerTurn();
            }
            else
            {
                CheckAttack(galo1, galo2);
                StartCoroutine(UseMove(galo2, galo1));

            }
            yield break;
        }       
        yield return new WaitForSeconds(2);
        var move = galo1.moves[galo1.selectedMove];
        switch (move.Name)
        {
            case ("Cabeçada"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                        if (galo2.isParry)
                        {
                            StartCoroutine(CheckHP(galo2, galo1, move.Damage));
                            galo2.CureStatus(ConditionID.pry);
                            galo2.isParry = false;
                        }
                        else
                        {
                            StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                            StartCoroutine(Recoil(galo1, move.Damage / 2));
                            yield return RunMoveEffects(move, galo1, galo2);
                        }

                    }
                    else
                    {
                        dialogueText.text = $"{galo1.nomeGalo} não possui fôlego o suficiente!";
                        yield return new WaitForSeconds(2);
                        if (moveCount == 2)
                        {
                            Debug.Log("dude");
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                            break;
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
                        if (galo2.isParry)
                        {
                            StartCoroutine(CheckHP(galo2, galo1, move.Damage));
                            galo2.CureStatus(ConditionID.pry);
                            galo2.isParry = false;
                        }
                        else
                        {
                            if (galo2.Status.Contains(ConditionDB.Conditions[ConditionID.stn]))
                            {
                                StartCoroutine(CheckHP(galo1, galo2, move.Damage * 3));
                            }
                            else
                            {
                                StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                            }
                            yield return RunMoveEffects(move, galo1, galo2);
                        }
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.nomeGalo} não possui fôlego o suficiente!";
                        yield return new WaitForSeconds(2);
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                            break;
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                    }
                    break;
                }
            case ("Sermão"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);
                        galo1.RemoveSP(move.SpCost);
                        if ((galo2.currentSP - 30) < 0)
                        {
                            StartCoroutine(CheckHP(galo1, galo2, (galo2.currentSP - 30) * -10));
                            galo2.RemoveSP(30);
                            yield return RunMoveEffects(move, galo1, galo2);
                        }
                        else
                        {
                            galo2.RemoveSP(30);
                            yield return RunMoveEffects(move, galo1, galo2);
                            if (moveCount == 2)
                            {
                                state = BattleState.PlayerTurn;
                                PlayerTurn();
                                break;
                            }
                            else
                            {
                                StartCoroutine(UseMove(galo2, galo1));

                            }
                        }


                    }
                    else
                    {
                        dialogueText.text = $"{galo1.nomeGalo} não possui fôlego o suficiente!";
                        yield return new WaitForSeconds(2);
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                            break;
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
                            dialogueText.text = $"{galo1.nomeGalo} perdeu seu foco e errou!";
                            yield return new WaitForSeconds(2);
                            if (moveCount == 2)
                            {
                                state = BattleState.PlayerTurn;
                                PlayerTurn();
                                break;
                            }
                            else
                            {
                                StartCoroutine(UseMove(galo2, galo1));

                            }
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
                        dialogueText.text = $"{galo1.nomeGalo} não possui fôlego o suficiente!";
                        yield return new WaitForSeconds(2);
                        if (moveCount == 2)
                        {
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                            break;
                        }
                        else
                        {
                            StartCoroutine(UseMove(galo2, galo1));

                        }
                    }

                    break;
                }
            case ("Manipulação"):
                {
                    if (galo1.currentSP >= move.SpCost)
                    {
                        dialogueText.text = $"{galo1.nomeGalo} usou {move.Name}!";
                        yield return new WaitForSeconds(2);

                        galo1.RemoveSP(move.SpCost);
                        if (galo2.isParry)
                        {
                            StartCoroutine(CheckHP(galo2, galo1, move.Damage));
                            galo2.CureStatus(ConditionID.pry);
                            galo2.isParry = false;
                        }
                        else
                        {
                            if (galo1.Status != null)
                            {
                                StartCoroutine(CheckHP(galo1, galo2, move.Damage * 2));
                            }
                            else
                            {
                                StartCoroutine(CheckHP(galo1, galo2, move.Damage));
                            }
                            yield return RunMoveEffects(move, galo1, galo2);
                        }
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.nomeGalo} não possui fôlego o suficiente!";
                        yield return new WaitForSeconds(2);
                        if (moveCount == 2)
                        {
                            Debug.Log("dude");
                            state = BattleState.PlayerTurn;
                            PlayerTurn();
                            break;
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
                            yield return RunMoveEffects(move, galo1, galo2);
                            if (moveCount == 2)
                            {

                                state = BattleState.PlayerTurn;
                                PlayerTurn();
                                break;
                            }
                            else
                            {
                                StartCoroutine(UseMove(galo2, galo1));

                            }

                        }
                        else
                        {
                            if (galo2.isParry)
                            {
                                StartCoroutine(CheckHP(galo2, galo1, move.Damage));
                                galo2.CureStatus(ConditionID.pry);
                                galo2.isParry = false;
                                yield return RunMoveEffects(move, galo1, galo2);
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
                        dialogueText.text = $"{galo1.nomeGalo} não possui fôlego o suficiente!";
                        yield return new WaitForSeconds(2);
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
    IEnumerator Recoil(Galo galo1, int dmg)
    {
        galo1.tookDamage = true;
        dmg = Mathf.FloorToInt((dmg * UnityEngine.Random.Range(0.9f, 1.1f) * (galo1.attack / galo1.guard)));
        bool isDead = galo1.TakeDamage(dmg);
        yield return new WaitForSeconds(1 / Math.Clamp(dmg, 1, 1000));
        if (isDead)
        {
            EndBattle(galo1);
        }
    }
    void EndBattle(Galo galo1)
    {
        state = BattleState.WIN;
        p1HUD.gameObject.SetActive(false);
        p2HUD.gameObject.SetActive(false);
        endScreen.SetActive(true);      
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
                    if (!galo1.Status.Contains(ConditionDB.Conditions[effects.Status]))
                    {
                        galo1.SetStatus(effects.Status);
                        galo1.OnInflicted();
                        yield return ShowStatusChanges(galo1, effects);
                    }
                    else
                    {
                        dialogueText.text = $"{galo1.nomeGalo} já possui esse efeito de status";
                        yield return new WaitForSeconds(2);
                    }
                }
                else
                {
                    if (!galo2.Status.Contains(ConditionDB.Conditions[effects.Status]))
                    {
                        galo2.SetStatus(effects.Status);
                        galo2.OnInflicted();
                        yield return ShowStatusChanges(galo2, effects);
                    }
                    else
                    {
                        dialogueText.text = $"{galo2.nomeGalo} já possui esse efeito de status";
                        yield return new WaitForSeconds(2);
                    }
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

    public void Revanche()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Sair()
    {
        SceneManager.LoadScene("Main menu");
    }
    public void MoveCheck()
    {
        
        if (command1.inputEnabled == true)
        {
            for (int i = 0; i < p1Galo.moves.Count; i++)
            {
                if (command1.inputString == p1Galo.moves[i].Combo)
                {
                    p1Galo.selectedMove = i;
                    p1Input = false;
                    command1.inputEnabled = false;
                    commandBox1.SetActive(false);
                    command1.inputString = null;
                }
            }

           
        }
          if (command2.inputEnabled == true)
        {

            for (int i = 0; i < p2Galo.moves.Count; i++)
            {
                if (command2.inputString == p2Galo.moves[i].Combo)
                {
                    p2Galo.selectedMove = i;
                    p2Input = false;
                    command2.inputEnabled = false;
                    commandBox2.SetActive(false);
                    command2.inputString = null;

                }
            }
            
        }
    }
}
