using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 3;
    private int vidasTotais;
    
    public GameObject[] vidasSprite;

    private void Start()
    {
        vidasTotais = vidas;
    }

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (vidasSprite.Length >0)
        {

            for (int i = vidasTotais; i > vidas; i--)
            {
                vidasSprite[i-1].SetActive(false);
            }
        }
    }

    public void PerderVida()
    {
        vidas--;

        if (vidas <= 0)
        {
            ReiniciarBoss();
        }
    }

    public void ReiniciarBoss()
    {
        // reinicia a cena atual (fase do boss)
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
