using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Load scene on click

public class ModalWindow : MonoBehaviour
{
    private int diamantes;
    public TextMeshProUGUI diamantesText;

    public string gameScene; // Nome da cena que você deseja carregar
    // public GameObject MenuSpeed;

    // imprimir o nome da cena no console
    private void Start()
    {
        // diamantes = 1; // Set your desired starting value here
        // PlayerPrefs.SetInt("diamantes", diamantes);
        Debug.Log(gameScene);
        if (PlayerPrefs.HasKey("diamantes"))
        {
            diamantes = PlayerPrefs.GetInt("diamantes");
        }
        else
        {
            Debug.Log("No diamantes key");
            diamantes = 1; // Set your desired starting value here
            PlayerPrefs.SetInt("diamantes", diamantes);
        }
        diamantesText.text = diamantes.ToString();
    }

    public void OnButtonClick0()
    {
        PlayerPrefs.SetString("Music", "demo");
        PlayerPrefs.SetString("Speed","1");
        SceneManager.LoadScene(gameScene); // Carrega a cena com o nome fornecido

    }

    public void OnButtonClick1()
    {
        PlayerPrefs.SetString("Music", "mozart");
        PlayerPrefs.SetString("Speed","1");
        SceneManager.LoadScene(gameScene); // Carrega a cena com o nome fornecido

    }

    public void OnButtonClick2()
    {
        PlayerPrefs.SetString("Music", "mozart2");
        PlayerPrefs.SetString("Speed","1");
        SceneManager.LoadScene(gameScene); // Carrega a cena com o nome fornecido

    }
    
    public void OnButtonClick3()
    {
        PlayerPrefs.SetString("Music", "classica1");
        PlayerPrefs.SetString("Speed","1");
        SceneManager.LoadScene(gameScene); // Carrega a cena com o nome fornecido

    }

    // public void Speed1()
    // {
    //     PlayerPrefs.SetString("Speed","1");
    //     SceneManager.LoadScene(gameScene); // Carrega a cena com o nome fornecido
    //     Debug.Log("Scene loaded 1");
    //     Debug.Log(gameScene);
    // }
}
