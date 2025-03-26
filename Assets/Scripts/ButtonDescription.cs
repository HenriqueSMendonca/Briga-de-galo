using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonDescription : MonoBehaviour
{
    public TextMeshProUGUI description;
    public Galo galo;
    public int move;
    public int cost;
    private Button.ButtonClickedEvent button = new Button.ButtonClickedEvent();

    private void Awake()
    {
        button = gameObject.GetComponent<Button>().onClick;     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            description.text = galo.moves[move].Description;
            if (cost > galo.currentSP)
            {
                gameObject.GetComponent<Button>().onClick = null;
            }
            else
            {
                gameObject.GetComponent<Button>().onClick = button;
            } 
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
