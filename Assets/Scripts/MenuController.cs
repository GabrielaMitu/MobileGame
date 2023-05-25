using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //public GameObject endPanel; // public GameObject endPanel;
    public GameObject startPanel;
    public GameObject manualPanel;
    public GameObject[] pauseUI; // index 0: button, index 1: panel
    public GameObject endPanel;
    public GameObject winPanel;

    // [SerializeField] private AudioSource menuSong;
    // [SerializeField] private AudioSource backSong;

    // Start is called before the first frame update
    void Start()
    {
        //endPanel.SetActive(false);
        startPanel.SetActive(true);
        Time.timeScale = 0;
        pauseUI[0].SetActive(false);
        manualPanel.SetActive(false);
        //menuSong.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Back()
    {
        //endPanel.SetActive(false);
        startPanel.SetActive(true);
        Time.timeScale = 0;
        pauseUI[0].SetActive(false);
        manualPanel.SetActive(false);
    }

    public void ManualPanel()
    {
        manualPanel.SetActive(true);
        startPanel.SetActive(false);
    }

    public void MenuStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 0;
        manualPanel.SetActive(false);
        //backSong.Pause();
    }
    
    public void Play()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
        pauseUI[0].SetActive(true);
        manualPanel.SetActive(false);
        //menuSong.Pause();
        //backSong.Play();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0; // tempo fica na velocidade zero, ou seja, nada acontece
        pauseUI[0].SetActive(false);
        pauseUI[1].SetActive(true);
    }

    public void UnPause()
    {
        Time.timeScale = 1;
        pauseUI[0].SetActive(true);
        pauseUI[1].SetActive(false);
    }

    public void LoseGame() 
    {
        endPanel.SetActive(true);
        pauseUI[0].SetActive(false);
    }

    public void WinGame() 
    {
        winPanel.SetActive(true);
        pauseUI[0].SetActive(false);
    }
}
