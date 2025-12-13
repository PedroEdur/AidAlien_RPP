using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 5f;
    public float tempoDeVida = 1.7f;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }
}
