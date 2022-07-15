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
    public Button RefreshBtn;

    public GameObject roomPrefab;
    public GameObject Content;
    
    private List<GameObject> roomPrefabs = new List<GameObject>();
    private List<RoomInfo> roomList = new List<RoomInfo>();

    public override void OnRoomListUpdate(List<RoomInfo> updatedRoomList)
    {
        for (int i= 0; i < updatedRoomList.Count; i++)
        {
            if (roomList.Contains(updatedRoomList[i])) //변동사항이 변경일때
            {
                if (updatedRoomList[i].RemovedFromList) //변경내용이 삭제일 때
                {
                    Debug.Log(updatedRoomList[i].Name + " 삭제");

                    //int index = roomList.FindIndex(item => item.Name == updatedRoomList[i].Name);
                    roomList.Remove(updatedRoomList[i]);

                }
                else //변경내용이 삭제가 아닐때(룸 접속인원 수 변동)
                {
                    Debug.Log(updatedRoomList[i].Name + " 추가");

                    int index = roomList.FindIndex(item => item.Name == updatedRoomList[i].Name);
                    roomList[index] = updatedRoomList[i];
                }
                
            }
            else //변동사항이 추가일때
            {
                Debug.Log(updatedRoomList[i].Name+" 추가");
                roomList.Add(updatedRoomList[i]);
            }
        }
        DisplayRooms();
    }
    public void DisplayRooms() {
        for (int i = 0; i < roomPrefabs.Count; i++)
        {
            Destroy(roomPrefabs[i]);
        }
        roomPrefabs.Clear();

        for (int i = 0; i < roomList.Count; i++)
        {
            GameObject groom = Instantiate(roomPrefab);
            groom.transform.parent = Content.transform;
            groom.GetComponent<RectTransform>().localScale = roomPrefab.GetComponent<RectTransform>().localScale;
            groom.GetComponent<RectTransform>().localPosition = new Vector3(roomPrefab.GetComponent<RectTransform>().localPosition.x, roomPrefab.GetComponent<RectTransform>().localPosition.y - (i * 100f), roomPrefab.GetComponent<RectTransform>().localPosition.z);
            string roomName = roomList[i].Name;
            groom.GetComponentInChildren<Text>().text = roomList[i].Name + "    " + roomList[i].PlayerCount + "/" + roomList[i].MaxPlayers;
            groom.GetComponent<Button>().onClick.AddListener(() =>
            {
                //Debug.Log(" joinRoom to" + roomList[i].Name);
                PhotonNetwork.JoinRoom(roomName);
            });
        }

    }

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;
        db = GameObject.Find("UserInfo").GetComponent<DatabaseManager>();

        if (!PhotonNetwork.IsConnected)
        {
            onlineMonitoringText.text = "서버에 접속중..";
            PhotonNetwork.ConnectUsingSettings();

        }
        else {
            onlineMonitoringText.text = "연결됨 : 환영합니다! " + db.getNickName() + "님!";

        }

        RandomBtn.onClick.AddListener(()=> {
            Connect();
        });
        RefreshBtn.onClick.AddListener(() => {
            //RefreshRoomList();
        });
    }



    public override void OnConnectedToMaster()
    {
        onlineMonitoringText.text = "연결됨 : 환영합니다! " + db.getNickName() + "님!";
        RandomBtn.interactable = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {


        onlineMonitoringText.text = "연결 유실 : 연결정보를 잃었습니다.\n 재접속중...";
        RandomBtn.interactable = false;
        PhotonNetwork.ConnectUsingSettings();
    }


    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
        //PhotonNetwork.NickName = "Player " + UnityEngine.Random.Range(0, 1000).ToString("0000");
        PhotonNetwork.NickName = db.getNickName();
    }

    public void Connect()
    {
        Debug.Log("Try Connect Room");
        if (PhotonNetwork.IsConnected)
        {
            onlineMonitoringText.text = "연결됨 : 랜덤 룸에 접속중...";
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            onlineMonitoringText.text = "연결 유실 : 연결정보를 잃었습니다.\n 재접속중...";
            RandomBtn.interactable = false;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        onlineMonitoringText.text = "연결됨 : 생성된 룸이 없음. 룸을 생성 중...";
        Debug.Log("Creating Room");
        PhotonNetwork.CreateRoom(PhotonNetwork.NickName+"의 룸", new RoomOptions { MaxPlayers = 0 });

    }

    public override void OnJoinedRoom()
    {
        onlineMonitoringText.text = "연결됨 : 룸에 접속 중...";
        Debug.Log("Join Room");
        PhotonNetwork.LoadLevel("Room_Scene");

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("JoinRoom Error : "+returnCode+", "+message);
    }
}
