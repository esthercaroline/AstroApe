using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VictoryMenu : MonoBehaviour
{
    public TextMeshProUGUI tempoFinalText;
    public TextMeshProUGUI scoreFinalText;

    void Start()
    {
        // Pega o tempo final salvo
        float tempoFinal = PlayerPrefs.GetFloat("FinalTime", 0f);
        int scoreFinal = PlayerPrefs.GetInt("FinalScore", 0);
        
        Debug.Log($"Score recuperado: {scoreFinal}"); // Debug para verificar o score
        
        // Converte para minutos e segundos
        int minutos = (int)(tempoFinal / 60f);
        int segundos = (int)(tempoFinal % 60f);
        
        // Mostra o tempo final
        if (tempoFinalText != null)
        {
            tempoFinalText.text = $"Seu tempo: {minutos:00}:{segundos:00}";
        }
        else
        {
            Debug.LogError("tempoFinalText não está configurado!");
        }

        // Mostra o score final
        if (scoreFinalText != null)
        {
            // Forçar atualização do texto e garantir que está visível
            scoreFinalText.gameObject.SetActive(true);
            scoreFinalText.text = "Seu score: " + scoreFinal.ToString();
            scoreFinalText.enabled = true;
            Debug.Log($"Texto do score definido como: {scoreFinalText.text}"); // Debug para verificar o texto
            
            // Forçar atualização do componente
            scoreFinalText.ForceMeshUpdate();
        }
        else
        {
            Debug.LogError("scoreFinalText não está configurado!");
        }
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene(0); // MainMenu
    }
} 