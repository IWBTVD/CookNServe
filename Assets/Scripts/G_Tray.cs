using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class G_Tray : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshPro[] trayNumberText;
    [SerializeField]
    private TextMeshPro[] trayOrderText;
    public int trayNumber;

    //0: 햄버거, 1: 감튀 2: 콜라
    public Transform[] foodTransforms;

    public GameObject cola;
    public GameObject frenchFry;


    // Start is called before the first frame update
    void Start()
    {
        cola.SetActive(false);
        frenchFry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTrayNumber(int n)
    {
        trayNumber = n;
        trayNumberText[0].text = trayNumber.ToString();
        trayNumberText[1].text = trayNumber.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Cola")
        {
            cola.SetActive(true);
            cola.GetComponent<G_SafeDestroy>().destroyThis();
        }
        else if(collision.transform.tag == "FrenchFries")
        {
            frenchFry.SetActive(true);
        }
    }

    public void PlaceDish(GameObject dish)
    {
        dish.transform.parent = transform;
    }
}
