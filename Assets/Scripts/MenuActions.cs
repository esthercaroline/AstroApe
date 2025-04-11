using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuActions : MonoBehaviour
{
    public void IniciaJogo()
    {
        // Limpa os valores salvos de tempo e score
        PlayerPrefs.DeleteKey("FinalTime");
        PlayerPrefs.DeleteKey("FinalScore");
        PlayerPrefs.Save();
        
        GameController.Init(); // Inicializa estado do jogo
        SceneManager.LoadScene(2); // Phase1
    }

    public void Menu()
    {
        SceneManager.LoadScene(0); // MainMenu
    }

    public void Instrucoes()
    {
        SceneManager.LoadScene(1); // Instrucoes
    }
}
