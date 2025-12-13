using UnityEngine;

public class RaioTrador : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("coletavel"))
		{
			// Desliga gravidade do item
			var gravidade = collision.GetComponent<GravidadeDoPlaneta>();
			if (gravidade != null)
				gravidade.gravidade = false;

			// Reproduz som do item
			var audio = collision.GetComponent<AudioSource>();
			if (audio != null)
				audio.Play();
		}
		
		if (collision.CompareTag("Slot"))
		{
			Debug.Log("Slot");
			collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
			collision.gameObject.transform.GetChild(0).gameObject.SetActive(true);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("coletavel"))
		{
			// Religa gravidade quando sair do raio
			var gravidade = collision.GetComponent<GravidadeDoPlaneta>();
			if (gravidade != null)
				gravidade.gravidade = true;
		}

	}
}


