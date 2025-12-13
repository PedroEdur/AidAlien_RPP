using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipalManager : MonoBehaviour
{
    [SerializeField] private string nomeDoLevelDeJogo;

    [Header("Painéis do Menu")] [SerializeField]
    private GameObject painelMenuInicial;

    [SerializeField] private GameObject painelOpcoes;
    [SerializeField] private GameObject painelCreditos; // << Novo painel adicionado

    // Iniciar jogo
    public void cotscene()
    {
        SceneManager.LoadScene(nomeDoLevelDeJogo);
    }

    // Abrir Opções
    public void AbrirConfiguraçõesBotton()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(true);
        painelCreditos.SetActive(false);
    }

    // Fechar Opções
    public void FecharOpcoes()
    {
        painelOpcoes.SetActive(false);
        painelMenuInicial.SetActive(true);
        painelCreditos.SetActive(false);
    }

    // --- NOVAS FUNÇÕES: Créditos ---

    // Abrir Créditos
    public void AbrirCreditos()
    {
        painelMenuInicial.SetActive(false);
        painelOpcoes.SetActive(false);
        painelCreditos.SetActive(true);
    }

    // Fechar Créditos
    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
        painelMenuInicial.SetActive(true);
        painelOpcoes.SetActive(false);
    }

    // Sair do Jogo
    public void SairJogo()
    {
        Debug.Log("Sair do Jogo");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
