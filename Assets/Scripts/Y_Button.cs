using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Y_Button : MonoBehaviour
{
    private Button bt;
    void Start()
    {
        bt = GetComponent<Button>();
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player"){
            bt.onClick.Invoke();
        }
    }
}
