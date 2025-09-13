using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.InputSystem.XInput;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI hpText, spText;
    public TextMeshProUGUI[] abilityText;
    public ButtonDescription[] descriptions;
    public TextMeshProUGUI[] abilityHelp;
    public GameObject[] abilityIcons; 
    private PlayerInput player;
    // Start is called before the first frame update

    // Update is called once per frame

    public void SetHUD(Galo galo)
    {
            if (this.gameObject.name == "P1UI")
            {
                player = GameObject.Find("P1").GetComponent<PlayerInput>();
            }
            else if (this.gameObject.name == "P2UI")
            {
            if (GameObject.Find("P2") != null)
            {
                player = GameObject.Find("P2").GetComponent<PlayerInput>();
            } else
            {
                player = GameObject.Find("PlayerInput").GetComponent<PlayerInput>();
            }
            }

        hpText.text = galo.currentHp.ToString() + "/" + galo.maxHP.ToString();
        spText.text = galo.currentSP.ToString() + "/" + galo.maxSP.ToString();
        for (int i = 0; i < abilityText.Length; i++)
        {
            abilityText[i].text = galo.moves[i + 2].Name;
            if (player.devices[0].name == "Keyboard")
            {
                abilityHelp[i].text = galo.moves[i + 2].Name;
                abilityIcons[i].GetComponent<TextMeshProUGUI>().text = "<color=yellow>" + galo.moves[i + 2].Combo + "</color>";


            }
            else if (player.devices[0].name == "DualShock4GamepadHID")
            {
                abilityHelp[i].text = galo.moves[i + 2].Name;
                for (int j = 0; j < galo.moves[i + 2].ComboKap.Count; j++)
                {
                    Instantiate(galo.moves[i + 2].ComboPs4[j], abilityIcons[i].gameObject.transform);
                    

                }
            }
            else if (player.devices[0].name == "XInputControllerWindows")
            {
                abilityHelp[i].text = galo.moves[i + 2].Name;
                for (int j = 0; j < galo.moves[i + 2].ComboKap.Count; j++)
                {
                    Instantiate(galo.moves[i + 2].ComboKap[j], abilityIcons[i].gameObject.transform);
                }
            }
        }
        for (int i = 0; i < descriptions.Length; i++)
        {          
            descriptions[i].galo = galo;
            //descriptions[i].cost = descriptions[i].galo.moves[i].SpCost;
        }
    }
    public IEnumerator SetHP(Galo galo, int dmg)
    {
        
        int fakeHP = galo.currentHp;
        for (int i = 0; i < dmg && fakeHP > 0; i++)
        {
            
            yield return new WaitForSeconds(1 / Math.Clamp(dmg, 1f, 1000));
            fakeHP--;
            hpText.text = fakeHP.ToString() + "/" + galo.maxHP.ToString();
        }
        yield return new WaitForSeconds(0);
    }
    public IEnumerator HealHP(Galo galo, int dmg)
    {       
        int fakeHP = galo.currentHp;
        for (int i = 0; i < dmg && fakeHP < galo.maxHP; i++)
        {
            yield return new WaitForSeconds(1 / Math.Clamp(dmg, 1f, 1000));
            fakeHP++;
            hpText.text = fakeHP.ToString() + "/" + galo.maxHP.ToString();
        }
    }

    public IEnumerator SetSP(Galo galo, int sp)
    {
        int fakeSP = galo.currentSP;
        for (int i = 0; i < sp && fakeSP > 0; i++)
        {
            yield return new WaitForSeconds(1 / Math.Clamp(sp, 1f, 1000));
            fakeSP--;
            spText.text = fakeSP.ToString() + "/" + galo.maxSP.ToString();
        }
    }
    public IEnumerator HealSP(Galo galo, int sp)
    {
        int fakeSP = galo.currentSP;
        for (int i = 0; i < sp && fakeSP < galo.maxSP; i++)
        {
            yield return new WaitForSeconds(1 / Math.Clamp(sp, 1f, 1000));
            fakeSP++;
            spText.text = fakeSP.ToString() + "/" + galo.maxSP.ToString();
        }
    }
}
