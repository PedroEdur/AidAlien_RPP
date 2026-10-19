
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Vida do chefe")]
    public int vida = 67;
    private int vidaMaxima;

    [Header("Tiros")]
    public GameObject projetilInimigoPrefab;
    public Transform pontoDeTiro;
    public float tempoEntreTiros = 2f;

    [Header("Configuração de fase")]
    public bool ehBoss = false;
    public string proximaFase = "Cutscene final";

    [Header("Movimento")]
    public float velocidade = 3f;
    public float distanciaDeMovimento = 3f;

    [Header("Transição de faixa")]
    public float duracaoInvulnerabilidade = 2f;
    public SpriteRenderer spriteChefe;

    private Animator animator;
    private float contadorTiro;
    private float contadorComportamento;
    private float contadorInvulnerabilidade;
    private Vector3 posicaoInicial;
    private Vector3 direcao;
    private Color corOriginal = Color.white;
    private bool invulneravel;
    private bool derrotado;
    private int faixaVidaAtual = -1;
    private int comportamentoAtual;

    private void Awake()
    {
        vidaMaxima = vida;
        animator = GetComponent<Animator>();
        posicaoInicial = transform.position;

        if (spriteChefe == null)
        {
            spriteChefe = GetComponent<SpriteRenderer>();
        }

        if (spriteChefe != null)
        {
            corOriginal = spriteChefe.color;
        }
    }

    private void Start()
    {
        AtualizarFaixaVida(true);
        EscolherDirecao();
    }

    private void Update()
    {
        if (derrotado)
        {
            return;
        }

        AtualizarFaixaVida(false);

        if (invulneravel)
        {
            contadorInvulnerabilidade -= Time.deltaTime;

            if (spriteChefe != null)
            {
                float piscar = Mathf.PingPong(Time.time * 8f, 1f);
                Color cor = corOriginal;
                cor.a = Mathf.Lerp(0.25f, 1f, piscar);
                spriteChefe.color = cor;
            }

            if (contadorInvulnerabilidade <= 0f)
            {
                invulneravel = false;

                if (spriteChefe != null)
                {
                    spriteChefe.color = corOriginal;
                }
            }

            return;
        }

        contadorComportamento += Time.deltaTime;
        contadorTiro += Time.deltaTime;

        ExecutarMovimento();
        ExecutarTiros();

        if (contadorComportamento >= 5f)
        {
            contadorComportamento = 0f;

            if (animator != null)
            {
                int proximoComportamento = (comportamentoAtual + 1) % 3;
                animator.SetInteger("Comportamento", proximoComportamento);
            }
        }
    }

    public void DefinirComportamento(int faixaVida, int comportamento)
    {
        if (derrotado)
        {
            return;
        }

        bool mudouFaixa = faixaVidaAtual != -1 &&
                          faixaVidaAtual != faixaVida;

        faixaVidaAtual = faixaVida;
        comportamentoAtual = comportamento;
        contadorComportamento = 0f;

        if (mudouFaixa)
        {
            invulneravel = true;
            contadorInvulnerabilidade = duracaoInvulnerabilidade;
            contadorTiro = 0f;

            if (spriteChefe != null)
            {
                spriteChefe.color = corOriginal;
            }
        }

        EscolherDirecao();
    }

    private void AtualizarFaixaVida(bool inicial)
    {
        if (vidaMaxima <= 0 || animator == null)
        {
            return;
        }

        float porcentagem = (float)vida / vidaMaxima;
        int novaFaixa;

        if (porcentagem > 0.5f)
        {
            novaFaixa = 0;
        }
        else if (porcentagem >= 0.2f)
        {
            novaFaixa = 1;
        }
        else
        {
            novaFaixa = 2;
        }

        if (inicial || novaFaixa != faixaVidaAtual)
        {
            animator.SetInteger("FaixaVida", novaFaixa);

            if (inicial)
            {
                faixaVidaAtual = -1;
                animator.SetInteger("Comportamento", 0);
            }
        }
    }

    private void ExecutarMovimento()
    {
        float multiplicadorVelocidade = 1f;

        if (faixaVidaAtual == 1)
        {
            multiplicadorVelocidade = 1.4f;
        }
        else if (faixaVidaAtual == 2)
        {
            multiplicadorVelocidade = 1.8f;
        }

        if (comportamentoAtual == 0)
        {
            transform.position += direcao * velocidade *
                                  multiplicadorVelocidade * Time.deltaTime;

            if (Vector3.Distance(posicaoInicial, transform.position) >=
                distanciaDeMovimento)
            {
                EscolherDirecao();
            }
        }
        else if (comportamentoAtual == 1)
        {
            Vector3 alvo = posicaoInicial + Vector3.right * distanciaDeMovimento;
            transform.position = Vector3.MoveTowards(
                transform.position,
                alvo,
                velocidade * multiplicadorVelocidade * Time.deltaTime);

            if (Vector3.Distance(transform.position, alvo) < 0.1f)
            {
                posicaoInicial = transform.position;
            }
        }
        else
        {
            float deslocamento = Mathf.Sin(Time.time * 2f) *
                                 distanciaDeMovimento;
            Vector3 posicao = transform.position;
            posicao.x = posicaoInicial.x + deslocamento;
            transform.position = posicao;
        }
    }

    private void ExecutarTiros()
    {
        if (projetilInimigoPrefab == null || pontoDeTiro == null)
        {
            return;
        }

        float intervalo = tempoEntreTiros;

        if (faixaVidaAtual == 1)
        {
            intervalo *= 0.75f;
        }
        else if (faixaVidaAtual == 2)
        {
            intervalo *= 0.5f;
        }

        if (comportamentoAtual == 1)
        {
            intervalo *= 0.7f;
        }
        else if (comportamentoAtual == 2)
        {
            intervalo *= 1.3f;
        }

        if (contadorTiro >= intervalo)
        {
            Instantiate(
                projetilInimigoPrefab,
                pontoDeTiro.position,
                Quaternion.identity);

            contadorTiro = 0f;
        }
    }

    private void EscolherDirecao()
    {
        Vector2 novaDirecao = Random.insideUnitCircle.normalized;

        if (novaDirecao == Vector2.zero)
        {
            novaDirecao = Vector2.left;
        }

        direcao = novaDirecao;
        posicaoInicial = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (derrotado || invulneravel)
        {
            return;
        }

        if (outro.CompareTag("TiroJogador"))
        {
            vida--;

            if (vida <= 0)
            {
                Morrer();
            }
        }
    }

    private void Morrer()
    {
        derrotado = true;

        if (spriteChefe != null)
        {
            spriteChefe.color = corOriginal;
        }

        if (ehBoss && !string.IsNullOrEmpty(proximaFase))
        {
            SceneManager.LoadScene(proximaFase);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
