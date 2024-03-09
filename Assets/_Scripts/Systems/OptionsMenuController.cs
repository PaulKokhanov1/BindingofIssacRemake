using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
    public float Selection;

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


    [Space(10)]
    [Header("SFXBlocks")]
    public List<GameObject> OFFSFXBlocks;
    public List<GameObject> ONSFXBlocks;

    [Space(10)]
    [Header("MusicBlocks")]
    public List<GameObject> OFFMusicBlocks;
    public List<GameObject> ONMusicBlocks;    
   
    
    [Space(10)]
    [Header("BrightnessBlocks")]
    public List<GameObject> OFFBrightnessBlocks;
    public List<GameObject> ONBrightnessBlocks;

    private AudioManager audioManager;
    private Sound music;
    public List<Sound> sfx = new List<Sound>();

    // Start is called before the first frame update
    void Start()
    {
        Selection = 1f;
        audioManager = FindObjectOfType<AudioManager>();
        //Add all Music sounds HERE
        music = Array.Find(audioManager.sounds, sound => sound.name == "Background Music");
        //Add all sfx sounds HERE
        sfx.Add(Array.Find(audioManager.sounds, sound => sound.name == "Tear Shot"));
        sfx.Add(Array.Find(audioManager.sounds, sound => sound.name == "Tear Impact"));
        sfx.Add(Array.Find(audioManager.sounds, sound => sound.name == "Issac Hurt"));
        sfx.Add(Array.Find(audioManager.sounds, sound => sound.name == "Issac Dies"));
        sfx.ForEach(go => go.source.volume = 0.6f); //resetting all sfx sounds 
        drawBars(ONMusicBlocks, OFFMusicBlocks, music);
        drawBars(ONSFXBlocks, OFFSFXBlocks, sfx[0]);

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

        

        //SFX
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
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                increaseSFX();
                drawBars(ONSFXBlocks, OFFSFXBlocks, sfx[0]);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                decreaseSFX();
                drawBars(ONSFXBlocks, OFFSFXBlocks, sfx[0]);

            }
        }

        //Music
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
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                increaseVolume();
                drawBars(ONMusicBlocks, OFFMusicBlocks, music);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                decreaseVolume();
                drawBars(ONMusicBlocks, OFFMusicBlocks, music);

            }
        }

        //Fullscreen
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

            //enable fullScreen
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Screen.fullScreen = !Screen.fullScreen;
                checkFullScreen();
            }
        }    
        //Brightness
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
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                increaseBrightness();
                //drawBars(ONBrightnessBlocks, OFFBrightnessBlocks);

            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                decreaseBrightness();
                //drawBars(ONBrightnessBlocks, OFFBrightnessBlocks);

            }
        }

    }



    void checkFullScreen()
    {
        if (Screen.fullScreen)
        {
            ONSprite.SetActive(true);
            OFFSprite.SetActive(false);
        }
        else
        {
            ONSprite.SetActive(false);
            OFFSprite.SetActive(true);
        }
    }

    void drawBars(List<GameObject> ONBlocks, List<GameObject> OFFBlocks, Sound s)
    {
        ONBlocks.ForEach(go => go.SetActive(false));
        OFFBlocks.ForEach(go => go.SetActive(true));
        for (int i = 0; i < Mathf.Floor(s.source.volume * OFFBlocks.Count); i++)
        {
            OFFBlocks[i].SetActive(false);
            ONBlocks[i].SetActive(true);
        }
    }

    void increaseVolume()
    {
        float volume = music.source.volume;
        if (volume >= 1f)
        {
            music.source.volume = 1f;
            music.volume = music.source.volume;
        }
        else
        {
            music.source.volume += 0.2f;
            music.volume = music.source.volume;

        }

    }    
    
    void decreaseVolume()
    {
        float volume = music.source.volume;
        if (volume <= 0f)
        {
            music.source.volume = 0f;
            music.volume = music.source.volume;
        }
        else
        {
            music.source.volume -= 0.2f;
            music.volume = music.source.volume;

        }

    }

    private void decreaseBrightness()
    {
        throw new NotImplementedException();
    }

    private void increaseBrightness()
    {
        throw new NotImplementedException();
    }

    private void increaseSFX()
    {
        FindObjectOfType<AudioManager>().Play("Tear Shot");
        float volume = sfx[0].source.volume;    //assuming all sfx sounds will start at the same volume
        if (volume >= 1f)
        {
            sfx.ForEach(go => {
                go.source.volume = 1f;
                go.volume = go.source.volume;
            }) ;
        }
        else
        {
            sfx.ForEach(go => {
                go.source.volume += 0.2f;
                go.volume = go.source.volume;
            });
        }
    }

    private void decreaseSFX()
    {
        FindObjectOfType<AudioManager>().Play("Tear Shot");
        float volume = sfx[0].source.volume;    //assuming all sfx sounds will start at the same volume
        if (volume <= 0f)
        {
            sfx.ForEach(go => {
                go.source.volume = 0f;
                go.volume = go.source.volume;
            });
        }
        else
        {
            sfx.ForEach(go => {
                go.source.volume -= 0.2f;
                go.volume = go.source.volume;
            });
        }
    }
}
