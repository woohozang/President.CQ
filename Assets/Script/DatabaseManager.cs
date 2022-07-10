using Firebase;
using Firebase.Auth;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    DatabaseReference reference;
    FirebaseAuth auth;

    [SerializeField]
    private string uid;
    [SerializeField]
    private string nickName;
    [SerializeField]
    private string rating;

    public string getUid() {
        return uid;
    }
    public string getNickName()
    {
        return nickName;
    }
    public string getRating()
    {
        return rating;
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        DontDestroyOnLoad(this);
        FirebaseApp.DefaultInstance.Options.DatabaseUrl =
                   new System.Uri("https://presidentcq-4854b-default-rtdb.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetValueFireBase(string key, string value)
    {

        reference.Child(key).SetValueAsync(value);

    }
}
