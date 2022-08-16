using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class G_CompletedHamburger
{
    public static IngredientType[] cheeseBurger = {IngredientType.BurnBottom,
                                                   IngredientType.Cabbage,
                                                   IngredientType.Tomato,
                                                   IngredientType.Cheese,
                                                   IngredientType.CookedCutletB,
                                                   IngredientType.BurnTop,
                                                    };

    public static IngredientType[] doublePattyBurger = {IngredientType.BurnBottom,
                                                         IngredientType.Cabbage,
                                                         IngredientType.Cheese,
                                                         IngredientType.CookedCutletB,
                                                         IngredientType.BurnBottom,
                                                         IngredientType.CookedCutletB,
                                                         IngredientType.Mushroom,
                                                         IngredientType.BurnTop,
                                                    };

    /// <summary>
    /// 버거가 제대로 쌓였는지 검사하는 함수
    /// </summary>
    /// <param name="ingList">내가 쌓은 버거의 리스트</param>
    /// <param name="burgerArray">만족해야하는 버거 리스트</param>
    /// <returns></returns>
    public static bool CheckBurger(List<IngredientType> ingList, IngredientType[] burgerArray)
    {
        int i = 0;
        int correctNumber = 0;
        foreach(IngredientType ingredient in ingList)
        {
            //검사 횟수가 버거배열 크기보다 커지면 실패
            if (i > burgerArray.Length - 1)
                return false;
            if(ingredient == burgerArray[i])
            {
                correctNumber++;
            }
            else
            {
                correctNumber = 0;
            }
            i++;
        }

        if (correctNumber == burgerArray.Length)
            return true;

        else return false;
    }
}
