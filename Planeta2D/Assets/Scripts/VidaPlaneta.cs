using System;
using UnityEngine;

public class VidaPlaneta : MonoBehaviour
{
  public int vida = 100;
  public int DanoDaBala = 10;
  
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TiroInimigo"))
        {
            vida -= DanoDaBala;
            Destroy(other.gameObject);
        }
        
    }
}
