using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_StartBtn : MonoBehaviour
{
    G_PhotonGrabbable grabbable;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        grabbable = GetComponent<G_PhotonGrabbable>();
        gameManager = GetComponent<GameManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(grabbable.isGrabbed)
        {
            gameManager.StartGame();
        }
    }
}
