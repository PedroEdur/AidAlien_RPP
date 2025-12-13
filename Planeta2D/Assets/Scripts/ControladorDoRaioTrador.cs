using UnityEngine;

public class ControladorDoRaioTrador : MonoBehaviour
{
	public GameObject raioTrator;
	public KeyCode inPutRaioTrator = KeyCode.Z;
    
    void Start()
    {
        
    }

  
    void Update()
    {
	    if(Input.GetKey(inPutRaioTrator))
	    {
		    raioTrator.SetActive(true);
	    }
	    else
	    {
	    	raioTrator.SetActive(false);
	    }
    }
}
