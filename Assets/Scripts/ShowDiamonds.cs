using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowDiamonds : MonoBehaviour
{
    public TextMeshProUGUI textoContagemDiamantes;

    private void Start()
    {
        // Obtém a contagem de diamantes do GameController
        int contagemDiamantes = countDiamond.instance.ObterContagemDiamantes();

        // Atualiza o texto para exibir a contagem de diamantes
        textoContagemDiamantes.text = ": " + contagemDiamantes.ToString();
    }
}
