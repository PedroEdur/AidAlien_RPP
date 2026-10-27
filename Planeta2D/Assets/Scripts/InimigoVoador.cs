
using UnityEngine;

public class InimigoVoador : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 1.5f;
    public float distanciaDeMovimento = 2f;
    public float tempoParaMudarDirecao = 2f;

    [Header("Limites da tela")]
    public float limiteEsquerdo = -5f;
    public float limiteDireito = 5f;
    public float limiteInferior = 2f;
    public float limiteSuperior = 4f;

    private Vector3 posicaoInicial;
    private Vector3 direcao;
    private float contador;
    private Enemy chefe;

    private void Awake()
    {
        chefe = GetComponent<Enemy>();
    }

    private void Start()
    {
        posicaoInicial = transform.position;
        EscolherDirecao();
    }

    private void Update()
    {
        if (chefe != null && chefe.EstaInvulneravel())
        {
            posicaoInicial = transform.position;
            contador = 0f;
            return;
        }

        contador += Time.deltaTime;

        Vector3 proximaPosicao =
            transform.position + direcao * velocidade * Time.deltaTime;

        proximaPosicao.x = Mathf.Clamp(
            proximaPosicao.x,
            Mathf.Min(limiteEsquerdo, limiteDireito),
            Mathf.Max(limiteEsquerdo, limiteDireito)
        );

        proximaPosicao.y = Mathf.Clamp(
            proximaPosicao.y,
            Mathf.Min(limiteInferior, limiteSuperior),
            Mathf.Max(limiteInferior, limiteSuperior)
        );

        transform.position = proximaPosicao;

        if (contador >= tempoParaMudarDirecao ||
            Vector3.Distance(posicaoInicial, transform.position) >= distanciaDeMovimento)
        {
            EscolherDirecao();
        }
    }

    private void EscolherDirecao()
    {
        contador = 0f;
        posicaoInicial = transform.position;

        Vector2 novaDirecao = Random.insideUnitCircle.normalized;

        if (novaDirecao == Vector2.zero)
            novaDirecao = Vector2.down;

        direcao = new Vector3(
            novaDirecao.x,
            novaDirecao.y,
            0f
        );

        if (transform.position.x <= limiteEsquerdo)
            direcao.x = Mathf.Abs(direcao.x);

        if (transform.position.x >= limiteDireito)
            direcao.x = -Mathf.Abs(direcao.x);

        if (transform.position.y <= limiteInferior)
            direcao.y = Mathf.Abs(direcao.y);

        if (transform.position.y >= limiteSuperior)
            direcao.y = -Mathf.Abs(direcao.y);

        if (direcao.sqrMagnitude < 0.01f)
            direcao = Vector3.down;

        direcao.Normalize();
    }
}
