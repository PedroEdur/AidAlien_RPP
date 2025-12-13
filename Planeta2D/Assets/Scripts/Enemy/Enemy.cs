using UnityEngine;
using UnityEngine.SceneManagement; // necessário para trocar de cena

public class Enemy : MonoBehaviour
{
    public int vida = 15;                     // vida da nave inimiga (boss ou inimigo normal)
    public GameObject projetilInimigoPrefab;  // tiro que ela dispara
    public Transform pontoDeTiro;             // posição de onde o tiro sai
    public float tempoEntreTiros = 2f;
    private float contador;

    [Header("Configuração de fase")]
    public bool ehBoss = false;              // marque TRUE se este inimigo for o boss
    public string proximaFase;               // nome da cena que deve carregar após derrotar o boss

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tempoEntreTiros)
        {
            Instantiate(projetilInimigoPrefab, pontoDeTiro.position, Quaternion.identity);
            contador = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("TiroJogador"))
        {
            vida--;
            Destroy(outro.gameObject);

            if (vida <= 0)
            {
                Morrer();
            }
        }
    }

    void Morrer()
    {
        Destroy(gameObject);

        // Se for o boss, troca de fase
        if (ehBoss)
        {
            SceneManager.LoadScene(proximaFase);
        }
    }
}
