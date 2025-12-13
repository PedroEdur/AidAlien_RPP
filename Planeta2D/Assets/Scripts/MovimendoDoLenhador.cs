using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class MovimentoDoLenhador : MonoBehaviour
{
    public float recargarVelocidade = 0.4f;
    private float tempo = 0;

    public GameObject machado;
    public Transform pontoDeAremesso;
    void Start()
    {
      
    }
    
    private void Update()
    {

        if (tempo > recargarVelocidade)
        {
            GameObject obj = Instantiate(machado) as GameObject;
            obj.transform.position = pontoDeAremesso.position;
            obj.transform.rotation = pontoDeAremesso.rotation;

            tempo = 0;
        }
        
        tempo += Time.deltaTime;

    }
    
}

