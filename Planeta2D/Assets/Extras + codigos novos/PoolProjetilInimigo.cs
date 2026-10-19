
using UnityEngine;
using UnityEngine.Pool;

public class PoolProjetilInimigo : MonoBehaviour
{
    public static PoolProjetilInimigo Instance { get; private set; }

    [Header("Configuração do pool")]
    public GameObject projetilPrefab;
    public int capacidadeInicial = 15;
    public int tamanhoMaximo = 50;

    private ObjectPool<GameObject> pool;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (projetilPrefab == null)
        {
            Debug.LogError("PoolProjetilInimigo: configure o prefab do tiro inimigo no Inspector.", this);
            enabled = false;
            return;
        }

        if (projetilPrefab.GetComponent<ProjetilInimigo>() == null)
        {
            Debug.LogError("O prefab do tiro inimigo precisa ter o componente ProjetilInimigo no objeto principal.", projetilPrefab);
            enabled = false;
            return;
        }

        pool = new ObjectPool<GameObject>(
            CriarProjetil,
            AtivarProjetil,
            DesativarProjetil,
            DestruirProjetil,
            false,
            Mathf.Max(1, capacidadeInicial),
            Mathf.Max(1, tamanhoMaximo)
        );
    }

    private GameObject CriarProjetil()
    {
        GameObject projetil = Instantiate(projetilPrefab);
        ProjetilInimigo script = projetil.GetComponent<ProjetilInimigo>();

        script.DefinirPool(this);
        projetil.SetActive(false);

        return projetil;
    }

    private void AtivarProjetil(GameObject projetil)
    {
        projetil.SetActive(true);
    }

    private void DesativarProjetil(GameObject projetil)
    {
        projetil.SetActive(false);
    }

    private void DestruirProjetil(GameObject projetil)
    {
        if (projetil != null)
            Destroy(projetil);
    }

    public GameObject ObterProjetil(Vector3 posicao, Quaternion rotacao)
    {
        if (pool == null)
        {
            Debug.LogError("PoolProjetilInimigo: o pool não foi inicializado.", this);
            return null;
        }

        GameObject projetil = pool.Get();

        if (projetil == null)
            return null;

        projetil.transform.SetPositionAndRotation(posicao, rotacao);
        return projetil;
    }

    public void DevolverProjetil(GameObject projetil)
    {
        if (projetil != null && pool != null && projetil.activeSelf)
            pool.Release(projetil);
    }
}
