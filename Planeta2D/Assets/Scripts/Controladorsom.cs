using UnityEngine;
using UnityEngine.UI;

public class Controladorsom : MonoBehaviour
{
    private bool estadoSom = true;

    [SerializeField] private AudioSource fundoMusical;

    [SerializeField] private Sprite somLigadoSprite;
    [SerializeField] private Sprite somDesLigadoSprite;

    [SerializeField] private Image muteImage;

    public void LigarDesligarsom()
    {
        estadoSom = !estadoSom;

        // Liga/desliga o áudio
        fundoMusical.enabled = estadoSom;

        // Troca o ícone
        if (estadoSom)
        {
            muteImage.sprite = somLigadoSprite;
        }
        else
        {
            muteImage.sprite = somDesLigadoSprite;
        }
    }

    public void VolumeMusical(float value)
    {
        fundoMusical.volume = value;
    }
}