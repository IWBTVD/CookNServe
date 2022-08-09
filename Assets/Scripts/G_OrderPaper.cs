using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class G_OrderPaper : MonoBehaviour
{
    public int orderNumber;

    public TextMeshProUGUI orderNumberText;

    void Awake()
    {
        //orderNumberText = GetComponentInChildren<TextMeshPro>();
    }

    public void SetOrderNumber(int n)
    {
        orderNumber = n;
        orderNumberText.text = orderNumber.ToString();
    }
}
