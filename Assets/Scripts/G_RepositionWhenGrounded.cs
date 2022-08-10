using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_RepositionWhenGrounded : MonoBehaviour
{
    //초기 위치값을 리스폰 값으로 할것인지
    public bool setStartPositionIntoRespawnPosition;

    public Transform respawnPosition;

    private void Start()
    {
        if (setStartPositionIntoRespawnPosition)
            respawnPosition = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= 0.2f)
        {
            transform.position = respawnPosition.position;
        }
    }
}
