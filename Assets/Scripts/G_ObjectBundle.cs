using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ObjectBundle : MonoBehaviour
{
    public GameObject prefab;

    public Transform spawnPoint;

    private void Start()
    {
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }

    public void GrabbedCreatedObject()
    {
        Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
}
