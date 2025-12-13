using UnityEngine;

public class CollectibleAnimation : MonoBehaviour
{
    public float rotationSpeed = 100f; // velocidade de rotação
    public float pulseSpeed = 2f;      // velocidade de pulsação
    public float pulseAmount = 0.1f;   // quanto o objeto pulsa

    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;
    }

    void Update()
    {
        // Roda o objeto no eixo Y (bom para 3D, moedas, etc)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        // Anima um "pulsar" (boa para 2D)
        float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;
        transform.localScale = startScale * scale;
    }
}