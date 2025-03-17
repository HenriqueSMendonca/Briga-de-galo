using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galo : MonoBehaviour
{
    public int charID;
    public string nomeGalo;
    public int maxHP;
    public int currentHp;
    public int maxSP;
    public int currentSP;
    public bool isPoisoned, isStunned;
    public int poisonedTime;
    public List<Moves> moves;
    public int selectedMove;
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
        currentHp -= dmg;
        if (currentHp <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
