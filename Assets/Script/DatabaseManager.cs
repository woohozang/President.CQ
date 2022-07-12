using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    DatabaseReference reference;

    [SerializeField]
    private string uid;
    [SerializeField]
    private string nickName;
    [SerializeField]
    private int rating;

    public string getUid() {
        return uid;
    }
    public string getNickName()
    {
        return nickName;
    }
    public int getRating()
    {
        return rating;
    }

    void Start()
    {
        DontDestroyOnLoad(this);
        FirebaseApp.DefaultInstance.Options.DatabaseUrl =
                   new System.Uri("https://presidentcq-4854b-default-rtdb.firebaseio.com/");

        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetFirebaseReference(string uid) {
        this.uid = uid;
        reference = reference.Child("users").Child(uid);
        GetUserInformationFromFirebase();
    }
    public void GetUserInformationFromFirebase() {
        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot dataSnapshot = task.Result;
                nickName = dataSnapshot.Child("nickName").GetValue(true).ToString();
                rating = int.Parse(dataSnapshot.Child("rating").GetValue(true).ToString());
            }
        }
        );
    }
    
    public void SetValueFireBase(string key, string value)
    {

        reference.Child(key).SetValueAsync(value);

    }
}
