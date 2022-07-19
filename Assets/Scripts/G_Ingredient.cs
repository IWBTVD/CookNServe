using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Ingredient : MonoBehaviour
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
    private bool isGrabbed = false;   //grabbable.isGrabbed보다 한프레임 늦게 켜지고 꺼짐


    private void Start()
    {
        triggerCollider = GetComponent<MeshCollider>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        
        if (ovrGrabbable.isGrabbed)
        {
            isStackable = false;
            isGrabbed = true;
            triggerCollider.enabled = false;
        }
        //잡는 즉시 한번만 실행
        if (ovrGrabbable.isGrabbed != isGrabbed)
        {
            isGrabbed = false;
            isStackable = true;
            timer = 0f;
        }

        //손에서 놓은 후 0.5초동안 그릇에 닿으면 쌓아짐
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
    /// ????? ????? Hamburger????? ????? ????? ?????????
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hamburger" && isStackable && !isUsed)
        {
            G_Hamburger hamburger = other.GetComponent<G_Hamburger>();
            hamburger.StackIngredient(meshObject, height, ingredientType);
            isUsed = true;
            meshObject.GetComponent<RemovableMeshFilter>().RemoveMeshFilter();
            Destroy(gameObject, 2f);    
        }
        
    }
}
