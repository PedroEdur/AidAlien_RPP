using UnityEngine;

public class PortalShow : MonoBehaviour
{
    
    public GameObject portal;

     void Start()
    {
        
    }


    void Update()
    {
        if (transform.childCount == 0)
        {
            if ( portal != null)
            {
                portal.SetActive(true);
            }
        }

    }
}
