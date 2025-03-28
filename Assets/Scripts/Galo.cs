using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Galo : MonoBehaviour
{
    public int charID;
    public string nomeGalo;
    public float guard;
    public float attack;
    public float carSpeed;
    public int maxHP;
    public int currentHp;
    public int maxSP;
    public int currentSP;
    public bool isParry;
    public bool tookDamage;
    public List<Moves> moves;
    public int selectedMove;
    public BattleHud battleHud;
    public List<Condition> Status = new List<Condition>();
    public bool TakeDamage(int dmg)
    {
        StartCoroutine(battleHud.SetHP(this ,dmg));
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
            Status.Add(ConditionDB.Conditions[conditionId]);      
        for (int i = 0; Status?.Count > i; i++)
        {
            Status[i]?.OnStart?.Invoke(this);
        }
    
    }
    public void OnAfterTurn()
    {
        for (int i = 0; Status?.Count > i; i++)
        {
            Status[i]?.OnAfterTurn?.Invoke(this);
        }
    }
    public void CureStatus(ConditionID cond)
    {
        for (int i = 0; i < Status.Count; i++)
        {
            if (Status[i] == ConditionDB.Conditions[cond])
            {
                Debug.Log($"{Status[i].Name} foi curado");
                Status.RemoveAt(i);
            }
        }
        
        
    }
    public void OnInflicted()
    {
        for (int i = 0; Status?.Count > i; i++)
        {
            Status[i]?.OnInflicted?.Invoke(this);
        }
    }
    public bool OnBeforeMove()
    {
        for (int i = 0; Status?.Count > i; i++)
        {
            if (Status[i]?.OnBeforeMove != null)
            {
                return Status[i].OnBeforeMove(this);
            }
        }
        return true;
    }
}
