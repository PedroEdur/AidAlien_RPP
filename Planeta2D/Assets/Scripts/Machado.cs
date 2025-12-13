using System;
using Unity.VisualScripting;
using UnityEngine;

public class Machado : MonoBehaviour
{
   public float velocidade = 1;
   public float tempoDeVida = 5f;
   
    void Start()
    {
         Destroy(gameObject, tempoDeVida);
    }

 
    void Update()
    {
        transform.Translate(Vector2.up * velocidade * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Planeta"))
        {
            
        }
        
        Destroy(gameObject);
    }
}
