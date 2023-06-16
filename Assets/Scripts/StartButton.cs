using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject panel; // Referência ao painel que você deseja mostrar

    private void Start()
    {
        panel.SetActive(false); // Certifique-se de que o painel esteja desativado no início
    }

    public void OnStartButtonClicked()
    {
        // Se ja estiver ativo, desative-o
        if (panel.activeSelf)
        {
            panel.SetActive(false);
            return;
        }
        panel.SetActive(true); // Ative o painel quando o botão Start for pressionado
    }
}