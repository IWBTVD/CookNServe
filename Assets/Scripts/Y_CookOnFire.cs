using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Y_CookOnFire : MonoBehaviourPun
{
    // Start is called before the first frame update
    [SerializeField]
    private float time; // 익히거나 타는데 걸리는 시간
    private float currentTime; // 업데이트해 나갈 시간. time 에 도달시킬 것.

    private bool isDone; // 끝났으면 더 이상 불에 있어도 계산 무시할 수 있게끔

    public bool isSoundPlayed = false;

    [SerializeField]
    public Material cookedMaterial;
    public AudioClip cookingClip;

    public G_Ingredient g_Ingredient;

    private MeshRenderer meshRenderer;
    private AudioSource audioSource;

    public bool isCooking = false;

    private void Start()
    {
        g_Ingredient = GetComponent<G_Ingredient>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isCooking)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= time)
            {
                photonView.RPC("CompleteCooking", RpcTarget.AllBuffered);
            }
        }
        else
            currentTime = 0f;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Fire" && !isDone)
        {
            if(!isSoundPlayed)
            {
                audioSource.PlayOneShot(cookingClip);
                isSoundPlayed = true;
            }
            
            isCooking = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.transform.tag == "Fire" && !isDone)
        {
            isCooking = false;
        }
    }

    [PunRPC]
    void CompleteCooking() {
        isDone = true;
        meshRenderer.material = cookedMaterial; //메테리얼 교체
        g_Ingredient.isCooked = true; //구워짐
        g_Ingredient.ingredientType = IngredientType.CookedCutletB;
    }
}
