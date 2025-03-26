using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galo : MonoBehaviour
{
    public int charID;
    public string nomeGalo;
    public float guard;
    public float attack;
    public int maxHP;
    public int currentHp;
    public int maxSP;
    public int currentSP;
    public List<Moves> moves;
    public int selectedMove;
    public BattleHud battleHud;
    public int StatusTime { get; set; }
    public Condition Status {  get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool TakeDamage(int dmg)
    {
        StartCoroutine(battleHud.SetHP(this ,dmg));
        Debug.Log(dmg);
        currentHp -= dmg;
        if ( currentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Heal(int dmg)
    {
        dmg *= -1;
        StartCoroutine(battleHud.HealHP(this, dmg));
        currentHp += dmg;
        if (currentHp > maxHP)
        {
            currentHp = maxHP;
        }
    }
    public void RemoveSP(int spCost)
    {
        StartCoroutine(battleHud.SetSP(this, spCost));
        currentSP -= spCost;
        if (currentSP < 0)
        {
            currentSP = 0;
        }
    }
    public void HealSP(int spHeal)
    {
        spHeal *= -1;
        StartCoroutine(battleHud.HealSP(this, spHeal));
        currentSP += spHeal;
        if (currentSP > maxSP)
        {
            currentSP = maxSP;
        }
    }
    public void SetStatus(ConditionID conditionId)
    {
        Status = ConditionDB.Conditions[conditionId];
        Status?.OnStart?.Invoke(this);
    
    }
    public void OnAfterTurn()
    {
        Status?.OnAfterTurn?.Invoke(this);
    }
    public void CureStatus()
    {
        Status = null;
    }
    public void OnInflicted()
    {
        Status?.OnInflicted?.Invoke(this);
    }
}
