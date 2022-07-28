using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Y_CookOnFire : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float time; // 익히거나 타는데 걸리는 시간
    private float currentTime; // 업데이트해 나갈 시간. time 에 도달시킬 것.

    private bool done; // 끝났으면 더 이상 불에 있어도 계산 무시할 수 있게끔

    [SerializeField]
    public Material cookedMaterial;

    public G_Ingredient g_Ingredient;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        g_Ingredient = GetComponent<G_Ingredient>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Fire Touch");
    }
    private void OnCollisionStay(Collision other) {
        if(other.transform.tag == "Fire" && !done)
        {
            Debug.Log("Cooking");
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                done = true;
                meshRenderer.material = cookedMaterial; //메테리얼 교체
                g_Ingredient.isCooked = true; //구워짐
                g_Ingredient.ingredientType = IngredientType.CookedCutletB;

            }
        }
    }
}
