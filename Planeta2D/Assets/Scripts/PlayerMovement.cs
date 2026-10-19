using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    private Rigidbody2D rb;
    private float movimentoHorizontal;
    private float movimentoVertical;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movimento horizontal
        if (Input.GetKey(KeyCode.RightArrow))
            movimentoHorizontal = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            movimentoHorizontal = -1f;
        else
            movimentoHorizontal = 0f;

        // Movimento vertical
        if (Input.GetKey(KeyCode.UpArrow))
            movimentoVertical = 1f;
        else if (Input.GetKey(KeyCode.DownArrow))
            movimentoVertical = -1f;
        else
            movimentoVertical = 0f;
    }

    void FixedUpdate()
    {
        // Move o player para os quatro lados
        rb.linearVelocity = new Vector2(
            movimentoHorizontal * velocidade,
            movimentoVertical * velocidade
        );

        // Vira o player de acordo com a direção horizontal
        if (movimentoHorizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movimentoHorizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}