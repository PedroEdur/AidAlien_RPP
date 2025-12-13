using UnityEngine;
using UnityEngine.SceneManagement;

public class CutsceneSkip : MonoBehaviour
{
    [Header("Configurações")]
    [Tooltip("Nome da cena para onde o jogo vai depois de pular a cutscene.")]
    public string proximaCena = "";

    [Tooltip("Se quiser apenas esconder o vídeo em vez de trocar de cena.")]
    public GameObject objetoCutscene; // opcional (ex: VideoPlayer, Timeline)

    // Este método é chamado ao clicar no botão
    public void PularCutscene()
    {
        Debug.Log("[CutsceneSkip] Botão 'Pular' clicado!");

        // Caso você use uma cena de cutscene separada:
        if (!string.IsNullOrEmpty(proximaCena))
        {
            SceneManager.LoadScene(proximaCena);
        }
        else
        {
            // Caso a cutscene esteja na mesma cena (por ex. vídeo no Canvas)
            if (objetoCutscene != null)
            {
                objetoCutscene.SetActive(false);
            }

            // Aqui você pode ativar o gameplay ou outro GameObject
            // Exemplo:
            // gameplay.SetActive(true);
        }
    }
}