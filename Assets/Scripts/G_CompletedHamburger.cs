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
    /// 햄버거 완성됐는지 검사하는거
    /// </summary>
    /// <param name="list">그릇에 담긴 재료리스트</param>
    /// <param name="hamburgerArray">햄버거 완성 조건</param>
    /// <returns></returns>
    public static bool CheckHamburger(List<IngredientType> list, IngredientType[] hamburgerArray)
    {
        int i = 0;
        int rightIngredientNum = 0;
        foreach (IngredientType ing in list)
        {
            //햄버거 배열보다 i값이 크면 많이쌓은거라서 아님
            if (i > hamburgerArray.Length - 1) return false;
            else if(ing == hamburgerArray[i++])
            {
                rightIngredientNum++;
            }
            else
            {
                //재료가 다르면 잘못만든거
                rightIngredientNum = 0;
            }
        }

        //맞은 재료의 개수가 햄버거배열하고 동일하면 완성한거
        if (rightIngredientNum == hamburgerArray.Length)
            return true;
        else
            return false;
    }
}
