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
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,4);
                },
                
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.psn]);
                    Debug.Log(galo.Status[index].Name);
                    Debug.Log(galo.Status[index].StatusTime);
                    if (galo.Status[index].StatusTime <= 0)
                    {
                        galo.CureStatus(ConditionID.psn);  
                    } else
                    {
                        Debug.Log(galo.Status[index].StatusTime);
                        galo.Status[index].StatusTime--;
                    }
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
                    Conditions[ConditionID.grd].StatusTime = 0;
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
                    if (galo.Status[index].StatusTime <= 0)
                    {
                        
                        galo.CureStatus(ConditionID.grd);
                        galo.guard -= 1;
                    } else
                    {
                        galo.Status[index].StatusTime--;
                    }
                    
                    
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
                },
                 OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.stn]);
                    if (galo.Status[index].Percentage <= 25){
                        galo.Status[index].Percentage += 25;
                    }
                    
                },
                OnBeforeMove = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.stn]);
                   galo.Status[index].Percentage -= 50;
                    
                        galo.CureStatus(ConditionID.stn);
                    return false;
                    
                }
                
            }
        },
        {
            ConditionID.nau,
            new Condition()
            {
                Name = "Nausea",
                StartMessage = "está enjoado",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.nau]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,4);
                },
                OnInflicted = (Galo galo) =>
                { bool inflicted = false;
                    if (inflicted == false){
                    galo.guard -= 0.5f;
                    galo.carSpeed -= 1;
                        inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.nau]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.nau);
                        galo.guard += 0.5f;
                        galo.carSpeed += 1;
                    } else
                    {
                        galo.Status[index].StatusTime--;
                    }


                }
            }
        },
        {
            ConditionID.wek,
            new Condition()
            {
                Name = "Fraqueza",
                StartMessage = "foi enfraquecido",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.wek]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,4);
                },
                OnInflicted = (Galo galo) =>
                { bool inflicted = false;
                    if (inflicted == false){
                    galo.attack -= 0.5f;
                        inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.wek]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.wek);
                        galo.attack += 0.5f;
                    } else
                    {
                        galo.Status[index].StatusTime--;
                    }


                }
            }
        },
        {
            ConditionID.irn,
            new Condition()
            {
                Name = "Fortalecido",
                StartMessage = "se fortaleceu",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.irn]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,4);
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
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.irn]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.irn);
                        galo.guard -= 1;
                    } else
                    {
                        galo.Status[index].StatusTime--;
                    }


                }
            }
        },
        {
            ConditionID.off,
            new Condition()
            {
                Name = "Despreparado",
                StartMessage = "perdeu sua postura",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.off]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,3);
                },
                OnInflicted = (Galo galo) =>
                { bool inflicted = false;
                    if (inflicted == false){
                    galo.guard -= 1f;
                        inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.off]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.off);
                        galo.guard += 1f;
                    } else
                    {
                        galo.Status[index].StatusTime--;
                    }


                }
            }
        },
        {
            ConditionID.pry,
            new Condition()
            {
                Name = "Preparado",
                StartMessage = "está preparando um contra-ataque",
                Percentage = 100,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.pry]);
                    galo.Status[index].StatusTime = 0;
                },
                OnInflicted = (Galo galo) =>
                { bool inflicted = false;
                    if (inflicted == false){
                    galo.isParry = true;
                        inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.pry]);
                    if (galo.Status[index].StatusTime <= 0)
                    {
                        
                        galo.CureStatus(ConditionID.pry);
                        galo.isParry= false;
                        galo.SetStatus(ConditionID.off);
                    } else
                    {
                        galo.Status[index].StatusTime--;
                    }


                }
            }
        },

    };
}
public enum ConditionID
{
   none, psn, stn, grd, nau, wek, irn, off, pry
}
