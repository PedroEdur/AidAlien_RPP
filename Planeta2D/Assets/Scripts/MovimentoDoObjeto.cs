using UnityEngine;

public class MovimentoDoObjeto : MonoBehaviour
{
    private GravidadeDoPlaneta _gravidadeDoPlaneta; 
	
    Rigidbody2D _rigidbody2D;
    public bool noChao = false;
    
    void OnCollisionEnter2D(Collision2D colisao)
    {
        if(colisao.gameObject.CompareTag("Objeto") || colisao.gameObject.CompareTag("Abitante"))
        {
            _rigidbody2D.linearVelocity = Vector3.zero;
            _rigidbody2D.angularVelocity = 0;
        }
		
        if(colisao.gameObject.CompareTag("Planeta"))
        {
            noChao = true;
        }
			
    }
	
    void OnCollisionExit2D(Collision2D colisao)
    {
        if(colisao.gameObject.CompareTag("Planeta"))
        {
            noChao = false;
        }
    }
   
    void Start()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _gravidadeDoPlaneta = gameObject.GetComponent<GravidadeDoPlaneta>();

    }

 
    void Update()
    {
        
    }
}
