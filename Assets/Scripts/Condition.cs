using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition
{
  public string Name {  get; set; }
 public string Description { get; set; }
    public string StartMessage { get; set; }
    public int Percentage { get; set; }
    public int StatusTime { get; set; }

    public bool Inflicted { get; set; }

    public Action<Galo> OnAfterTurn {  get; set; }
    public Action<Galo> OnInflicted { get; set; }
    public Action<Galo> OnStart { get; set; }
    
    public Func<Galo, bool> OnBeforeMove { get; set; }
}
