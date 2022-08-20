    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_DoorRotation : MonoBehaviour
{
    Transform doorAxis;
    public float openSpeed = 5f;
    public bool isOpen = false;
    public bool callOpen = false;
    
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
    private void OnCollisionEnter(Collision other) {//삭제하면 안됨
        if (other.gameObject.tag == "Player")
        {
            Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - this.transform.position, this.transform.up);
            Debug.Log(targetRotation);
            if(targetRotation.x > 0){
                isOpen = true;
                doorAnimator.SetBool("isOpen", isOpen);
                doorAnimator.SetFloat("Move", 1.0f);
            }
            if(targetRotation.x < 0){
                isOpen = true;
                doorAnimator.SetBool("isOpen", isOpen);
                doorAnimator.SetFloat("Move", -1.0f);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //충돌방향체크
            Quaternion targetRotation = Quaternion.LookRotation(other.transform.position - this.transform.position, this.transform.up);
            Debug.Log(targetRotation);
            if(targetRotation.x > 0){
                isOpen = true;
                doorAnimator.SetBool("isOpen", isOpen);
                doorAnimator.SetFloat("Move", 1.0f);
                Debug.Log("Foward");
            }
            if(targetRotation.x < 0){
                isOpen = true;
                doorAnimator.SetBool("isOpen", isOpen);
                doorAnimator.SetFloat("Move", -1.0f);
                Debug.Log("Backward");
            }
        }
    }
    
}
