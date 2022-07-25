using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FixedRotation : MonoBehaviour
{
    public Transform ovrCameraRig;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ovrCameraRig.position;
    }

}
