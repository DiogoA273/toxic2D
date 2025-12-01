using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class MenuController : MonoBehaviour
{
    // Chamado pelo botão "Start"
    public void StartGame()
    {
        // Troca para a cena do jogo (substitua "Jogo" pelo nome da sua cena)
        SceneManager.LoadScene("Jogo1");
    }

    // Chamado pelo botão "Sair"
    public void ExitGame()
    {
        // Fecha o jogo ao ser compilado
        Application.Quit();

        // Apenas para debug dentro do Editor da Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
