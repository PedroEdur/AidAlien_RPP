using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            GerenciadorFase1.instance.ColetarItem();
            Destroy(gameObject);
        }
    }
}