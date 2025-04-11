using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI vidasText;    // Referência ao texto de vidas
    public TextMeshProUGUI tempoText;    // Referência ao texto de tempo
    public TextMeshProUGUI scoreText;    // Referência ao texto de score
    public TextMeshProUGUI mensagemBonusText; // Referência ao texto de bônus

    private float tempoDeJogo = 0f;

    void Start()
    {
        if (vidasText == null || tempoText == null || scoreText == null)
        {
            Debug.LogError("UI Text components não configurados no UIManager!");
            enabled = false;
            return;
        }

        AtualizaVida(PlayerHealth.vidaAtual);
        AtualizarTempo();
        AtualizarScore(0);
        
        // Inicializa o texto de bônus invisível
        if (mensagemBonusText != null)
        {
            mensagemBonusText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        tempoDeJogo += Time.deltaTime;
        AtualizarTempo();
    }

    public void AtualizarTempo()
    {
        if (tempoText != null)
        {
            int minutos = (int)(tempoDeJogo / 60f);
            int segundos = (int)(tempoDeJogo % 60f);
            tempoText.text = $"Tempo: {minutos:00}:{segundos:00}";
        }
    }

    public void AtualizarScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score}";
        }
    }

    public void AtualizaVida(int vidaAtual)
    {
        if (vidasText != null)
        {
            vidasText.text = $"Vidas: {vidaAtual}";
        }
    }

    public void MostrarMensagemBonus(string mensagem)
    {
        if (mensagemBonusText != null)
        {
            mensagemBonusText.text = mensagem;
            mensagemBonusText.gameObject.SetActive(true);
            
            // Esconde a mensagem após 2 segundos
            Invoke("EsconderMensagemBonus", 2f);
        }
    }

    private void EsconderMensagemBonus()
    {
        if (mensagemBonusText != null)
        {
            mensagemBonusText.gameObject.SetActive(false);
        }
    }

    public float GetTempoFinal()
    {
        return tempoDeJogo;
    }
}
