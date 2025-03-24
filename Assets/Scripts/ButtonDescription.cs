using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonDescription : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Galo galo;
    public int move;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            description.text = galo.moves[move].Description;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            description.text = "";
        }
    }
}
