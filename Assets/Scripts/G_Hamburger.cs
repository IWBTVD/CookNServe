using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_Hamburger : MonoBehaviourPun
{
    //내 주석들 돌려내!!
    //내가 건드리니까 꺠짐 ㅋㅋㅋㅋ
    public List<IngredientType> stackedIngredients = new List<IngredientType>();
    
    public float totalHeight = 0.005f;
    public Y_IngredientSound ingredientSound;
    public BoxCollider triggerCollider;

    private void Start() {
        ingredientSound = GetComponent<Y_IngredientSound>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Ingredient" )
        {
           ingredientSound.playSound();
        }
    }

    [PunRPC]
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
    }
}
