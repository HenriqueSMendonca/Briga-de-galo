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
    // Start is called before the first frame update

    // Update is called once per frame
    public void SetHUD(Galo galo)
    {
        hpText.text = galo.currentHp.ToString() + "/" + galo.maxHP.ToString();
        spText.text = galo.currentSP.ToString() + "/" + galo.maxSP.ToString();
        for (int i = 0; i < abilityText.Length; i++)
        {
            abilityText[i].text = galo.moves[i].Name;
        }
    }
    public IEnumerator SetHP(Galo galo, int dmg)
    {
       for (int i = 0; i < dmg && galo.currentHp > 0; i++)
        {
            yield return new WaitForSeconds(1 / Math.Clamp(dmg, 1, 5));
            galo.currentHp--;
            hpText.text = galo.currentHp.ToString() + "/" + galo.maxHP.ToString();
        }
        yield return new WaitForSeconds(2);
    }
}
