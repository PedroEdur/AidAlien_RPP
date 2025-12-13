using UnityEngine;

public class InimigoVoador : MonoBehaviour
{
        
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;
    


    public float distanciaDeVisao = 10;
    public float velocidade = 5;

    
    public Transform[] pontosDePatrulha;
    private int pontoAtual = 0;
    
   void Start()
    {

        
      for(int i = 0; i <pontosDePatrulha.Length; i++)
        {

          Debug.Log(pontosDePatrulha[i].name);
          
          pontosDePatrulha[i].transform.SetParent(null);
        }
        
    }
  void Update()
    {
                Debug.Log(pontoAtual);
                
                transform.position = Vector3.MoveTowards(transform.position, 
                    pontosDePatrulha[pontoAtual].position, 
                    velocidade * Time.deltaTime);

                if((Mathf.Abs(pontosDePatrulha[pontoAtual].position.x - transform.position.x) <= 0.01f ||
                   Mathf.Abs(pontosDePatrulha[pontoAtual].position.y - transform.position.y) <= 0.01f ))
                {
                    pontoAtual++;
                    if (pontoAtual >= pontosDePatrulha.Length)
                    {
                        pontoAtual = 0;
                    }
                    
                }
      
    }
}
