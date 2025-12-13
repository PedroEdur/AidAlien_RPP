using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    private Rigidbody2D rb;

    [Header("Armas")]
    public GameObject projetilPrefab;   // prefab do tiro de plasma
    public Transform pontoDeTiro;       // posição de onde o tiro sai
    public GameObject escudo;           // objeto do escudo
    public GameObject armaVisual;       // sprite/objeto da arma visível

    private bool usandoPlasma = true;   // começa com arma ativa

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AlternarArma(true); // começa com a arma ativa
    }

    void Update()
    {
        // Movimento pelas setas esquerda e direita
        float movimento = 0f;
        if (Input.GetKey(KeyCode.RightArrow))
            movimento = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            movimento = -1f;

        rb.linearVelocity = new Vector2(movimento * velocidade, rb.linearVelocity.y);

        // Trocar entre arma e escudo (Z)
        if (Input.GetKeyDown(KeyCode.Z))
        {
            usandoPlasma = !usandoPlasma;
            AlternarArma(usandoPlasma);
        }

        // Disparar com X (só se estiver usando plasma)
        if (usandoPlasma && Input.GetKeyDown(KeyCode.X))
        {
            Atirar();
        }
    }

    void Atirar()
    {
        // Cria o projétil no ponto de tiro, virado pra cima
        Instantiate(projetilPrefab, pontoDeTiro.position, Quaternion.identity);
    }

    void AlternarArma(bool plasmaAtivo)
    {
        // Ativa o escudo quando plasma estiver desligado
        escudo.SetActive(!plasmaAtivo);

        // Mostra a arma visual quando o plasma estiver ativo
        if (armaVisual != null)
            armaVisual.SetActive(plasmaAtivo);
    }
}

