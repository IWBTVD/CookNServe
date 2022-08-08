using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_CustomerGroup : MonoBehaviour
{
    public G_CustomerAI[] customers;

    //리더가 테이블에 도착하면 나머지 고객도 앉음
    public void SitOnTheSeat(G_Seat seat)
    {
        foreach (G_CustomerAI c in customers)
        {
            if (!c.isLeader)
                c.SitAfterLeader(seat);
        }
    }

}
