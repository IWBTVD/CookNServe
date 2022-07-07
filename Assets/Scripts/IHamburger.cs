using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHamburger
{
    public Hamburger GetHamburger();
    public void StackIngredient(Ingredient ingredient);
}

