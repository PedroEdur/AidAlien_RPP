using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public int vida = 2;           // vida atual começa com 2
    public int vidaMaxima = 2;     // número máximo de corações é 2

    public Image[] coracao;        // arraste 2 imagens aqui
    public Sprite cheio;           // sprite do coração cheio
    public Sprite vazio;           // sprite do coração vazio

    void Start()
    {
        HealthLogic();
    }

    void Update()
    {
        // Atualiza os corações a cada frame
        HealthLogic();

        // (Opcional) Teste: perde vida com espaço, ganha com H
        if (Input.GetKeyDown(KeyCode.Space))
            vida--;
        if (Input.GetKeyDown(KeyCode.H))
            vida++;
    }

    void HealthLogic()
    {
        // Impede que passe do limite
        if (vida > vidaMaxima)
            vida = vidaMaxima;
        if (vida < 0)
            vida = 0;

        for (int i = 0; i < coracao.Length; i++)
        {
            if (i < vida)
                coracao[i].sprite = cheio;
            else
                coracao[i].sprite = vazio;

            coracao[i].enabled = i < vidaMaxima;
        }
    }
}
