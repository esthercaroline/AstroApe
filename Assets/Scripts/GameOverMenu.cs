using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI tempoFinalText;
    public TextMeshProUGUI mensagemText;

    void Start()
    {
        // Pega o tempo final salvo
        float tempoFinal = PlayerPrefs.GetFloat("FinalTime", 0f);
        
        // Converte para minutos e segundos
        int minutos = (int)(tempoFinal / 60f);
        int segundos = (int)(tempoFinal % 60f);
        
        // Mostra o tempo final
        if (tempoFinalText != null)
        {
            tempoFinalText.text = $"Tempo final: {minutos:00}:{segundos:00}";
        }

        // Mostra uma mensagem motivacional aleatória
        if (mensagemText != null)
        {
            string[] mensagens = new string[]
            {
                "Não desista! Tente novamente!",
                "Você consegue fazer melhor!",
                "A prática leva à perfeição!",
                "Continue tentando!"
            };
            
            mensagemText.text = mensagens[Random.Range(0, mensagens.Length)];
        }
    }

    public void TentarNovamente()
    {
        SceneManager.LoadScene("Phase1"); // Carrega a fase novamente
    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Volta para o menu principal
    }
} 