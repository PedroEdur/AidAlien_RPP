using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void TentarNovamente()
    {
        SceneManager.LoadScene("Gameplay");
    }
}