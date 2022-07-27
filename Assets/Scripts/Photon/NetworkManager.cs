using Photon.Pun;   // Photon 의 여러 기능을 포함 라이브러리를 Unity에서 컴포넌트로 사용 가능
using Photon.Realtime;  // Realtime Network 게임 개발 c# 라이브러리
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : MonoBehaviourPunCallbacks // PUN 구현할때 override 사용해 코드 작성해야됨
{
    private string gameVersion = "1"; // 같은 버전끼리 매칭하기 위해 string 사용 숫자뿐만 아닌 다른 것도 사용 가능

    public PlayerSpawner playerSpawner;

    private void Start()
    {
        // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.GameVersion = gameVersion;
        // 설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinOrCreateRoom("Room", new RoomOptions { MaxPlayers = 2 }, null);
    }

    public override void OnJoinedRoom()
    {
        playerSpawner.SpawnPlayer();
    }

}
