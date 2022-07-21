using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_RemovableMeshFilter : MonoBehaviour
{
    public MeshFilter[] meshfilters = new MeshFilter[1];

    // Start is called before the first frame update
    void Start()
    {
        if (meshfilters[0] == null)
        {
            meshfilters[0] = GetComponent<MeshFilter>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveMeshFilter()
    {
        foreach (var mesh in meshfilters)
        {
            mesh.mesh = null;
        }
    }
}
