using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDescription : MonoBehaviour, ISelectHandler
{
    public TextMeshProUGUI description;
    public Galo galo;
    public int move;
    public int cost;
    public string device;
    private Button.ButtonClickedEvent button = new Button.ButtonClickedEvent();

    private void Awake()
    {
        button = gameObject.GetComponent<Button>().onClick;     
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (galo.moves[move].Description != null && device == "keyboard")
        {
            description.text = "(<color=yellow>" + galo.moves[move].Combo + "</color>)" + galo.moves[move].Description;
        }
        else if(galo.moves[move].Description != null && device == "kap")
        {
            description.text = "(   " + galo.moves[move].ComboKap + ")" + galo.moves[move].Description;
        }
        else if (galo.moves[move].Description != null && device == "ps4")
        {
            description.text = "(   " + galo.moves[move].ComboPs4 + ")" + galo.moves[move].Description;
        }
        else
        {
            description.text = "";
        }
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
