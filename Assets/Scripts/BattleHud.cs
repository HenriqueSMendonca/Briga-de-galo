using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI hpText, spText;
    public TextMeshProUGUI[] abilityText;
    public ButtonDescription[] descriptions;
    // Start is called before the first frame update

    // Update is called once per frame
    public void SetHUD(Galo galo)
    {
        hpText.text = galo.currentHp.ToString() + "/" + galo.maxHP.ToString();
        spText.text = galo.currentSP.ToString() + "/" + galo.maxSP.ToString();
        for (int i = 0; i < abilityText.Length; i++)
        {
            abilityText[i].text = galo.moves[i + 2].Name;
        }
        for (int i = 0; i < descriptions.Length; i++)
        {
            descriptions[i].galo = galo;
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
}
