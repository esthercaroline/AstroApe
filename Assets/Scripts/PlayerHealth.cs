using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int vidaMaxima = 3;
    public static int vidaAtual;

    private UIManager uiManager;
    private GameController gameController;

    void Start()
    {
        // Inicializa as vidas do jogador
        vidaAtual = vidaMaxima;
        // Procura o UIManager e GameController na cena
        uiManager = Object.FindFirstObjectByType<UIManager>();
        gameController = Object.FindFirstObjectByType<GameController>();
        uiManager.AtualizaVida(vidaAtual);
    }

    public void LevarDano(int dano)
    {
        // Reduz o número de vidas
        vidaAtual -= dano;
        Debug.Log("Jogador levou dano. Vida restante: " + vidaAtual);
        // Atualiza o indicador de vidas
        uiManager.AtualizaVida(vidaAtual);

        // Se as vidas chegarem a 0, o jogador morre
        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("Jogador morreu!");
        // Chama o GameOver do GameController para salvar o tempo
        if (gameController != null)
        {
            gameController.GameOver();
        }
        else
        {
            // Fallback caso não encontre o GameController
            PlayerPrefs.SetFloat("FinalTime", Time.timeSinceLevelLoad);
            SceneManager.LoadScene("GameOver");
        }
    }
}
