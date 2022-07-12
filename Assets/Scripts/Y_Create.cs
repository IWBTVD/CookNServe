using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_Create : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject food;
    public GameObject factory;
    public GameObject thisObject;
    public int numOfFood;
    void Update()
    {  
        CreateFood();
    }

    void CreateFood(){
        Invoke("DoCreate", 2f);
    }

    public void DoCreate()
    {
        if (transform.childCount < numOfFood)
        {
            GameObject stuff = Instantiate(food);
            stuff.transform.position = factory.transform.position;
            stuff.transform.parent = thisObject.transform;
        }
    }
}
