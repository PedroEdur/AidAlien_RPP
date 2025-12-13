using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCena : MonoBehaviour
{
  public string nomedacena;
    
    void Start()
    {
        
    }

  
    void Update()
    {
        
    }
    
    public void carregarcena()
    {
        SceneManager.LoadScene(nomedacena);
    }
}
