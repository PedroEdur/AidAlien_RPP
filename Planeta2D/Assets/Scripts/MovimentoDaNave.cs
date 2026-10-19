
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

    [SerializeField]
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
        Debug.DrawCircle(eixoDaHorbita.transform.position, distanciaMinimaDoPlaneta, 32, Color.red);

        distanciaDoEixoDeRotacao = Vector2.Distance(transform.position, eixoDaHorbita.transform.position);

        posicaoDaNave = transform.position;
        posicaoDoEixoDaHorbita = eixoDaHorbita.position;

        distanciaDoEixo = Vector2.Distance(posicaoDaNave, posicaoDoEixoDaHorbita);

        if (rotacaoNaHorbita == false && naOrbita)
        {
            if (Input.GetKey(inPutRigth))
            {
                transform.RotateAround(eixoDaHorbita.transform.position, Vector3.forward, -velocidadeDaHorbita * Time.deltaTime);
            }

            if (Input.GetKey(inPutLeft))
            {
                transform.RotateAround(eixoDaHorbita.transform.position, Vector3.forward, velocidadeDaHorbita * Time.deltaTime);
            }
        }

        if (distanciaDoEixoDeRotacao <= distanciaMinimaDoPlaneta)
        {
            if (naOrbita == false)
            {
                naOrbita = true;
            }

            if (naOrbita == true)
            {
                transform.position = posicaoAntiga;
            }

            transform.rotation = Quaternion.LookRotation(Vector3.forward, transform.position - eixoDaHorbita.transform.position);
        }
        else
        {
            posicaoAntiga = transform.position;
        }

        if (Input.GetKeyDown(inPutJump) && naOrbita)
        {
            naOrbita = false;
        }

        if (naOrbita == false)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);

            if (Input.GetKey(inPutRigth) && transform.position.x < 8.6f)
            {
                transform.position += Vector3.right * velocidadeDaNave * Time.deltaTime;
            }

            if (Input.GetKey(inPutLeft) && transform.position.x > -8.6f)
            {
                transform.position += Vector3.left * velocidadeDaNave * Time.deltaTime;
            }

            if (Input.GetKey(inPuUp) && transform.position.y < 4.6f)
            {
                transform.position += Vector3.up * velocidadeDaNave * Time.deltaTime;
            }

            if (Input.GetKey(inPuDown) && transform.position.y > -4.6f)
            {
                transform.position += Vector3.down * velocidadeDaNave * Time.deltaTime;
            }
        }
    }
}
