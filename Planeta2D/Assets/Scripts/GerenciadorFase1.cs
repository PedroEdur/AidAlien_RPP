using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorFase1 : MonoBehaviour
{
    public static GerenciadorFase1 instance;
    public GameObject portal;


    public int totalItens;

    void Awake()
    {
        instance = this;
    }

    private void Update()
    {
     //   if (totalItens <= 0)
      //  {
            // MudarDeFase();
           
            if (transform.childCount <= 0)
            {
                if ( portal != null)
                {
                    portal.SetActive(true);
                }
            }
       // }
    }


    public void ColetarItem()
    {
        totalItens--;
    }

    void MudarDeFase()
    {
        int faseAtual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(faseAtual + 1);
    }
    
}