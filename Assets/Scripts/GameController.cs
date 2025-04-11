using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private float gameTime = 0f;
    private UIManager uiManager;
    private static int collectableCount = -1;
    private int score = 0;
    
    [Header("Configurações de Tempo")]
    public float intervaloBonus = 15f;    // A cada 15 segundos
    public float duracaoBonus = 5f;       // Bônus dura 5 segundos
    public float multiplicadorVelocidade = 2f; // Dobra a velocidade do inimigo

    [Header("Configurações de Score")]
    public int pontosPorBanana = 10;
    public float tempoParaBonusMaximo = 60f; // Tempo ideal para terminar (1 minuto)
    public int bonusMaximo = 100;           // Bônus máximo por terminar rápido
    
    private float proximoBonus;
    private bool bonusAtivo;
    private EnemyMovement inimigo;

    void Awake()
    {
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

    void Start()
    {
        uiManager = Object.FindFirstObjectByType<UIManager>();
        inimigo = Object.FindFirstObjectByType<EnemyMovement>();
        proximoBonus = intervaloBonus;
        score = 0;
        if (uiManager != null)
        {
            uiManager.AtualizarScore(score);
        }
    }

    void Update()
    {
        // Atualiza o tempo de jogo
        gameTime += Time.deltaTime;
        
        // Atualiza UI
        if (uiManager != null)
        {
            uiManager.AtualizarTempo();
        }

        // Sistema de bônus de velocidade para o inimigo
        if (gameTime >= proximoBonus && !bonusAtivo && inimigo != null)
        {
            AtivarBonus();
        }
    }

    private void AtivarBonus()
    {
        bonusAtivo = true;
        proximoBonus = gameTime + intervaloBonus;
        
        if (inimigo != null)
        {
            inimigo.AplicarBonus(multiplicadorVelocidade);
            Invoke("DesativarBonus", duracaoBonus);
            
            Debug.Log("Inimigo Enfurecido!");
            if (uiManager != null)
            {
                uiManager.MostrarMensagemBonus("Cuidado! Inimigo mais rápido!");
            }
        }
    }

    private void DesativarBonus()
    {
        bonusAtivo = false;
        if (inimigo != null)
        {
            inimigo.DesativarBonus();
        }
    }

    // Método estático para inicializar o jogo
    public static void Init()
    {
        collectableCount = 4; // Define quantos itens precisam ser coletados
        if (instance != null)
        {
            instance.gameTime = 0f; // Reseta o tempo do jogo
            instance.score = 0;
            if (instance.uiManager != null)
            {
                instance.uiManager.AtualizarScore(0);
                instance.uiManager.AtualizarTempo(); // Atualiza o display do tempo
            }
        }
    }

    // Método para adicionar pontos
    public void AdicionarPontos(int pontos)
    {
        score += pontos;
        if (uiManager != null)
        {
            uiManager.AtualizarScore(score);
        }
    }

    // Calcula o bônus baseado no tempo
    private int CalcularBonusTempo()
    {
        if (gameTime <= tempoParaBonusMaximo)
        {
            return bonusMaximo; // Bônus máximo se completar dentro do tempo ideal
        }
        else
        {
            // Diminui o bônus gradualmente após o tempo ideal
            float percentualBonus = Mathf.Max(0, 1 - ((gameTime - tempoParaBonusMaximo) / tempoParaBonusMaximo));
            return Mathf.RoundToInt(bonusMaximo * percentualBonus);
        }
    }

    // Método estático para coletar itens
    public static void Collect()
    {
        if (collectableCount > 0)
        {
            collectableCount--;
            // Adiciona pontos por banana
            if (instance != null)
            {
                instance.AdicionarPontos(instance.pontosPorBanana);
            }
            Debug.Log("Coletável coletado. Restam: " + collectableCount);

            if (collectableCount == 0)
            {
                // Adiciona bônus por tempo e salva score final
                if (instance != null)
                {
                    int bonusTempo = instance.CalcularBonusTempo();
                    instance.AdicionarPontos(bonusTempo);
                    PlayerPrefs.SetFloat("FinalTime", instance.gameTime);
                    PlayerPrefs.SetInt("FinalScore", instance.score);
                }
                SceneManager.LoadScene("Victory");
            }
        }
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        // Salva o tempo final e score antes de mudar de cena
        PlayerPrefs.SetFloat("FinalTime", gameTime);
        PlayerPrefs.SetInt("FinalScore", score);
        SceneManager.LoadScene("GameOver");
    }
}
