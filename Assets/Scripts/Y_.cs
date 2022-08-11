using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_ : MonoBehaviour
{
    public GameObject Door;
    public GameObject food;
    public GameObject factory;
    public GameObject thisObject;
    private Y_DoorRotation doorRotation; 
    bool callOpen;

    private void Awake() {
        doorRotation = Door.GetComponent<Y_DoorRotation>();
    }
    void Update()
    {
        callOpen = doorRotation.callOpen;
        if(callOpen){
            if (transform.childCount < 1)
            {
                GameObject stuff = Instantiate(food);
                stuff.transform.position = factory.transform.position;
                stuff.transform.parent = thisObject.transform;
            }
        }
    }
}
