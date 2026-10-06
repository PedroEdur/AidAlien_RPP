using UnityEngine;
using UnityEngine.SceneManagement;

public class BootSystem : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene("Gameplay");
    }
}