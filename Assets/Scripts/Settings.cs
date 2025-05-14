using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public UnityEngine.UI.Slider musica, efeitos;
    public TextMeshProUGUI mPercent, ePercent;
    public AudioSource mAudioSource;

    // Update is called once per frame
    void Update()
    {
        
        
        mPercent.text = $"{Mathf.RoundToInt(musica.value * 100)}%";
        ePercent.text = $"{Mathf.RoundToInt(efeitos.value * 100)}%";
        PlayerPrefs.SetFloat("Musica", musica.value);
        PlayerPrefs.SetFloat("Efeitos", efeitos.value);

        mAudioSource.volume = PlayerPrefs.GetFloat("Musica");
    }
}
