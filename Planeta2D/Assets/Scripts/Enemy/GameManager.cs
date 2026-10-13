using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int vidas = 3;
    private int danosRecebidos = 0;

    public GameObject[] vidasSprite;

    private void Awake()
    {
        instance = this;
        danosRecebidos = 0;
    }

    private void Start()
    {
        AtualizarVidas();
    }

    public void PerderVida()
    {
        danosRecebidos++;

        Debug.Log("Dano recebido: " + danosRecebidos);

        AtualizarVidas();

        if (danosRecebidos >= 4)
        {
            SceneManager.LoadScene("Perdeu");
        }
    }

    private void AtualizarVidas()
    {
        int iconesVisiveis = Mathf.Clamp(3 - danosRecebidos, 0, 3);

        for (int i = 0; i < vidasSprite.Length; i++)
        {
            if (vidasSprite[i] != null)
            {
                vidasSprite[i].SetActive(i < iconesVisiveis);
            }
        }
    }

    public void ReiniciarBoss()
    {
        SceneManager.LoadScene("Perdeu");
    }
}