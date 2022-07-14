using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    List<string> attenderList = new List<string>();


    public Text attenderListText;
    public Text RoomNameText;

    public Button startBtn;
    public Button exitBtn;

    public PhotonView pv;

    void Start()
    {
        RoomNameText.text = PhotonNetwork.CurrentRoom.Name;
        pv.RPC("AddAttender", RpcTarget.AllBuffered, PhotonNetwork.NickName);


        startBtn.onClick.AddListener(()=> {
            pv.RPC("GameStart", RpcTarget.All);
        });
        exitBtn.onClick.AddListener(()=> {
            PhotonNetwork.LeaveRoom();
        });

    }
    public override void OnLeftRoom()
    {
        if (SceneManager.GetActiveScene().name == "Room_Scene") {
            SceneManager.LoadScene("Lobby_Scene");
            return;
        }
    }
    void FixedUpdate()
    {
        string temp="";
        foreach (string s in attenderList) {
            temp += s + "\n";
        }
        attenderListText.text = temp;

        if (PhotonNetwork.IsMasterClient)
        {
            startBtn.interactable = true;
        }
    }

    [PunRPC]
    void AddAttender(string nickName) {
        attenderList.Add(nickName);
    }

    [PunRPC]
    void GameStart() {
        PhotonNetwork.LoadLevel("Game_Scene");
    }


}
