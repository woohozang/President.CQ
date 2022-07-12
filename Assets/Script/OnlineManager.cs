using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnlineManager : MonoBehaviourPunCallbacks
{
    public Text onlineMonitoringText;
    private string gameVersion = "1";
    public DatabaseManager db;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        db = GameObject.Find("UserInfo").GetComponent<DatabaseManager>();

        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("First Login");

        }
        else if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
            Debug.Log("Already Login");
        }


        onlineMonitoringText.text = "서버에 접속중..";
    }
    private void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            //룸정보 불러오기
        }
        else
        {
            //db.GetUserInformationFromFireBase();
            onlineMonitoringText.text = "온라인 : 데이터베이스와 통신 중...";

        }
    }

    public override void OnConnectedToMaster()
    {
        onlineMonitoringText.text = onlineMonitoringText.text = "온라인 : 환영합니다! " + db.getNickName() + "님!";

        PhotonNetwork.JoinLobby();//마스터 서버 연결시 로비로 연결
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // 룸 접속 버튼을 비활성화


        onlineMonitoringText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도 중...";

        //마스터 서버로의 재접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnJoinedLobby()//로비에 연결시 작동
    {
        Debug.Log("Joined Lobby");
        //PhotonNetwork.NickName = "Player " + UnityEngine.Random.Range(0, 1000).ToString("0000");
        PhotonNetwork.NickName = db.getNickName();
    }

    public void Connect()
    {

        if (PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            onlineMonitoringText.text = "온라인 : 채널에 접속 중..";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            //마스터 서버로의 재접속 시도
            onlineMonitoringText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        onlineMonitoringText.text = "온라인 : 활성화된 채널 없음. 새로운 채널 생성";
        Debug.Log("Creating Room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 0 });

    }

    public override void OnJoinedRoom()
    {
        //접속 상태 표시
        onlineMonitoringText.text = "온라인 : 채널 생성 성공";
        Debug.Log("Join Room");
        PhotonNetwork.LoadLevel("Scene_Field");
        //LoadingSceneController.Instance.LoadScene("Scene_Field");

    }
}
