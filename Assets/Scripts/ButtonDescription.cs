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
    private Button.ButtonClickedEvent button = new Button.ButtonClickedEvent();

    private void Awake()
    {
        button = gameObject.GetComponent<Button>().onClick;     
    }
    public void OnSelect(BaseEventData eventData)
    {
        if (galo.moves[move].Description != null)
        {
            description.text = galo.moves[move].Description;
        } else
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
