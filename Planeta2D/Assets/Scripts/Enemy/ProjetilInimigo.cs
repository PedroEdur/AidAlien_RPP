
using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    public float velocidade = 5f;
    public float tempoDeVida = 5f;

    private PoolProjetilInimigo pool;
    private float tempoDecorrido;

    public void DefinirPool(PoolProjetilInimigo poolProjetil)
    {
        pool = poolProjetil;
    }

    private void OnEnable()
    {
        tempoDecorrido = 0f;
    }

    private void Update()
    {
        transform.position += Vector3.down * velocidade * Time.deltaTime;

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= tempoDeVida)
            DevolverAoPool();
    }

    private void OnTriggerEnter2D(Collider2D outro)
    {
        if (outro.CompareTag("Escudo"))
        {
            DevolverAoPool();
            return;
        }

        if (outro.CompareTag("Player"))
        {
            if (GameManager.instance != null)
                GameManager.instance.PerderVida();

            DevolverAoPool();
        }
    }

    private void DevolverAoPool()
    {
        if (pool != null)
            pool.DevolverProjetil(gameObject);
        else
            gameObject.SetActive(false);
    }
}
