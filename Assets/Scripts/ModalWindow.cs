using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Load scene on click

public class ModalWindow : MonoBehaviour
{
    public string gameScene; // Nome da cena que você deseja carregar
    
    // imprimir o nome da cena no console
    private void Start()
    {
        Debug.Log(gameScene);
    }

    public void OnButtonClick()
    {
        SceneManager.LoadScene(gameScene); // Carrega a cena com o nome fornecido
        Debug.Log("Scene loaded");
        Debug.Log(gameScene);
    }
}
