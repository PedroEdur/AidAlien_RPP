using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class Pontuacao : MonoBehaviour
{
    public TMPro.TextMeshProUGUI texto;

    public int pontos = 0;
    
    void Start()
    {
        
    }


    void Update()
    {
        texto.text = pontos.ToString();
        Debug.Log(pontos);
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("coletavel"))
        {
            pontos += collision.gameObject.GetComponent<Moeda>().valor;
            //collision.gameObject.SetActive(false);

            Destroy(collision.gameObject);
        }

    }

}


