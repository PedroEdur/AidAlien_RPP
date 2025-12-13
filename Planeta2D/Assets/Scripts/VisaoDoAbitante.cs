using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisaoDoAbitante : MonoBehaviour
{
    public OlhoDoAbitante olho1;
    public OlhoDoAbitante olho2;

    private MovimentoDoAbitante _movimentoDoAbitante;
    
    public bool avistamentoConfirmado = false;
    

    void Start()
    {
        _movimentoDoAbitante = GetComponent<MovimentoDoAbitante>();
    }
    

    void Update()
    {
        if (_movimentoDoAbitante.sentidoDoMovimento == MovimentoDoAbitante.Sentido.Horario)
        {
            olho1.gameObject.SetActive(true);
            olho2.gameObject.SetActive(false);
        }
        else
        if (_movimentoDoAbitante.sentidoDoMovimento == MovimentoDoAbitante.Sentido.AntiHorario)
        {
            olho2.gameObject.SetActive(true);
            olho1.gameObject.SetActive(false);
        }


        if (olho1.avistamento == true || olho2.avistamento == true)
        {
            avistamentoConfirmado = true;
        }
        
        if(  avistamentoConfirmado == true)
        {
            Debug.Log("Alerta da invasão....!!!!!!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
}
