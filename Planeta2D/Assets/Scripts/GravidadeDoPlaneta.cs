using UnityEngine;

public class GravidadeDoPlaneta : MonoBehaviour
{
	public GameObject planeta;
	public bool gravidade = true;
	
	private float distancia;
	
	
	private Rigidbody2D _rigidbody2D;
	
	public float forcaDaGravidade;
	
	public float distanciaDaGravidade;
	
	private  Vector3 vertorDeForca;
	
	private float anguloDeVisao;
	
	public MovimentoDoAbitante _movimentoDoAbitante;
	public MovimentoDoObjeto _movimentoDoObjeto;
	
    void Start()
    {
	    _rigidbody2D = GetComponentInParent<Rigidbody2D>();
	    _movimentoDoAbitante = GetComponent<MovimentoDoAbitante>();
	    _movimentoDoObjeto = GetComponent<MovimentoDoObjeto>();
    }
    
    void Update()
	{
		if (_movimentoDoAbitante != null)
		{ 
			
			if (_movimentoDoAbitante.noChao)
					{
						_rigidbody2D.linearVelocity = Vector3.zero;
						_rigidbody2D.angularVelocity = 0;
					}


			if (gravidade == true && _movimentoDoAbitante.noChao == false)
			{

				//rotaciona o objeto para centro do planeta
				//transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - planeta.transform.position);
				anguloDeVisao = 90 + Mathf.Atan2(vertorDeForca.y, vertorDeForca.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, 0f, anguloDeVisao);


				distancia = Vector3.Distance(gameObject.transform.position, planeta.transform.position);

				if (distancia <= distanciaDaGravidade)
				{

					vertorDeForca = planeta.transform.position - transform.position;

				
					_rigidbody2D.AddForce(vertorDeForca.normalized * (1.0f - distancia / distanciaDaGravidade) *
					                      forcaDaGravidade);

				}
				else
				{
					//remove as forcas aplcadas ao objeto
					_rigidbody2D.linearVelocity = new Vector3(0, 0, 0);
				}

			}
			else
			{
				if (distancia <= distanciaDaGravidade)
				{

					vertorDeForca = planeta.transform.position - transform.position;
					//inverte a gravidade
					_rigidbody2D.AddForce(vertorDeForca.normalized * (1.0f - distancia / distanciaDaGravidade) *
						-forcaDaGravidade / 2);

				}
			}
		}
		else
		{

			if (_movimentoDoObjeto != null)
			{


				if (_movimentoDoObjeto.noChao)
				{
					_rigidbody2D.linearVelocity = Vector3.zero;
					_rigidbody2D.angularVelocity = 0;
				}

				if (gravidade == true)
				{

					//rotaciona o objeto para centro do planeta
					//transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - planeta.transform.position);
					anguloDeVisao = 90 + Mathf.Atan2(vertorDeForca.y, vertorDeForca.x) * Mathf.Rad2Deg;
					transform.rotation = Quaternion.Euler(0f, 0f, anguloDeVisao);


					distancia = Vector3.Distance(gameObject.transform.position, planeta.transform.position);

					if (distancia <= distanciaDaGravidade)
					{

						vertorDeForca = planeta.transform.position - transform.position;
						_rigidbody2D.AddForce(vertorDeForca.normalized * (1.0f - distancia / distanciaDaGravidade) *
						                      forcaDaGravidade);

					}
					else
					{
						//remove as forcas aplcadas ao objeto
						_rigidbody2D.linearVelocity = new Vector3(0, 0, 0);
					}

				}
				else
				{
					if (distancia <= distanciaDaGravidade)
					{

						vertorDeForca = planeta.transform.position - transform.position;
						//inverte a gravidade
						_rigidbody2D.AddForce(vertorDeForca.normalized * (1.0f - distancia / distanciaDaGravidade) *
							-forcaDaGravidade / 2);

					}
				}
			}

		}
	}
}
