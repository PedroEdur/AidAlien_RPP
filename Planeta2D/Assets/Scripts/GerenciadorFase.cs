uusing UnityEngine;
using UnityEngine.SceneManagement;

public class GerenciadorFase : MonoBehaviour
{
    public static GerenciadorFase instance;

    public int totalItens;     // Quantos itens existem na fase
    private int itensColetados;

    void Awake()
    {
        instance = this;
    }

    public void ColetarItem()
    {
        itensColetados++;

        if (itensColetados >= totalItens)
        {
            MudarDeFase();
        }
    }

    void MudarDeFase()
    {
        int faseAtual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(faseAtual + 1);
    }
}
