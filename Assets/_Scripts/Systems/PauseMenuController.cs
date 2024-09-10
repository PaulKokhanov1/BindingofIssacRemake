using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public float Selection;

    [Space(10)]
    [Header("Resume")]
    public GameObject ResumeSprite;
    public GameObject resumeSelected;    
    
    [Space(10)]
    [Header("Menu")]
    public GameObject MenuSprite;
    public GameObject menuSelected;    
    
    [Space(10)]
    [Header("Quit")]
    public GameObject QuitSprite;
    public GameObject quitSelected;

    private PauseMenu pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        Selection = 1f;
        pauseMenu = GetComponentInParent<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        //Managing Menu items traversal using arrow keys
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

        //Play
        if (Selection == 1)
        {
            ResumeSprite.SetActive(false);
            resumeSelected.SetActive(true);
            MenuSprite.SetActive(true);
            menuSelected.SetActive(false);            
            QuitSprite.SetActive(true);
            quitSelected.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pauseMenu.Resume();
            }
        }
                
        //Menu
        if (Selection == 2)
        {
            ResumeSprite.SetActive(true);
            resumeSelected.SetActive(false);
            MenuSprite.SetActive(false);
            menuSelected.SetActive(true);
            QuitSprite.SetActive(true);
            quitSelected.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                pauseMenu.LoadMenu();
            }

        }
                
        //Quit
        if (Selection == 3)
        {
            ResumeSprite.SetActive(true);
            resumeSelected.SetActive(false);
            MenuSprite.SetActive(true);
            menuSelected.SetActive(false);
            QuitSprite.SetActive(false);
            quitSelected.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                pauseMenu.QuitGame();
            }
        }

    }

}
