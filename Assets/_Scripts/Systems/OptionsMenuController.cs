using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
    float Selection;

    [Space(10)]
    [Header("SFX")]
    public GameObject SFXSprite;
    public GameObject sfxSelected;

    [Space(10)]
    [Header("Music")]
    public GameObject MusicSprite;
    public GameObject musicSelected;

    [Space(10)]
    [Header("Fullscreen")]
    public GameObject FullscreenSprite;
    public GameObject fullscreenSelected;    
    
    [Space(10)]
    [Header("Brightness")]
    public GameObject BrightnessSprite;
    public GameObject brightnessSelected;

    [Space(10)]
    [Header("ON_OFF")]
    public GameObject ONSprite;
    public GameObject OFFSprite;

    // Start is called before the first frame update
    void Start()
    {
        Selection = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (Selection <= 4) //3 is the number of buttons we have
            {
                Selection++;
            }
            if (Selection > 4) //3 is the number of buttons we have
            {
                Selection = 1f;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (Selection >= 1) //3 is the number of buttons we have
            {
                Selection--;
            }
            if (Selection < 1) //3 is the number of buttons we have
            {
                Selection = 4f;
            }
        }

        if (Selection == 1)
        {
            SFXSprite.SetActive(false);
            sfxSelected.SetActive(true);
            MusicSprite.SetActive(true);
            musicSelected.SetActive(false);
            FullscreenSprite.SetActive(true);
            fullscreenSelected.SetActive(false);
            BrightnessSprite.SetActive(true);
            brightnessSelected.SetActive(false);
        }

        if (Selection == 2)
        {
            SFXSprite.SetActive(true);
            sfxSelected.SetActive(false);
            MusicSprite.SetActive(false);
            musicSelected.SetActive(true);
            FullscreenSprite.SetActive(true);
            fullscreenSelected.SetActive(false);
            BrightnessSprite.SetActive(true);
            brightnessSelected.SetActive(false);
        }

        if (Selection == 3)
        {
            SFXSprite.SetActive(true);
            sfxSelected.SetActive(false);
            MusicSprite.SetActive(true);
            musicSelected.SetActive(false);
            FullscreenSprite.SetActive(false);
            fullscreenSelected.SetActive(true);
            BrightnessSprite.SetActive(true);
            brightnessSelected.SetActive(false);
        }        
        if (Selection == 4)
        {
            SFXSprite.SetActive(true);
            sfxSelected.SetActive(false);
            MusicSprite.SetActive(true);
            musicSelected.SetActive(false);
            FullscreenSprite.SetActive(true);
            fullscreenSelected.SetActive(false);
            BrightnessSprite.SetActive(false);
            brightnessSelected.SetActive(true);
        }

    }
}
