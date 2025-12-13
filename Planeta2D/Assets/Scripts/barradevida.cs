using UnityEngine;
using UnityEngine.UI;

public class barradevida : MonoBehaviour
{
    public Enemy inimigo;
    public Slider slider;
    void Start()
    {
        if (inimigo != null)
        {
            slider.maxValue = inimigo.vida;
        }
    }


    void Update()
    {
        slider.value = inimigo.vida;
        
    }
}
