using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    public GameObject mainMenu;
    public GameObject optionsMenu;

    private MainMenuController mainMenuController;
    private OptionsMenuController optionsMenuController;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuController = GetComponentInChildren<MainMenuController>();
        optionsMenuController = GetComponentInChildren<OptionsMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Entering Options Menu
        if (mainMenuController.Selection == 2)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                mainMenu.SetActive(false);
                optionsMenu.SetActive(true);
            }
        }


    }
}
