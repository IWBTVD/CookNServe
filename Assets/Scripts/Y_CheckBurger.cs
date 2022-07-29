using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CheckBurger : MonoBehaviour
{

   public List<IngredientType> recipe1 = new List<IngredientType>();
   private bool isSame = true;
   int numOfRecipe , i;
   private void Start() {
      int num = Random.Range(0,2);
      switch (num){
         case 0:
            BurgerNum1();
            Debug.Log("BurgerNum1");
            break;
         case 1:
            BurgerNum2();
            Debug.Log("BurgerNum2");
            break;
         case 2:
            BurgerNum3();
            Debug.Log("Burgernum3");
            break;
         default:
            break;
      }
      numOfRecipe = recipe1.Count;
      Debug.Log(numOfRecipe);
   }
   private void OnTriggerEnter(Collider other) {
      if(other.tag == "Hamburger"){
         G_Hamburger enterBurger = other.GetComponent<G_Hamburger>();
         int numOfEnterBurger = enterBurger.stackedIngredients.Count;
         Debug.Log(numOfEnterBurger);
         
         if(numOfRecipe != numOfEnterBurger){
            isSame = false;
         }
         if(numOfRecipe == numOfEnterBurger){
            for(i = 0; i < numOfEnterBurger; i++){
               if(!recipe1[i].Equals(enterBurger.stackedIngredients[i])){
                  isSame = false;
               }
               else
                  isSame = true;
            } 
         }
         
         if(isSame){
            Debug.Log("Same");
         }
         else{
            Debug.Log("Diffrent");
         }
      }
   }
   
   void BurgerNum1(){
      recipe1.Add(IngredientType.BurnBottom);
      recipe1.Add(IngredientType.CookedCutletB);
      recipe1.Add(IngredientType.Cabbage);
      recipe1.Add(IngredientType.BurnTop);
   }
   void BurgerNum2(){
      recipe1.Add(IngredientType.BurnBottom);
      recipe1.Add(IngredientType.CookedCutletB);
      recipe1.Add(IngredientType.Cheese);
      recipe1.Add(IngredientType.Cabbage);
      recipe1.Add(IngredientType.BurnTop);
   }
   void BurgerNum3(){
      recipe1.Add(IngredientType.BurnBottom);
      recipe1.Add(IngredientType.CookedCutletB);
      recipe1.Add(IngredientType.Cabbage);
      recipe1.Add(IngredientType.Mushroom);
      recipe1.Add(IngredientType.BurnTop);
   }
}
