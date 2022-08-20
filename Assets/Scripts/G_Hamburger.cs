using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class G_Hamburger : MonoBehaviourPun
{
    //재료를 담는 리스트
    public List<IngredientType> stackedIngredients = new List<IngredientType>();
    
    public float totalHeight = 0.005f;
    public Y_IngredientSound ingredientSound;
    public BoxCollider triggerCollider;
    public TextMeshPro textMesh;

    public bool isComplete = false;

    public HamburgerType hamburgerType;

    private void Start() {
        ingredientSound = GetComponent<Y_IngredientSound>();

        textMesh.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ingredient" )
        {
           ingredientSound.playSound();
        }
        if(other.gameObject.tag == "Tray")
        {
            
        }
    }

    public void StackIngredient(GameObject meshObject, float height, IngredientType ingredientType)
    {
        stackedIngredients.Add(ingredientType);
        GameObject newMeshObject = Instantiate(meshObject, transform);
        newMeshObject.transform.localPosition = new Vector3(0, totalHeight, 0);
        newMeshObject.transform.localRotation = Quaternion.identity;
        newMeshObject.transform.Rotate(new Vector3(0, Random.Range(0f, 180f), 0));
        totalHeight += height;

        triggerCollider.size = new Vector3(triggerCollider.size.x,
                                           triggerCollider.size.y + height,
                                           triggerCollider.size.z);
        triggerCollider.center = new Vector3(triggerCollider.center.x,
                                             triggerCollider.center.y + (height / 2),
                                             triggerCollider.center.z);

        CheckHamburger();
    }

    public void CheckHamburger()
    {
        //치즈버거 완성?
        if(G_CompletedHamburger.CheckHamburger(stackedIngredients, G_CompletedHamburger.cheeseBurger))
        {
            isComplete = true;
            hamburgerType = HamburgerType.CheeseBurger;
            textMesh.text = "CheeseBurger";
            textMesh.gameObject.SetActive(true);
        }

        if (G_CompletedHamburger.CheckHamburger(stackedIngredients, G_CompletedHamburger.doublePattyBurger))
        {
            isComplete = true;
            hamburgerType = HamburgerType.DoublePattyBurger;
            textMesh.text = "DoublePattyBurger";
            textMesh.gameObject.SetActive(true);
        }
        

        if(hamburgerType != HamburgerType.None && !isComplete)
        {
            hamburgerType = HamburgerType.None;
            textMesh.gameObject.SetActive(false);
        }

    }
}
