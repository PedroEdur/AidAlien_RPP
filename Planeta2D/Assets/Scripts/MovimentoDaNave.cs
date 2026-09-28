using UnityEngine;

public class MovimentoDaNave : MonoBehaviour
{
	public KeyCode inPutLeft = KeyCode.LeftArrow;
	public KeyCode inPutRigth = KeyCode.RightArrow;
	public KeyCode inPuUp = KeyCode.UpArrow;
	public KeyCode inPuDown = KeyCode.DownArrow;
	
	public KeyCode inPutJump = KeyCode.Space;
	
	public Transform eixoDaHorbita;
	public float distanciaDoEixo;
	
	public float velocidadeDaHorbita = 100f;
	public bool rotacaoNaHorbita = false;

	public float velocidadeDaNave = 10f;
	
	public bool naOrbita = false;
	public float distanciaMinimaDoPlaneta = 5;
	
	[ SerializeField ]
	private float distanciaDoEixoDeRotacao;
	
	private Vector2 posicaoDaNave;
	private Vector2 posicaoDoEixoDaHorbita;
	private Vector3 posicaoAntiga;

    void Start()
    {
	    posicaoAntiga = transform.position;
    }

    void Update()
	{
		//desenha a horbita do planeta
		Debug.DrawCircle(eixoDaHorbita.transform.position, distanciaMinimaDoPlaneta, 32, Color.red);
		
		//distancia 2D entre dosi pontos
		distanciaDoEixoDeRotacao = Vector2.Distance(transform.position,eixoDaHorbita.transform.position);
		
		posicaoDaNave = transform.position;
		posicaoDoEixoDaHorbita = eixoDaHorbita.position;
		
		distanciaDoEixo = Vector2.Distance( posicaoDaNave,posicaoDoEixoDaHorbita);
		
		// refazer a função RotateAround() utilzando calculos matematicos e esplicar o passo a passo do funcionamento
		
		//controle de movumento na orbirta do planeta
		if(rotacaoNaHorbita  == false && naOrbita)
		{
    	
    	
			if(Input.GetKey(inPutRigth))
			{
				this.transform.RotateAround(eixoDaHorbita.transform.position,  Vector3.forward, -velocidadeDaHorbita * Time.deltaTime);
			}
		
			if(Input.GetKey(inPutLeft))
			{
				this.transform.RotateAround(eixoDaHorbita.transform.position,  Vector3.forward, velocidadeDaHorbita * Time.deltaTime);
			}
		
		}
		
		
		//sinaliza se o planeta está orbita do planeta
		if(distanciaDoEixoDeRotacao <= distanciaMinimaDoPlaneta)
		{
			if(naOrbita == false)
			{
				naOrbita = true;
			}
			
			if(naOrbita == true)
			{
				transform.position = posicaoAntiga;
			}
			
			//Aponte Para O Eixo De Rotacao
			transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - eixoDaHorbita.transform.position);
		}
		else
		{
		 posicaoAntiga = transform.position;
		 //naOrbita = false;
		}
		
		//deixar hortbita
		if(Input.GetKeyDown(inPutJump) && naOrbita)
		{
			naOrbita = false;
		}
		
		//controle da nave fora da orbita do planeta
		if(naOrbita == false)
		{
			transform.rotation = Quaternion.EulerAngles(0,0,0);
			
			
			if(Input.GetKey(inPutRigth)  && transform.position.x < 8.6f)
			{
				this.transform.position += Vector3.right * velocidadeDaNave * Time.deltaTime;
			}
		
			if(Input.GetKey(inPutLeft) && transform.position.x > -8.6f)
			{
				this.transform.position += Vector3.left * velocidadeDaNave * Time.deltaTime;
			}
			
			if(Input.GetKey(inPuUp) && transform.position.y < 4.6f)
			{
				this.transform.position += Vector3.up * velocidadeDaNave * Time.deltaTime;
			}
		
			if(Input.GetKey(inPuDown) && transform.position.y > - 4.6f)
			{
				this.transform.position +=  Vector3.down * velocidadeDaNave * Time.deltaTime;
			}
			
		}
		
		
	}
	
    
}