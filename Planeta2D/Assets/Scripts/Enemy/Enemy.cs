
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    [Header("Vida do chefe")]
    public int vida = 200;
    private int vidaMaxima;

    [Header("Tiros")]
    public GameObject projetilInimigoPrefab;
    public Transform pontoDeTiro;
    public float tempoEntreTiros = 2.5f;

    [Header("Configuração de fase")]
    public bool ehBoss = true;
    public string proximaFase = "Cutscene final";

    [Header("Movimento")]
    public float velocidade = 1.5f;
    public float limiteEsquerdo = -5f;
    public float limiteDireito = 5f;

    [Header("Transição de faixa")]
    public float duracaoInvulnerabilidade = 2f;
    public SpriteRenderer spriteChefe;

    [Header("Ataques normais")]
    public float aberturaLeque = 35f;
    public int tirosCirculares = 8;
    public float intervaloComportamento = 6f;

    [Header("Terceiro estágio - Cruz giratória")]
    public float intervaloTirosCruz = 0.3f;
    public float velocidadeRotacaoCruz = 45f;

    private Animator animator;
    private InimigoVoador inimigoVoador;

    private float contadorTiro;
    private float contadorComportamento;
    private float contadorInvulnerabilidade;
    private float contadorTiroCruz;
    private float anguloCruz;
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
        inimigoVoador = GetComponent<InimigoVoador>();

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
                cor.a = Mathf.Lerp(
                    0.3f,
                    1f,
                    Mathf.PingPong(Time.time * 6f, 1f)
                );

                spriteChefe.color = cor;
            }

            if (contadorInvulnerabilidade <= 0f)
            {
                invulneravel = false;

                if (spriteChefe != null)
                    spriteChefe.color = corOriginal;

                contadorComportamento = 0f;
                contadorTiro = 0f;
                contadorTiroCruz = 0f;

                EscolherDestino();
            }

            return;
        }

        contadorComportamento += Time.deltaTime;

        if (faixaVidaAtual == 2)
        {
            anguloCruz += velocidadeRotacaoCruz * Time.deltaTime;
            contadorTiroCruz += Time.deltaTime;
        }
        else
        {
            contadorTiro += Time.deltaTime;
        }

        if (inimigoVoador == null)
            ExecutarMovimento();

        ExecutarTiros();

        if (contadorComportamento >= intervaloComportamento)
        {
            contadorComportamento = 0f;

            int proximoComportamento = (comportamentoAtual + 1) % 3;

            if (animator != null)
            {
                animator.SetInteger(
                    "Comportamento",
                    proximoComportamento
                );
            }
            else
            {
                comportamentoAtual = proximoComportamento;
            }
        }
    }

    public bool EstaInvulneravel()
    {
        return invulneravel || derrotado;
    }

    public void DefinirComportamento(int faixaVida, int comportamento)
    {
        if (derrotado)
            return;

        bool mudouFaixa =
            faixaVidaAtual != -1 &&
            faixaVidaAtual != faixaVida;

        faixaVidaAtual = faixaVida;
        comportamentoAtual = comportamento;
        contadorComportamento = 0f;

        if (mudouFaixa)
        {
            invulneravel = true;
            contadorInvulnerabilidade = duracaoInvulnerabilidade;

            contadorTiro = 0f;
            contadorTiroCruz = 0f;

            if (spriteChefe != null)
                spriteChefe.color = corOriginal;
        }

        if (!invulneravel)
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
        float multiplicador =
            faixaVidaAtual == 2 ? 1.25f :
            faixaVidaAtual == 1 ? 1.1f : 1f;

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
            posicao.x += direcaoHorizontal *
                         velocidade *
                         multiplicador *
                         Time.deltaTime;

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
                posicao.x +
                Mathf.Sin(Time.time * 1.5f) *
                velocidade *
                multiplicador *
                Time.deltaTime,
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

        direcaoHorizontal =
            transform.position.x < (esquerda + direita) / 2f
            ? 1f
            : -1f;
    }

    private void ExecutarTiros()
    {
        if (pontoDeTiro == null ||
            PoolProjetilInimigo.Instance == null)
            return;

        if (faixaVidaAtual == 2)
        {
            if (contadorTiroCruz >= intervaloTirosCruz)
            {
                contadorTiroCruz = 0f;
                AtirarCruzGiratoria();
            }

            return;
        }

        float intervalo = tempoEntreTiros;

        if (faixaVidaAtual == 1)
            intervalo *= 0.8f;

        if (comportamentoAtual == 1)
            intervalo *= 0.9f;
        else if (comportamentoAtual == 2)
            intervalo *= 1.15f;

        if (contadorTiro < intervalo)
            return;

        contadorTiro = 0f;

        if (faixaVidaAtual == 1)
        {
            AtirarCircular();
        }
        else if (comportamentoAtual == 0)
        {
            AtirarLeque();
        }
        else if (comportamentoAtual == 1)
        {
            AtirarDirecionado();
        }
        else
        {
            AtirarCircular();
        }
    }

    private void AtirarLeque()
    {
        int quantidade = 3;
        float passo = aberturaLeque / (quantidade - 1);
        float anguloInicial = -aberturaLeque / 2f;

        for (int i = 0; i < quantidade; i++)
        {
            float angulo = anguloInicial + passo * i;

            Vector2 direcao =
                Quaternion.Euler(0f, 0f, angulo) * Vector2.down;

            CriarProjetil(direcao);
        }
    }

    private void AtirarDirecionado()
    {
        GameObject jogador = GameObject.FindGameObjectWithTag("Player");

        if (jogador == null)
        {
            AtirarLeque();
            return;
        }

        Vector2 direcao =
            ((Vector2)jogador.transform.position -
             (Vector2)pontoDeTiro.position).normalized;

        CriarProjetil(direcao);
    }

    private void AtirarCircular()
    {
        int quantidade = faixaVidaAtual == 1 ? 8 : tirosCirculares;

        for (int i = 0; i < quantidade; i++)
        {
            float angulo = i * (360f / quantidade);

            Vector2 direcao = new Vector2(
                Mathf.Cos(angulo * Mathf.Deg2Rad),
                Mathf.Sin(angulo * Mathf.Deg2Rad)
            );

            CriarProjetil(direcao);
        }
    }

    private void AtirarCruzGiratoria()
    {
        for (int i = 0; i < 4; i++)
        {
            float angulo = anguloCruz + i * 90f;

            Vector2 direcao = new Vector2(
                Mathf.Cos(angulo * Mathf.Deg2Rad),
                Mathf.Sin(angulo * Mathf.Deg2Rad)
            );

            CriarProjetil(direcao);
        }
    }

    private void CriarProjetil(Vector2 direcao)
    {
        GameObject projetil =
            PoolProjetilInimigo.Instance.ObterProjetil(
                pontoDeTiro.position,
                Quaternion.identity
            );

        if (projetil == null)
            return;

        ProjetilInimigo script =
            projetil.GetComponent<ProjetilInimigo>();

        if (script != null)
            script.DefinirDirecao(direcao);
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (derrotado || invulneravel)
            return;

        if (outro.CompareTag("TiroJogador"))
        {
            vida--;

            Projetil projetilJogador =
                outro.GetComponent<Projetil>();

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
