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
            //햄버거 객체로부터 컴포넌트 가져옴
            Hamburger hamburger = other.GetComponent<IHamburger>().GetHamburger();
            //햄버거에서 재료 쌓아올렸을 때 메소드 호출
            other.GetComponent<IHamburger>().StackIngredient(this);

            //자식 객체로 등록하고 위치를 햄버거 객체의 위로 올림
            transform.parent = other.transform;

            //햄버거 위치에 맞게 위치 이동
            transform.position = other.transform.position;
            transform.rotation = other.transform.rotation;
            transform.position += Vector3.up * hamburger.GetTopPosition();

            //다음 스택의 재료 위치 설정
            hamburger.NextTopPosition(height);

            fixedJoint.connectedBody = hamburger.GetRigidbody();
            transform.tag = "Hamburger";

            GetComponent<OVRGrabbable>().SetOffhandGrab(false);
            triggerCollider.enabled = false;
        }
    }

    /// <summary>
    /// 햄버거 위에 얹힌 이 재료 위에 새로운 재료가 올라오면, 자신의 부모 햄버거 객체를 리턴
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
