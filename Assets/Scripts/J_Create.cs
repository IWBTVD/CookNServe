using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class J_Create : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject food;
    public GameObject factory;
    public GameObject thisObject;
    void Update()
    {  
        CreateFood();
    }
    void CreateFood(){
        if(transform.childCount == 0){
               GameObject stuff = Instantiate(food);
               stuff.transform.position = factory.transform.position;
               stuff.transform.parent = thisObject.transform;
        }  
    }
}
