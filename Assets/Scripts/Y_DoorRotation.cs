using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_DoorRotation : MonoBehaviour
{
    // Start is called before the first frame update
    Transform doorAxis;
    public float openSpeed = 5f;
    private bool isOpen = false;
    private bool isClosed = false;

    void Start()
    {
        doorAxis = gameObject.GetComponentInParent<Transform>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            doRatation();
        }
    }
    private void doRatation(){
        doorAxis.eulerAngles = new Vector3(0,openSpeed * 2f * Time.deltaTime,0);
    }
}
