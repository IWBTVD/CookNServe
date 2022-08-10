using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class G_Tray : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshPro[] trayNumberText;
    public int trayNumber;

    //0: 햄버거, 1: 감튀 2: 콜라
    public Transform[] foodTransforms;


    // Start is called before the first frame update
    void Start()
    {
        
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
}
