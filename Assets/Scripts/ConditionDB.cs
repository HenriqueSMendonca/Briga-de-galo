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
                  galo.TakeDamage(galo.maxHP / 6);
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
                Inflicted = false,
                OnStart = (Galo galo) =>
                {
                    Conditions[ConditionID.grd].StatusTime = 0;
                },
                OnInflicted = (Galo galo) =>
                { 
                    if (ConditionDB.Conditions[ConditionID.grd].Inflicted == false){
                    galo.guard += 1;
                        ConditionDB.Conditions[ConditionID.grd].Inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.grd]);
                    if (galo.Status[index].StatusTime <= 0)
                    {
                        
                        galo.CureStatus(ConditionID.grd);
                        ConditionDB.Conditions[ConditionID.grd].Inflicted = false;
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
                Inflicted = false,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.nau]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(2,4);
                },
                OnInflicted = (Galo galo) =>
                { 
                    if (ConditionDB.Conditions[ConditionID.nau].Inflicted == false){
                    galo.guard -= 0.5f;
                    galo.carSpeed -= 3;
                        ConditionDB.Conditions[ConditionID.nau].Inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.nau]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.nau);
                        ConditionDB.Conditions[ConditionID.nau].Inflicted = false;
                        galo.guard += 0.5f;
                        galo.carSpeed += 3;
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
                Inflicted = false,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.wek]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,4);
                },
                OnInflicted = (Galo galo) =>
                { 
                    if (ConditionDB.Conditions[ConditionID.wek].Inflicted == false){
                    galo.attack -= 0.5f;
                        ConditionDB.Conditions[ConditionID.wek].Inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.wek]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.wek);
                        ConditionDB.Conditions[ConditionID.wek].Inflicted = false;
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
                Inflicted = false,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.irn]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,4);
                },
                OnInflicted = (Galo galo) =>
                {
                    if (ConditionDB.Conditions[ConditionID.irn].Inflicted == false){
                    galo.guard += 1;
                        ConditionDB.Conditions[ConditionID.irn].Inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.irn]);
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.CureStatus(ConditionID.irn);
                        ConditionDB.Conditions[ConditionID.irn].Inflicted = false;
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
                Inflicted = false,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.off]);
                    galo.Status[index].StatusTime = UnityEngine.Random.Range(1,3);
                },
                OnInflicted = (Galo galo) =>
                {
                    if (ConditionDB.Conditions[ConditionID.off].Inflicted == false){
                    galo.guard -= 1f;
                        ConditionDB.Conditions[ConditionID.off].Inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.off]);
                    if (galo.Status[index].StatusTime <= 0)
                    {
                       
                        galo.CureStatus(ConditionID.off);
                        ConditionDB.Conditions[ConditionID.off].Inflicted = false;
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
                Inflicted = false,
                OnStart = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.pry]);
                    galo.Status[index].StatusTime = 0;
                },
                OnInflicted = (Galo galo) =>
                { 
                    if (ConditionDB.Conditions[ConditionID.pry].Inflicted == false){
                    galo.isParry = true;
                        ConditionDB.Conditions[ConditionID.pry].Inflicted = true;
                    }
                },
                OnAfterTurn = (Galo galo) =>
                {
                    int index = galo.Status.IndexOf(ConditionDB.Conditions[ConditionID.pry]);
                    if (galo.Status[index].Percentage <= 50){
                        galo.Status[index].Percentage += 50;
                    }
                    if (galo.Status[index].StatusTime <= 0)
                    {

                        galo.Status[index].Percentage -= 100;
                        galo.CureStatus(ConditionID.pry);
                        ConditionDB.Conditions[ConditionID.pry].Inflicted = false;
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
