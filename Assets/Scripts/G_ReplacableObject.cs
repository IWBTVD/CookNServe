using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplacableObject : MonoBehaviour
{
    public bool isReplacable = true;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isReplacable && transform.position.y > 0.3)
            transform.position = originalPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isReplacable && transform.position.y > 0.3)
            transform.position = originalPosition;
    }
}
