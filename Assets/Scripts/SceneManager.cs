using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // --------------------------
    //  CENAS
    // --------------------------

    // Ir para uma cena específica pelo nome
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Reiniciar a cena atual
    public void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // Avançar para a próxima cena na Build
    public void NextScene()
    {
        int indexAtual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(indexAtual + 1);
    }

    // Voltar para o menu
    public void BackToMenu()
    {
        SceneManager.LoadScene("StartScene"); // Substitua pelo nome real da sua cena de menu
    }

    // --------------------------
    //  BOTÕES DO MENU
    // --------------------------

    // Botão "Start"
    public void StartGame()
    {
        SceneManager.LoadScene("Jogo1"); // Substitua pelo nome da sua cena principal
    }

    // Botão "Sair"
    public void ExitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
