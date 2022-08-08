using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Seat : MonoBehaviour
{
    public bool isUsing = false;

    public Transform[] seatTransform;
    public Collider tableEntrance;
    public Transform orderPaperTransform;


    public Transform GetThisTableEntrance()
    {
        return tableEntrance.transform;
    }

    public Transform GetSeat(int seatNumber)
    {
        return seatTransform[seatNumber];
    }
}
