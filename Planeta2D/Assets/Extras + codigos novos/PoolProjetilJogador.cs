
using UnityEngine;
using UnityEngine.Pool;

public class PoolProjetilJogador : MonoBehaviour
{
    public static PoolProjetilJogador Instance { get; private set; }

    [Header("Configuração do pool")]
    public GameObject projetilPrefab;
    public int capacidadeInicial = 10;
    public int tamanhoMaximo = 30;

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
            Debug.LogError("PoolProjetilJogador: arraste o prefab do projétil para o campo Projetil Prefab no Inspector.", this);
            enabled = false;
            return;
        }

        if (projetilPrefab.GetComponent<Projetil>() == null)
        {
            Debug.LogError("PoolProjetilJogador: o prefab precisa ter o componente Projetil (Script) no mesmo objeto principal.", projetilPrefab);
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
        Projetil scriptProjetil = projetil.GetComponent<Projetil>();

        if (scriptProjetil == null)
        {
            Debug.LogError("O projétil criado não possui o componente Projetil (Script).", projetil);
            Destroy(projetil);
            return null;
        }

        scriptProjetil.DefinirPool(this);
        projetil.SetActive(false);

        return projetil;
    }

    private void AtivarProjetil(GameObject projetil)
    {
        if (projetil != null)
            projetil.SetActive(true);
    }

    private void DesativarProjetil(GameObject projetil)
    {
        if (projetil != null)
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
            Debug.LogError("PoolProjetilJogador: o pool não foi inicializado.", this);
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
