using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_DoorRotation : MonoBehaviour
{
    public bool isOpen = false;
    public Animator doorAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

    public void OnCloseAnd()
    {
        isOpen = false;
        doorAnimator.SetBool("isOpen", isOpen);
        doorAnimator.SetFloat("Move", 0);
    }
}
