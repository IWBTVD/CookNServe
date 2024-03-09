using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FrenchFry : MonoBehaviour
{
    private OVRGrabbable grabbable;
    private G_Fryer fryer;

    private bool isCreated = true;

    public int myNumber;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<G_PhotonGrabbable>();
        fryer = FindObjectOfType<G_Fryer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isCreated)
        {
            if(grabbable.isGrabbed)
            {
                transform.parent = null;
                isCreated = false;
                if(fryer)
                    fryer.GrabFrenchFry(myNumber);
            } 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Tray")
            Destroy(gameObject);
    }


}
