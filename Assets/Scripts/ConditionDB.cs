using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionDB
{
    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn,
            new Condition()
            {
                Name = "Veneno",
                StartMessage = "foi envenenado",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.psn]);
                        Debug.Log(index);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,3);
                },
                
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.psn]);
                    if (Conditions[ConditionID.psn].StatusTime <= 0)
                    {
                        galo.CureStatus(index);  
                    }
                    galo.Status[index].StatusTime--;
                  galo.TakeDamage(galo.maxHP / 5);
                }
            }
        },
        {
            ConditionID.grd,
            new Condition()
            {
                Name = "Defendendo",
                StartMessage = "está defendendo",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    Conditions[ConditionID.psn].StatusTime = 0;
                },
                OnInflicted = (Galo galo) =>
                { bool inflicted = false;
                    if (inflicted == false){
                    galo.guard += 1;
                        inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.grd]);
                        Debug.Log(index);
                    if (galo.Status[index].StatusTime <= 0)
                    {
                        
                        galo.CureStatus(index);
                        galo.guard -= 1;
                    }
                    galo.Status[index].StatusTime--;
                }
            }
        },
        {
            ConditionID.stn,
            new Condition()
            {
                Name = "Atordoado",
                StartMessage = "foi atordoado",
                Percentage = 50,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.stn]);
                    galo.Status[index].StatusTime = 0;
                },
                 OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.stn]);
                    if (galo.Status[index].Percentage <= 25){
                        galo.Status[index].Percentage += 25;                        
                    }
                    Conditions[ConditionID.psn].StatusTime--;
                },
                OnBeforeMove = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.stn]);
                   galo.Status[index].Percentage -= 50;
                    
                        galo.CureStatus(index);
                    return false;
                    
                }
                
            }
        }
        
    };
}
public enum ConditionID
{
   none, psn, stn, grd, nau, wek, irn, off, pry
}
