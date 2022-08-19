using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class G_Fryer : MonoBehaviourPun
{
    private static G_Fryer fryerInstance;

    public Transform[] spawnPoint;

    public GameObject[] fries = new GameObject[3];

    public float timer = 10f;
    public float Timer { get => timer; set => ActionRPC(nameof(SetTimerRPC), value); }
    [PunRPC] void SetTimerRPC(float value) => timer = value;

    public bool isSpawn = false;

    private void Awake()
    {
        if (fryerInstance == null)
            fryerInstance = this;
        else
            this.enabled = false;
    }

    void Start()
    {
        Timer = Timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 0)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0f && !isSpawn)
            CreateFry();
    }

    public void CreateFry()
    {
        if(photonView.IsMine)
        {
            int i = 0;
            foreach (GameObject f in fries)
            {
                if (!f)
                {
                    fries[i] = PhotonNetwork.Instantiate("FrenchFries", spawnPoint[i].position, spawnPoint[i].rotation);
                    fries[i].transform.parent = transform;
                    fries[i].GetComponent<G_FrenchFry>().myNumber = i;
                }
                i++;
                isSpawn = true;
            }
        } 
    }

    public void GrabFrenchFry(int i)
    {
        isSpawn = false;
        fries[i] = null;
        if(timer <= 0f)
            timer = 5f;
    }

    void ActionRPC(string functionName, object value)
    {
        photonView.RPC(functionName, RpcTarget.All, value);
    }

}
