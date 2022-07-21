using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class UserManager : MonoBehaviour
{
    [SerializeField] List<Item> cards;
    public PhotonView PV;
    public DatabaseManager db;
    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("UserInfo").GetComponent<DatabaseManager>();
        
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetCards(List<Item> _cards)
    {
        cards = _cards;
    }
}
