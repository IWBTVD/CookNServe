using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

using Oculus.Interaction;


public class G_Socket : MonoBehaviour
{
    public LayerMask Layer;
    public GameObject Attach;
    public Material HoverMat;
    private Material[] mat;
    private Rigidbody rig;
    public bool Freeze = true;
    public bool wasInSoket = false;

    public UnityEvent SelectEnter;
    public UnityEvent SelectExit;

    private int count = 0;
    private GameObject Target;
    private GameObject hoverObject;
    private GameObject realObject;


    private void OnTriggerStay(Collider other)
    {
        // Check Layer 
        if ((Layer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            //Debug.LogError(other.gameObject.name +" Hit with Layermask");
            Target = other.gameObject;
            HoverObject();

            //If Target Object is grabbed and not actual in soket ( it would activate itself )
            if (Target.GetComponent<OVRGrabbable>().isGrabbed && wasInSoket == true)
            {
                count = 0;
                SelectExit.Invoke();

                if (Freeze == true)
                {
                    rig.constraints = RigidbodyConstraints.None;
                }

                wasInSoket = false;
            }

            //If Target Object get released in target area
            if (!Target.GetComponent<OVRGrabbable>().isGrabbed)
            {
                PlaceAtSoket();
            }
        }
    }
    private void PlaceAtSoket()
    {
        //place the Target Object in the Socket ( attach ) 
        if (count == 0)
        {
            DestroyHoverObject();

            Target.transform.parent = Attach.transform;
            Target.transform.rotation = Attach.transform.rotation;
            Target.transform.position = Attach.transform.position;
            if (Freeze == true)
            {
                rig = Target.GetComponent<Rigidbody>();
                rig.constraints = RigidbodyConstraints.FreezePosition;
            }


            SelectEnter.Invoke();

            wasInSoket = true;
            count = 1;
        }
    }


    //Create an instance of the target Object
    private void HoverObject()
    {
        if (hoverObject == null && wasInSoket == false)
        {
            //Debug.LogError("Hover Active");
            hoverObject = PhotonNetwork.Instantiate(Target.name, Attach.transform.position, Attach.transform.rotation);
            //hoverObject = Instantiate(Target, Attach.transform.position, Attach.transform.rotation);
            hoverObject.transform.parent = Attach.transform;
            hoverObject.layer = 0;

            Rigidbody r = hoverObject.GetComponent<Rigidbody>();
            r.constraints = RigidbodyConstraints.FreezeAll;

            Collider[] cols = hoverObject.GetComponents<Collider>();

            foreach(Collider c in cols)
            {
                c.enabled = false;
            }

            //hoverObject.GetComponent<Collider>().enabled = false;

            //hoverObject.GetComponent<MeshRenderer>().material = HoverMat;


            //Replace all Materials with the hover Material
            MeshRenderer[] ren;
            ren = hoverObject.GetComponents<MeshRenderer>();
            foreach (MeshRenderer rend in ren)
            {
                var mats = new Material[rend.materials.Length];
                for (var j = 0; j < rend.materials.Length; j++)
                {
                    mats[j] = HoverMat;
                }
                rend.materials = mats;
            }
        }

    }


    private void DestroyHoverObject()
    {
        if (hoverObject)
        {
            //Debug.LogError("Hover Inactive");
            Destroy(hoverObject);

        }
    }




    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if ((Layer.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            DestroyHoverObject();

        }
    }
}