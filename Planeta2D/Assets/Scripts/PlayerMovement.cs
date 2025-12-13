using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidade = 5f;
    private Rigidbody2D rb;
    private float movimento;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detecta apenas as setas direita e esquerda
        if (Input.GetKey(KeyCode.RightArrow))
            movimento = 1f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            movimento = -1f;
        else
            movimento = 0f;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(movimento * velocidade, rb.linearVelocity.y);

        // vira o player de acordo com a direção
        if (movimento > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (movimento < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
}
