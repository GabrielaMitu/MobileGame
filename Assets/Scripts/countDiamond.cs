using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class countDiamond : MonoBehaviour
{
    public static countDiamond instance; // Referência estática para o GameController

    private int diamantes = 0; // Variável para contar os diamantes

    private void Awake()
    {
        // Garante que apenas um GameController exista na cena
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Método para adicionar diamantes à contagem global
    public void GanharDiamante()
    {
        diamantes++;
        Debug.Log("Diamantes: " + diamantes);
    }

    // Método para obter a contagem atual de diamantes
    public int ObterContagemDiamantes()
    {
        return diamantes;
    }
}