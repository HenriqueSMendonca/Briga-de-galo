using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public TextMeshProUGUI hpText, spText;
    // Start is called before the first frame update

    // Update is called once per frame
    public void SetHUD(Galo galo)
    {
        hpText.text = galo.currentHp.ToString() + "/" + galo.maxHP.ToString();
        spText.text = galo.currentSP.ToString() + "/" + galo.maxSP.ToString();
    }
    public void SetHP(int hp)
    {
       
    }
}
