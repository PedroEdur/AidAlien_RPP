using UnityEngine;

public class Coletavel : MonoBehaviour
{
    // Variável estática para contar as coletas
    public static int contadorColetas = 0;

    // Método chamado quando outro collider entra na área do coletável
    void OnTriggerEnter(Collider outro)
    {
        // Verifica se o objeto que colidiu é o jogador
        if (outro.CompareTag("Jogador"))
        {
            // Incrementa o contador de coletas
            contadorColetas++;
            // Exibe no console o número de coletas
            Debug.Log("Coletáveis: " + contadorColetas);
            // Destroi o coletável após a coleta
            Destroy(gameObject);
        }
    }
}