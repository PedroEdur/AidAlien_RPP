
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float velocidade = 5f;
    private Rigidbody2D rb;

    [Header("Armas")]
    public GameObject projetilPrefab;
    public Transform pontoDeTiro;
    public GameObject escudo;
    public GameObject armaVisual;
    public float intervaloEntreTiros = 0.3f;

    private bool usandoPlasma = true;
    private float contadorTiro;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AlternarArma(true);
    }

    private void Update()
    {
        float movimentoHorizontal = 0f;
        float movimentoVertical = 0f;

        if (Input.GetKey(KeyCode.RightArrow))
            movimentoHorizontal = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            movimentoHorizontal = -1f;

        if (Input.GetKey(KeyCode.UpArrow))
            movimentoVertical = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            movimentoVertical = -1f;

        rb.linearVelocity = new Vector2(
            movimentoHorizontal * velocidade,
            movimentoVertical * velocidade
        );

        if (Input.GetKeyDown(KeyCode.Z))
        {
            usandoPlasma = !usandoPlasma;
            AlternarArma(usandoPlasma);
        }

        if (usandoPlasma && Input.GetKey(KeyCode.X))
        {
            contadorTiro -= Time.deltaTime;

            if (contadorTiro <= 0f)
            {
                Atirar();
                contadorTiro = intervaloEntreTiros;
            }
        }
        else
        {
            contadorTiro = 0f;
        }
    }

    private void Atirar()
    {
        if (PoolProjetilJogador.Instance != null)
        {
            PoolProjetilJogador.Instance.ObterProjetil(
                pontoDeTiro.position,
                Quaternion.identity
            );
        }
        else
        {
            Debug.LogWarning("PoolProjetilJogador não foi configurado na cena.");
        }
    }

    private void AlternarArma(bool plasmaAtivo)
    {
        if (escudo != null)
            escudo.SetActive(!plasmaAtivo);

        if (armaVisual != null)
            armaVisual.SetActive(plasmaAtivo);
    }
}
