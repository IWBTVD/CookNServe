using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
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
    private bool isGrabbed = false;   //손에서 잡고있을때 true

    private void Start()
    {
        triggerCollider = GetComponent<MeshCollider>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        //잡고있을때는 안달라붙게함
        if (ovrGrabbable.isGrabbed)
        {
            isStackable = false;
            isGrabbed = true;
            triggerCollider.enabled = false;
        }
        //손에서 놓자마자 타이머작동하고, 달라붙는거 허용
        if(ovrGrabbable.isGrabbed != isGrabbed)
        {
            isGrabbed = false;
            isStackable = true;
            timer = 0f;
        }

        //손에서 놓은 후 0.5초안에 그릇에 닿으면 붙도록 함 시간 지나면 안달라붙음
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
    /// 충돌한 객체가 Hamburger태그면 붙는다 조건도 만족해야함
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hamburger" && isStackable && !isUsed)
        {
            Hamburger hamburger = other.GetComponent<Hamburger>();
            hamburger.StackIngredient(meshObject, height, ingredientType);

            isUsed = true;
            meshObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
