using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_IngredientPack : MonoBehaviour
{
    private OVRGrabbable grabbable;
    private G_IngredientSpawner spawner;

    public bool isCreated = false;

    public int myNumber;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<G_PhotonGrabbable>();
        spawner = transform.parent.GetComponent<G_IngredientSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isCreated)
        {
            if (grabbable.isGrabbed)
            {
                transform.parent = null;
                isCreated = false;
                spawner.CreateIngredient(myNumber);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
