using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

//트레이
public class G_Tray : MonoBehaviourPun
{
    [SerializeField]
    private TextMeshPro[] trayNumberText;
    [SerializeField]
    private TextMeshPro[] trayOrderText;    //0~1 주문번호 2~3 주문내용
    public int trayNumber;

    public GameObject cola;
    public GameObject frenchFry;

    public GameObject menuMonitor;

    public HamburgerType orderedHamburger;

    private G_Seat mySeat;

    private bool isHamburgerServed; //햄버거 놔졌는가!
    private bool isFrenchFryServed; //감튀 놔졌는가!
    private bool isColaServed;      //콜라 놔졌는가!


    // Start is called before the first frame update
    void Start()
    {
        //콜라객체랑 감튀객체 비활성화
        cola.SetActive(false);
        frenchFry.SetActive(false);
    }

    //OrderPaper에서 사용됨
    public void SetTrayNumber(int n, G_Seat seat)
    {
        //햄버거 랜덤으로 선정
        //orderedHamburger = (HamburgerType)Random.Range(1, 2);

        //테스트용
        orderedHamburger = (HamburgerType)n;

        //트레이 번호는 매개변수값으로
        trayNumber = n;
        GetComponentInChildren<G_Hamburger>().SetDishNumber(trayNumber);
        //트레이 깃발 글자 변경
        trayNumberText[0].text = trayNumber.ToString();
        trayNumberText[1].text = trayNumber.ToString();

        trayOrderText[0].text = "Order " + trayNumber.ToString();
        trayOrderText[1].text = "Order" + trayNumber.ToString();

        SetTrayText();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Cola")
        {
            photonView.RPC(nameof(PlaceCola), RpcTarget.All);
        }
        else if(collision.transform.tag == "FrenchFries")
        {
            photonView.RPC(nameof(PlaceFrenchFry), RpcTarget.All);
        }

        if(collision.transform.tag == "Seat")
        {
            G_Seat seat = collision.transform.GetComponent<G_Seat>();
            //서빙 조건 만족
            if(isColaServed && isFrenchFryServed && isHamburgerServed && trayNumber == seat.seatNumber)
            {
                menuMonitor.SetActive(false);
                GetComponent<Rigidbody>().isKinematic = true;
                seat.ServedFood(gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //if(collision.transform.tag == "Hamburger")
        //{
        //    //그릇 제거하면 안되게만들어야댐
        //    if (collision.gameObject.GetComponent<G_Hamburger>().hamburgerType == orderedHamburger)
        //    {
        //        isHamburgerServed = false;
        //        SetTrayText();
        //    }
        //}
        
    }

    [PunRPC]
    public void PlaceCola()
    {
        cola.SetActive(true);
        isColaServed = true;
        SetTrayText();
    }

    [PunRPC]
    public void PlaceFrenchFry() {
        frenchFry.SetActive(true);
        isFrenchFryServed = true;
        SetTrayText();
    }
    
    public void SetTrayText()
    {
        string orderDetailText = orderedHamburger.ToString() + " " + (isHamburgerServed ? "O" : "X")
                                +"\n French Fry " + (isFrenchFryServed ? "O" : "X")
                                +"\n Drink " + (isColaServed? "O" : "X");
        trayOrderText[2].text = orderDetailText;
        trayOrderText[3].text = orderDetailText;
    }

    public void ReceiveHamburger(GameObject obj)
    {
        G_Hamburger h = obj.GetComponentInChildren<G_Hamburger>();
        if(orderedHamburger == h.hamburgerType)
        {
            photonView.RPC(nameof(PlaceHamburger), RpcTarget.AllBuffered, true);
        }
    }

    [PunRPC]
    public void PlaceHamburger(bool b)
    {
        isHamburgerServed = b;
        SetTrayText();
    }
}
