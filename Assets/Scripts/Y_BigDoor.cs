using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_BigDoor : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player" || other.tag == "Customer"){
            animator.SetBool("isOpen",true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player" || other.tag == "Customer"){
            animator.SetBool("isOpen",false);
        }
    }
}
