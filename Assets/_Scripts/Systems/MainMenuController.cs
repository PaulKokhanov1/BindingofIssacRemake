using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    float Selection;

    [Space(10)]
    [Header("Start")]
    public GameObject StartSprite;
    public GameObject startSelected;    
    
    [Space(10)]
    [Header("Options")]
    public GameObject OptionsSprite;
    public GameObject optionsSelected;    
    
    [Space(10)]
    [Header("Quit")]
    public GameObject QuitSprite;
    public GameObject quitSelected;
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
            if (Selection <= 3) //3 is the number of buttons we have
            {
                Selection++;
            }
            if (Selection > 3) //3 is the number of buttons we have
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
                Selection = 3f;
            }
        }

        if (Selection == 1)
        {
            StartSprite.SetActive(false);
            startSelected.SetActive(true);
            OptionsSprite.SetActive(true);
            optionsSelected.SetActive(false);            
            QuitSprite.SetActive(true);
            quitSelected.SetActive(false);
        }
                
        if (Selection == 2)
        {
            StartSprite.SetActive(true);
            startSelected.SetActive(false);
            OptionsSprite.SetActive(false);
            optionsSelected.SetActive(true);
            QuitSprite.SetActive(true);
            quitSelected.SetActive(false);
        }
                
        if (Selection == 3)
        {
            StartSprite.SetActive(true);
            startSelected.SetActive(false);
            OptionsSprite.SetActive(true);
            optionsSelected.SetActive(false);
            QuitSprite.SetActive(false);
            quitSelected.SetActive(true);
        }

    }
}
