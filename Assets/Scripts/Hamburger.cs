using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hamburger : MonoBehaviour, IHamburger
{
    public List<Ingredient> ingredients = new List<Ingredient>();

    private Rigidbody rigid;

    public float nextStackPosition = 0.01f;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    public void StackIngredient(Ingredient ingredient) 
    {
        ingredients.Add(ingredient);
    }

    public void NextTopPosition(float f)
    {
        nextStackPosition += f;
    }

    public float GetTopPosition()
    {
        return nextStackPosition;
    }

    public int GetIngredientLength()
    {
        return ingredients.Count;
    }

    public Rigidbody GetRigidbody()
    {
        return rigid;
    }

    public Hamburger GetHamburger()
    {
        return this;
    }
}
