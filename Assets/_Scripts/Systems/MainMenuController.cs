using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public float Selection;

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

        //Play
        if (Selection == 1)
        {
            StartSprite.SetActive(false);
            startSelected.SetActive(true);
            OptionsSprite.SetActive(true);
            optionsSelected.SetActive(false);            
            QuitSprite.SetActive(true);
            quitSelected.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Time.timeScale = 1f;
                PlayGame();
            }
        }
                
        //Options
        if (Selection == 2)
        {
            StartSprite.SetActive(true);
            startSelected.SetActive(false);
            OptionsSprite.SetActive(false);
            optionsSelected.SetActive(true);
            QuitSprite.SetActive(true);
            quitSelected.SetActive(false);


        }
                
        //Quit
        if (Selection == 3)
        {
            StartSprite.SetActive(true);
            startSelected.SetActive(false);
            OptionsSprite.SetActive(true);
            optionsSelected.SetActive(false);
            QuitSprite.SetActive(false);
            quitSelected.SetActive(true);

            if (Input.GetKeyDown(KeyCode.Return))
            {
                QuitGame();
            }
        }

    }

    public void PlayGame()
    {
        if(GameManager.instance != null)
        {
            Destroy(GameManager.instance.gameObject);
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
