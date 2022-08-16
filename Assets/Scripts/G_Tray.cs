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

    public GameObject cola;
    public GameObject frenchFry;

    private bool isColaPlaced;
    private bool isFrenchFryPlaced;
    private bool isHamburgerPlaced;
    public bool isReadyToServe = false;

    public HamburgerType hamburgerType;

    // Start is called before the first frame update
    void Start()
    {
        cola.SetActive(false);
        frenchFry.SetActive(false);

        int randomNumber = Random.Range(1, 2);
        hamburgerType = (HamburgerType)randomNumber;
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
            isColaPlaced = true;
            cola.SetActive(true);
            collision.gameObject.GetComponent<G_SafeDestoy>().destroyThis();
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
