
using UnityEngine;

public class Projetil : MonoBehaviour
{
    public float velocidade = 5f;
    public float tempoDeVida = 1.7f;

    private PoolProjetilJogador pool;
    private float tempoDecorrido;

    public void DefinirPool(PoolProjetilJogador poolProjetil)
    {
        pool = poolProjetil;
    }

    private void OnEnable()
    {
        tempoDecorrido = 0f;
    }

    private void Update()
    {
        transform.position += Vector3.up * velocidade * Time.deltaTime;

        tempoDecorrido += Time.deltaTime;

        if (tempoDecorrido >= tempoDeVida)
        {
            DevolverAoPool();
        }
    }

    public void DevolverAoPool()
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
