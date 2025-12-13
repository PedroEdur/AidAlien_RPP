using UnityEngine;
using UnityEngine.SceneManagement;

public class OlhoDoAbitante : MonoBehaviour
{
    public bool avistamento = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);

        if (col.gameObject.CompareTag("Player"))
        {
            avistamento = true;
            Debug.Log("Viu um ET...");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
