
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
    public float velocidade = 2f;
    public float limiteEsquerdo = -5f;
    public float limiteDireito = 5f;
    public float distanciaDeMovimento = 2f;

    [Header("Transição de faixa")]
    public float duracaoInvulnerabilidade = 2f;
    public SpriteRenderer spriteChefe;

    private Animator animator;
    private float contadorTiro;
    private float contadorComportamento;
    private float contadorInvulnerabilidade;
    private float destinoX;
    private float direcaoHorizontal = 1f;
    private float alturaInicial;
    private Color corOriginal = Color.white;
    private bool invulneravel;
    private bool derrotado;
    private int faixaVidaAtual = -1;
    private int comportamentoAtual;

    private void Awake()
    {
        vidaMaxima = vida;
        animator = GetComponent<Animator>();
        alturaInicial = transform.position.y;

        if (spriteChefe == null)
            spriteChefe = GetComponent<SpriteRenderer>();

        if (spriteChefe != null)
            corOriginal = spriteChefe.color;

        destinoX = transform.position.x;
    }

    private void Start()
    {
        AtualizarFaixaVida(true);
        EscolherDestino();
    }

    private void Update()
    {
        if (derrotado)
            return;

        AtualizarFaixaVida(false);

        if (invulneravel)
        {
            contadorInvulnerabilidade -= Time.deltaTime;

            if (spriteChefe != null)
            {
                Color cor = corOriginal;
                cor.a = Mathf.Lerp(0.25f, 1f, Mathf.PingPong(Time.time * 8f, 1f));
                spriteChefe.color = cor;
            }

            if (contadorInvulnerabilidade <= 0f)
            {
                invulneravel = false;

                if (spriteChefe != null)
                    spriteChefe.color = corOriginal;
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
                animator.SetInteger("Comportamento", (comportamentoAtual + 1) % 3);
        }
    }

    public void DefinirComportamento(int faixaVida, int comportamento)
    {
        if (derrotado)
            return;

        bool mudouFaixa = faixaVidaAtual != -1 && faixaVidaAtual != faixaVida;

        faixaVidaAtual = faixaVida;
        comportamentoAtual = comportamento;
        contadorComportamento = 0f;

        if (mudouFaixa)
        {
            invulneravel = true;
            contadorInvulnerabilidade = duracaoInvulnerabilidade;
            contadorTiro = 0f;

            if (spriteChefe != null)
                spriteChefe.color = corOriginal;
        }

        EscolherDestino();
    }

    private void AtualizarFaixaVida(bool inicial)
    {
        if (vidaMaxima <= 0 || animator == null)
            return;

        float porcentagem = (float)vida / vidaMaxima;

        int novaFaixa;

        if (porcentagem > 0.5f)
            novaFaixa = 0;
        else if (porcentagem >= 0.2f)
            novaFaixa = 1;
        else
            novaFaixa = 2;

        if (inicial)
        {
            faixaVidaAtual = -1;
            animator.SetInteger("FaixaVida", novaFaixa);
            animator.SetInteger("Comportamento", 0);
        }
        else if (novaFaixa != faixaVidaAtual)
        {
            animator.SetInteger("FaixaVida", novaFaixa);
        }
    }

    private void ExecutarMovimento()
    {
        float multiplicador = faixaVidaAtual == 2 ? 1.5f :
                              faixaVidaAtual == 1 ? 1.2f : 1f;

        float esquerda = Mathf.Min(limiteEsquerdo, limiteDireito);
        float direita = Mathf.Max(limiteEsquerdo, limiteDireito);

        Vector3 posicao = transform.position;

        if (comportamentoAtual == 0)
        {
            posicao.x = Mathf.MoveTowards(
                posicao.x,
                destinoX,
                velocidade * multiplicador * Time.deltaTime
            );

            if (Mathf.Abs(posicao.x - destinoX) < 0.05f)
                EscolherDestino();
        }
        else if (comportamentoAtual == 1)
        {
            posicao.x += direcaoHorizontal * velocidade * multiplicador * Time.deltaTime;

            if (posicao.x >= direita)
            {
                posicao.x = direita;
                direcaoHorizontal = -1f;
            }
            else if (posicao.x <= esquerda)
            {
                posicao.x = esquerda;
                direcaoHorizontal = 1f;
            }
        }
        else
        {
            posicao.x = Mathf.Clamp(
                posicao.x + Mathf.Sin(Time.time * 2f) * velocidade * multiplicador * Time.deltaTime,
                esquerda,
                direita
            );
        }

        posicao.x = Mathf.Clamp(posicao.x, esquerda, direita);
        posicao.y = alturaInicial;

        transform.position = posicao;
    }

    private void EscolherDestino()
    {
        float esquerda = Mathf.Min(limiteEsquerdo, limiteDireito);
        float direita = Mathf.Max(limiteEsquerdo, limiteDireito);

        destinoX = Random.Range(esquerda, direita);
        direcaoHorizontal = transform.position.x < (esquerda + direita) / 2f ? 1f : -1f;
    }

    private void ExecutarTiros()
    {
        if (pontoDeTiro == null)
            return;

        if (PoolProjetilInimigo.Instance == null)
        {
            Debug.LogWarning("O chefe não encontrou o PoolProjetilInimigo na cena.", this);
            return;
        }

        float intervalo = tempoEntreTiros;

        if (faixaVidaAtual == 1)
            intervalo *= 0.75f;
        else if (faixaVidaAtual == 2)
            intervalo *= 0.5f;

        if (comportamentoAtual == 1)
            intervalo *= 0.7f;
        else if (comportamentoAtual == 2)
            intervalo *= 1.3f;

        if (contadorTiro >= intervalo)
        {
            PoolProjetilInimigo.Instance.ObterProjetil(
                pontoDeTiro.position,
                Quaternion.identity
            );

            contadorTiro = 0f;
        }
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (derrotado || invulneravel)
            return;

        if (outro.CompareTag("TiroJogador"))
        {
            vida--;

            Projetil projetilJogador = outro.GetComponent<Projetil>();

            if (projetilJogador != null)
                projetilJogador.DevolverAoPool();

            if (vida <= 0)
                Morrer();
        }
    }

    private void Morrer()
    {
        derrotado = true;

        if (spriteChefe != null)
            spriteChefe.color = corOriginal;

        if (ehBoss && !string.IsNullOrEmpty(proximaFase))
            SceneManager.LoadScene(proximaFase);
        else
            Destroy(gameObject);
    }
}
