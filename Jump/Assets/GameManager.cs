using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public Button startButton;
    [SerializeField] public GameObject startUI;
    [SerializeField] public GameObject pauseUI;
    [SerializeField] public GameObject endUI;
    [SerializeField] public Transform playerPos;
    private Vector3 startPos;
    bool isPause = false;
    [SerializeField] [Range(0f, 5f)] public float playTimeScale;
    
    private void Start()
    {
        Time.timeScale = playTimeScale;
        startPos = new Vector3(-8.0f, -3.0f, 0.0f);
        GameStart();
    }
    private void Update()
    {
        GamePause();
    }
    public void GameStart()
    {
        Time.timeScale = 0;
        startUI.SetActive(true);
        pauseUI.SetActive(false);
        endUI.SetActive(false);
    }

    public void onClickedStartButton()
    {
        Time.timeScale = playTimeScale;
        isPause = false;
        pauseUI.SetActive(false);
        startUI.SetActive(false);
        endUI.SetActive(false);
    }

    public void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                isPause = true;
                ActiveSettingUI();
                return;
            }
            else
            {
                onClickedStartButton();
                return;
            }
        }
    }

    public void GameRestart()
    {
        playerPos.position = startPos;
        Camera.main.GetComponent<CameraController>().enabled=true;
        onClickedStartButton();
    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void ActiveSettingUI()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        startUI.SetActive(false);
        endUI.SetActive(false);
    }

    public void ActiveEndUI()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(false);
        startUI.SetActive(false);
        endUI.SetActive(true);
    }
}
