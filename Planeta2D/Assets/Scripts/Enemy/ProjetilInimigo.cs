using UnityEngine;
using UnityEngine.SceneManagement;

public class ProjetilInimigo : MonoBehaviour
{
    public float velocidade = 5f;
    public float tempoDeVida = 5f;

    void Start()
    {
        Destroy(gameObject, tempoDeVida);
    }

    void Update()
    {
        // movimento pra baixo
        transform.Translate(Vector2.down * velocidade * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Escudo"))
        {
            // se tocar no escudo, o tiro � repelido (pode ser destru�do)
            Destroy(gameObject);
        }
        
        if (outro.CompareTag("Player"))
        {
            // se acertar o jogador
            GameManager.instance.PerderVida();
            Destroy(gameObject);
        }
        
        if (outro.CompareTag("Planeta"))
        {
            // se atingir o planeta, reinicia a fase do boss
            GameManager.instance.ReiniciarBoss();
            Destroy(gameObject);
        }
    }
}
