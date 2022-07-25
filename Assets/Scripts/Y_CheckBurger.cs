using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CheckBurger : MonoBehaviour
{
   public List<IngredientType> recipe1 = new List<IngredientType>(new IngredientType[]{});

   private bool isSame = false;

   private void OnTriggerEnter(Collider other) {
      if(other.tag == "Hamburger"){
         G_Hamburger enterBurger = other.GetComponent<G_Hamburger>();
         isSame = recipe1.Equals(enterBurger.stackedIngredients);
         if(isSame){
            Debug.Log("Same");
         }
         else{
            Debug.Log("Diffrent");
         }
      }
   }


}
