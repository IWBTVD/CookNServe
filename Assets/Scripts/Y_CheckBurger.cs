using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CheckBurger : MonoBehaviour
{
   public List<IngredientType> recipe1 = new List<IngredientType>();

   bool isSame = false;
   private void Start() {
      planeBurger();
   }
   void planeBurger(){
      recipe1.Add(IngredientType.BurnBottom);
      recipe1.Add(IngredientType.CutletB);
      recipe1.Add(IngredientType.Cabbage);
      recipe1.Add(IngredientType.BurnTop);
   }

   private void OnTriggerEnter(Collider other) {
      if(other.tag == "Hamburger"){
         G_Hamburger enterBurger = other.GetComponent<G_Hamburger>();
         isSame = recipe1.Equals(enterBurger.stackedIngredients);
      }
   }

   private void Update() {
      if(isSame){
         Debug.Log("Same");
      }
      else{
         Debug.Log("Diffrent");
      }
   }
}
