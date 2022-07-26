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
    private GameObject go_CookedItem_Prefab; // 익혀진 혹은 탄 고기 아이템 교체
    public Material cookedMaterial;

    public G_Ingredient g_Ingredient;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        g_Ingredient = GetComponentInParent<G_Ingredient>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Touch");
    }
    private void OnCollisionStay(Collision other) {
        if(other.transform.tag == "Fire" && !done)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                done = true;
                //Instantiate(go_CookedItem_Prefab, transform.position, Quaternion.Euler(transform.eulerAngles));
                //Destroy(gameObject); // 날고기인 자기 자신은 파괴

                meshRenderer.material = cookedMaterial;
            }
        }
    }
}
