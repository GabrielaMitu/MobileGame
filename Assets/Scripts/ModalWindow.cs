using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Load scene on click

public class ModalWindow : MonoBehaviour
{
    public string gameScene; // Nome da cena que você deseja carregar
    // public GameObject MenuSpeed;

    // imprimir o nome da cena no console
    private void Start()
    {
        Debug.Log(gameScene);
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
