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

    public Button RandomBtn;

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

        RandomBtn.onClick.AddListener(Connect);

        onlineMonitoringText.text = "서버에 접속중..";
    }
    private void Update()
    {
        if (PhotonNetwork.IsConnected)
        {
            //������ �ҷ�����
        }
        else
        {
            //db.GetUserInformationFromFireBase();
            onlineMonitoringText.text = "서버 접속 실패 : 마스터서버에 재접속중...";

        }
    }

    public override void OnConnectedToMaster()
    {
        onlineMonitoringText.text = onlineMonitoringText.text = "연결됨 : 환영합니다! " + db.getNickName() + "님!";

        PhotonNetwork.JoinLobby();//������ ���� ����� �κ�� ����
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        // �� ���� ��ư�� ��Ȱ��ȭ


        onlineMonitoringText.text = "연결 유실 : 연결정보를 잃었습니다.\n 재접속중...";

        //������ �������� ������ �õ�
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnJoinedLobby()//�κ� ����� �۵�
    {
        Debug.Log("Joined Lobby");
        //PhotonNetwork.NickName = "Player " + UnityEngine.Random.Range(0, 1000).ToString("0000");
        PhotonNetwork.NickName = db.getNickName();
    }

    public void Connect()
    {

        if (PhotonNetwork.IsConnected)
        {
            //�� ���� ����
            onlineMonitoringText.text = "연결됨 : 랜덤 룸에 접속중...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {

            //������ �������� ������ �õ�
            onlineMonitoringText.text = "연결 유실 : 연결정보를 잃었습니다.\n 재접속중...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        onlineMonitoringText.text = "연결됨 : 생성된 룸이 없음. 룸을 생성 중...";
        Debug.Log("Creating Room");
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 0 });

    }

    public override void OnJoinedRoom()
    {
        //���� ���� ǥ��
        onlineMonitoringText.text = "연결됨 : 룸에 접속 중...";
        Debug.Log("Join Room");
        PhotonNetwork.LoadLevel("Game_Scene");
        //LoadingSceneController.Instance.LoadScene("Scene_Field");

    }
}
