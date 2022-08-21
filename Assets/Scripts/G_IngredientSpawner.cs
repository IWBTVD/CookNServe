using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_IngredientSpawner : MonoBehaviourPun
{
    private static G_IngredientSpawner spawnerInstance;

    public Transform[] spawnTransforms;
    public GameObject[] IngredientPacks = new GameObject[10];

    public GameObject[] prefabs;

    public float timer = 10f;
    public float Timer { get => timer; set => ActionRPC(nameof(SetTimerRPC), value);}
    [PunRPC] void SetTimerRPC(float value) => timer = value;

    public bool isSpawn = false;

    private void Awake()
    {
        if (spawnerInstance == null)
            spawnerInstance = this;
        else
            this.enabled = false;
    }

    void Start()
    {
        Timer = Timer;
    }

    void Update()
    {
        //if (timer >= 0)
        //{
        //    timer -= Time.deltaTime;
        //}
        //if (timer <= 0f && !isSpawn)
        //    CreateIngredient();
    }

    public void CreateIngredient()
    {
        int i = 0;
            foreach (GameObject obj in IngredientPacks)
            {
                if (!obj)
                {
                    string packName;
                    switch (i)
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

                    //IngredientPacks[i] = Instantiate(prefabs[i/2], spawnTransforms[i].position, Quaternion.identity);
                    IngredientPacks[i].transform.parent = transform;
                    IngredientPacks[i].GetComponent<G_IngredientPack>().myNumber = i;
                    IngredientPacks[i].GetComponent<G_IngredientPack>().isCreated = true;
                }
                i++;
                isSpawn = true;
            }
    }

    public void ActivateSpawner(int i)
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

    void ActionRPC(string functionName, object value)
    {
        photonView.RPC(functionName, RpcTarget.All, value);
    }

    public void InvokeProperties()
    {
        Timer = Timer;
    }
}