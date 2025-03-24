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
                OnStart = (Galo galo) =>
                {
                    galo.StatusTime = Random.Range(1,3);
                },
                
                OnAfterTurn = (Galo galo) =>
                {
                    if (galo.StatusTime <= 0)
                    {
                        galo.CureStatus();  
                    }
                    galo.StatusTime--;
                  galo.TakeDamage(galo.maxHP / 5);
                }
            }
        },
        {
            ConditionID.grd,
            new Condition()
            {
                Name = "Defendendo",
                StartMessage = "est� defendendo",
                OnInflicted = (Galo galo) =>
                {
                    galo.guard += 0.25f;
                }
            }
        },
        {
            ConditionID.stn,
            new Condition()
            {
                Name = "Atordoado",
                StartMessage = "foi atordoado",
                OnInflicted = (Galo galo) =>
                {
                  
                },
                 OnAfterTurn = (Galo galo) =>
                {
                    if (galo.StatusTime <= 0)
                    {
                        galo.CureStatus();
                    }
                    galo.StatusTime--;
                }
            }
        }
        
    };
}
public enum ConditionID
{
   none, psn, stn, grd, nau, wek, irn, off, pry
}
