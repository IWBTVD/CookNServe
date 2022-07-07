using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IHamburger
{
    public enum IngredientType
    {
        Cabbage,
        Patty,
        BurnTop,
        BurnBottom,
    }

    public IngredientType ingredientType;
    //public bool isUsed = false;
    public Collider triggerCollider;

    public float height;

    private Rigidbody rigid;
    private FixedJoint fixedJoint;

    public void Start()
    {
        rigid = GetComponent<Rigidbody>();
        fixedJoint = GetComponent<FixedJoint>();
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Hamburger")
        {
            //�ܹ��� ��ü�κ��� ������Ʈ ������
            Hamburger hamburger = other.GetComponent<IHamburger>().GetHamburger();
            //�ܹ��ſ��� ��� �׾ƿ÷��� �� �޼ҵ� ȣ��
            other.GetComponent<IHamburger>().StackIngredient(this);

            //�ڽ� ��ü�� ����ϰ� ��ġ�� �ܹ��� ��ü�� ���� �ø�
            transform.parent = other.transform;

            //�ܹ��� ��ġ�� �°� ��ġ �̵�
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            transform.position += Vector3.up * hamburger.GetTopPosition();

            //���� ������ ��� ��ġ ����
            hamburger.NextTopPosition(height);

            fixedJoint.connectedBody = hamburger.GetRigidbody();
            transform.tag = "Hamburger";

            GetComponent<OVRGrabbable>().SetOffhandGrab(false);
            triggerCollider.enabled = false;
        }
    }

    /// <summary>
    /// �ܹ��� ���� ���� �� ��� ���� ���ο� ��ᰡ �ö����, �ڽ��� �θ� �ܹ��� ��ü�� ����
    /// </summary>
    /// <returns></returns>
    public Hamburger GetHamburger()
    {
        return transform.parent.GetComponent<Hamburger>();
    }

    public void StackIngredient(Ingredient ingredient)
    {
        transform.parent.GetComponent<Hamburger>().StackIngredient(ingredient);
    }
}
