using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_IngredientSpawner : MonoBehaviourPun
{
    public Transform[] spawnTransforms;
    public GameObject[] IngredientPacks = new GameObject[10];

    public float timer = 5f;
    public bool isSpawn = false;

    void Start()
    {
        photonView.RPC(nameof(SetTimer), RpcTarget.All, 5f);
    }

    void Update()
    {
        if (timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0f && !isSpawn)
            CreateIngredient();
    }

    public void CreateIngredient()
    {
        int i = 0;
        foreach (GameObject obj in IngredientPacks)
        {
            if (!obj)
            {
                string packName;
                switch(i)
                {
                    case 0: packName = "IngredientTray Cabbage"; break;
                    case 1: packName = "IngredientTray Cabbage"; break;
                    case 2: packName = "IngredientTray Cheese"; break;
                    case 3: packName = "IngredientTray Cheese"; break;
                    case 4: packName = "IngredientTray Cutlet"; break;
                    case 5: packName = "IngredientTray Cutlet"; break;
                    case 6: packName = "IngredientTray Mushroom"; break;
                    case 7: packName = "IngredientTray Mushroom"; break;
                    case 8: packName = "IngredientTray Tomato"; break;
                    case 9: packName = "IngredientTray Tomato"; break;
                    default: packName = "IngredientTray Cabbage"; break;
                }

                IngredientPacks[i] = PhotonNetwork.Instantiate(packName, spawnTransforms[i].position, Quaternion.identity);
                IngredientPacks[i].transform.parent = transform;
                IngredientPacks[i].GetComponent<G_IngredientPack>().myNumber = i;
                IngredientPacks[i].GetComponent<G_IngredientPack>().isCreated = true;
            }
            i++;
            isSpawn = true;
        }
    }

    public void CreateIngredient(int i)
    {
        isSpawn = false;
        IngredientPacks[i] = null;
        if (timer <= 0f)
            photonView.RPC(nameof(SetTimer), RpcTarget.All, 5f);
    }

    [PunRPC]
    public void SetTimer(float t)
    {
        timer = t;
    }
}