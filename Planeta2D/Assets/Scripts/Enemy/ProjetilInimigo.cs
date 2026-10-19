
using UnityEngine;

public class ProjetilInimigo : MonoBehaviour
{
    public float velocidade = 2.5f;
    public float tempoDeVida = 7f;

    private PoolProjetilInimigo pool;
    private Vector2 direcao = Vector2.down;
    private float tempoDecorrido;

    public void DefinirPool(PoolProjetilInimigo poolProjetil)
    {
        pool = poolProjetil;
    }

    public void DefinirDirecao(Vector2 novaDirecao)
    {
        direcao = novaDirecao.sqrMagnitude > 0.001f
            ? novaDirecao.normalized
            : Vector2.down;
    }

    private void OnEnable()
    {
        tempoDecorrido = 0f;
        direcao = Vector2.down;
    }

    private void Update()
    {
        transform.position +=
            (Vector3)(direcao * velocidade * Time.deltaTime);

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= tempoDeVida)
        {
            DevolverAoPool();
        }
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
            {
                GameManager.instance.PerderVida();
            }

            DevolverAoPool();
        }
    }

    private void DevolverAoPool()
    {
        if (pool != null)
        {
            pool.DevolverProjetil(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
