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

    public HamburgerType hamburgerType = HamburgerType.None;

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
        if(G_CompletedHamburger.CheckBurger(stackedIngredients, G_CompletedHamburger.cheeseBurger))
        {
            textMesh.gameObject.SetActive(true);
            textMesh.text = "CheeseBurger";
        }

        if (G_CompletedHamburger.CheckBurger(stackedIngredients, G_CompletedHamburger.doublePattyBurger))
        {
            textMesh.gameObject.SetActive(true);
            textMesh.text = "DoublePattyBurger";
        }
    }

}
