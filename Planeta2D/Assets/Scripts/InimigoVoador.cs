
using UnityEngine;

public class InimigoVoador : MonoBehaviour
{
    public float velocidade = 5f;
    public float distanciaDeMovimento = 3f;
    public float tempoParaMudarDirecao = 2f;

    private Vector3 posicaoInicial;
    private Vector3 direcao;
    private float contador;

    private void Start()
    {
        posicaoInicial = transform.position;
        EscolherDirecao();
    }

    private void Update()
    {
        contador += Time.deltaTime;

        transform.position += direcao * velocidade * Time.deltaTime;

        if (contador >= tempoParaMudarDirecao ||
            Vector3.Distance(posicaoInicial, transform.position) >= distanciaDeMovimento)
        {
            EscolherDirecao();
        }
    }

    private void EscolherDirecao()
    {
        contador = 0f;

        Vector2 novaDirecao = Random.insideUnitCircle.normalized;

        if (novaDirecao == Vector2.zero)
        {
            novaDirecao = Vector2.up;
        }

        direcao = novaDirecao;
        posicaoInicial = transform.position;
    }
}
