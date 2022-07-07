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
    private bool isGrabbed = false;   //�տ��� ��������� true

    private void Start()
    {
        triggerCollider = GetComponent<MeshCollider>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    private void Update()
    {
        //����������� �ȴ޶�ٰ���
        if (ovrGrabbable.isGrabbed)
        {
            isStackable = false;
            isGrabbed = true;
            triggerCollider.enabled = false;
        }
        //�տ��� ���ڸ��� Ÿ�̸��۵��ϰ�, �޶�ٴ°� ���
        if(ovrGrabbable.isGrabbed != isGrabbed)
        {
            isGrabbed = false;
            isStackable = true;
            timer = 0f;
        }

        //�տ��� ���� �� 0.5�ʾȿ� �׸��� ������ �ٵ��� �� �ð� ������ �ȴ޶����
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
    /// �浹�� ��ü�� Hamburger�±׸� �ٴ´� ���ǵ� �����ؾ���
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
