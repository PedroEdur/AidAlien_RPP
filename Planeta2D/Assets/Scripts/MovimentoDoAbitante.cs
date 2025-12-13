using UnityEngine;

public class MovimentoDoAbitante : MonoBehaviour
{
	private GravidadeDoPlaneta _gravidadeDoPlaneta; 
	
	Rigidbody2D _rigidbody2D;
	public bool andando = true;
	public bool noChao = false;
	public float velocidade = 1;

	private SpriteRenderer _spriteRenderer;
	
	
	public enum Sentido {Horario, AntiHorario};
	public Sentido sentidoDoMovimento = Sentido.Horario;
	
	void OnCollisionEnter2D(Collision2D colisao)
	{
        if (colisao.gameObject.CompareTag("Objeto") 
            || colisao.gameObject.CompareTag("Abitante") 
            || colisao.gameObject.CompareTag("coletavel")
            || colisao.gameObject.CompareTag("Slot")
            || colisao.gameObject.CompareTag("Lenhador"))
		{
			_rigidbody2D.linearVelocity = Vector3.zero;
			_rigidbody2D.angularVelocity = 0;
			//_rigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
			
			switch(sentidoDoMovimento)
			{
			case Sentido.Horario:
				sentidoDoMovimento = Sentido.AntiHorario;
				break;
			
			case Sentido.AntiHorario:
				sentidoDoMovimento = Sentido.Horario;
				break;
			}
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
	    _spriteRenderer = GetComponent<SpriteRenderer>();

    }

 
    void Update()
    {
        
	    if(andando == true && noChao == true && _gravidadeDoPlaneta.gravidade == true)
	    {
        
    	 if(sentidoDoMovimento == Sentido.Horario)
    		{
     		transform.Translate( Vector2.right * (Time.deltaTime * velocidade), Space.Self);
	        _spriteRenderer.flipX = false;
    		 }
     
		 if(sentidoDoMovimento == Sentido.AntiHorario)
    		{
            transform.Translate( Vector2.left * (Time.deltaTime * velocidade), Space.Self);
            _spriteRenderer.flipX = true;
    		}
        
	    }
	    
	    
    }
}
