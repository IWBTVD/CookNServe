using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Ingredient : MonoBehaviour
{
    //한글 주석 왜깨짐 ㅡㅡ
    public IngredientType ingredientType;
    //public bool isUsed = false;

    public float height;
    public bool isUsed = false;

    public GameObject meshObject;

    private MeshCollider triggerCollider;
    private OVRGrabbable ovrGrabbable;

    private float timer;
    private float stackableTime = 0.5f;
    private bool isStackable = false;
    private bool isGrabbed = false;   //????? ????????? true

    private void Start()
    {
        triggerCollider = GetComponent<MeshCollider>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        //??????????? ????????
        if (ovrGrabbable.isGrabbed)
        {
            isStackable = false;
            isGrabbed = true;
            triggerCollider.enabled = false;
        }
        //????? ??????? ??????????, ????°? ???
        if(ovrGrabbable.isGrabbed != isGrabbed)
        {
            isGrabbed = false;
            isStackable = true;
            timer = 0f;
        }

        //????? ???? ?? 0.5???? ????? ?????? ????? ?? ?ð? ?????? ???????
        if(isStackable)
        {
            timer += Time.deltaTime;
            if (timer >= stackableTime)
            {
                isStackable = false;
                timer = 0f;
            }   
        }
    }

    /// <summary>
    /// ?浹?? ????? Hamburger?±?? ??´? ????? ?????????
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hamburger" && isStackable && !isUsed)
        {
            G_Hamburger hamburger = other.GetComponent<G_Hamburger>();
            hamburger.StackIngredient(meshObject, height, ingredientType);
            isUsed = true;
            meshObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
    }
}
