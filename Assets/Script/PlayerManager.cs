using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PlayerManager : MonoBehaviour
{
    [SerializeField] List<string> cards;
    public PhotonView PV;
    public DatabaseManager db;
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("UserInfo").GetComponent<DatabaseManager>();
        name = db.name;
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
