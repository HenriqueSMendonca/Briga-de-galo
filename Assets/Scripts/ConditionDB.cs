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
                    Conditions[ConditionID.psn].StatusTime = Random.Range(1,3);
                },
                
                OnAfterTurn = (Galo galo) =>
                {
                    if (Conditions[ConditionID.psn].StatusTime <= 0)
                    {
                       int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.psn]);
                        galo.CureStatus(index);  
                    }
                    Conditions[ConditionID.psn].StatusTime--;
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
                    if (Conditions[ConditionID.psn].StatusTime <= 0)
                    {
                        int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.grd]);
                        Debug.Log(index);
                        galo.CureStatus(index);
                        galo.guard -= 1;
                    }
                    Conditions[ConditionID.psn].StatusTime--;
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
                    Conditions[ConditionID.psn].StatusTime = 0;
                },
                 OnAfterTurn = (Galo galo) =>
                {
                    if (Conditions[ConditionID.stn].Percentage <= 25){
                        Conditions[ConditionID.stn].Percentage += 25;
                        Debug.Log(ConditionDB.Conditions[ConditionID.stn].Percentage);
                    }
                    Conditions[ConditionID.psn].StatusTime--;
                },
                OnBeforeMove = (Galo galo) =>
                {
                    
                   Conditions[ConditionID.stn].Percentage -= 50;
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.stn]);
                        galo.CureStatus(index);
                    Debug.Log(ConditionDB.Conditions[ConditionID.stn].Percentage);
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
