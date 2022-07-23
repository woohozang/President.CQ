using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public List<User> userList;
    List<string> attenderList;


    private void init()
    {
        int i = 1;
        foreach(string s in attenderList) {
            if ("È²À±°Ö¶ó"== s)
            {
                userList[0].Name = s;
                userList[0].SetName();
            }
            else {
                userList[i].Name = s;
                userList[i].SetName();
                i++;
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        attenderList = GameObject.Find("RoomManager").GetComponent<RoomManager>().attenderList;
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
