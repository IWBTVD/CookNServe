using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_DoorRotation : MonoBehaviour
{
    Transform doorAxis;
    public float openSpeed = 5f;
    private bool isOpen = false;
    private bool isClosed = false;


    public Animator doorAnimator;
    private float timer;
    private float closeTime = 3.0f;

    void Start()
    {
        doorAxis = GetComponentInParent<Transform>();
    }

    public void Update()
    {
        if(isOpen)
        {
            timer += Time.deltaTime;
            if(timer >= closeTime)
            {
                timer = 0;
                isOpen = false;
                doorAnimator.SetBool("isOpen", isOpen);
            }
        }
    }

    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.tag == "Player"){
            //doRatation();
            isOpen = true;
            doorAnimator.SetBool("isOpen", isOpen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isOpen = true;
            doorAnimator.SetBool("isOpen", isOpen);
        }
    }

    private void doRatation(){
        doorAxis.localRotation = new Quaternion(0, openSpeed * 2f * Time.deltaTime, 0, 0);
    }
}
