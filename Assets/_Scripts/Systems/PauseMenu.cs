using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public List<GameObject> textSprites;

    private PauseMenuController pauseMenuController;
    private CameraController cameraController;


    private void Start()
    {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        pauseMenuController = GetComponentInChildren<PauseMenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        textSprites.ForEach(sprite =>
        {
            sprite.transform.position = new Vector2(cameraController.currentCameraPosition.x, cameraController.currentCameraPosition.y);
        });
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void LoadMenu()
    {
        LevelChanger.Instance.FadeToLevel(0);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
