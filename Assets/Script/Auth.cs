using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField passwordField;
    
    [SerializeField] string UserID; // key

    [SerializeField] string email;
    [SerializeField] string password;
    [SerializeField] string nickname = null;
    [SerializeField] string ratingscore = null;

    public Button login_button;
    public Button register_button;

    FirebaseAuth auth; // firebase auth
    DatabaseReference reference; // firebase database
	// Start is called before the first frame update
	private void Awake()
	{
        auth = FirebaseAuth.DefaultInstance;

    }

    public void login()
	{
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task =>
            {
                if(task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
				{
                    Firebase.Auth.FirebaseUser user = task.Result;
                    Debug.Log(user.Email+" login complete");
                }
				else
				{
                    Debug.Log("login fail");
				}
            });
	}
    
    public void register()
	{
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task =>
            {
                if(!task.IsFaulted && !task.IsCanceled)
				{
                    email = emailField.text;
                    password = passwordField.text;

                    FirebaseUser newUser = task.Result;
                    Debug.Log("register complete");
                    CreateUserWithJson(new JoinDB(email, password, nickname, ratingscore), newUser.UserId);
				}                    
                else
				{
                    Debug.Log("register fail");
				}
            });
    }
    
    void CreateUserWithJson(JoinDB userInfo, string uid)
    {
        string data = JsonUtility.ToJson(userInfo);
        reference.Child("users").Child(uid).SetRawJsonValueAsync(data).ContinueWith(
            task =>
            {
                if(task.IsFaulted)
                {
                    Debug.Log("database setting isfaulted");
                }
                if(task.IsCanceled)
                {
                    Debug.Log("database setting iscanceled");
                }
                if (task.IsCompleted)
                {
                    Debug.Log("database setting iscompleted");
                }
            }); //database에 쓰기
       
    }

	void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://presidentcq-4854b-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        login_button.onClick.AddListener(() =>
        {
            login();
        });
        register_button.onClick.AddListener(() =>
        {
            register();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class JoinDB
    {//이메일, 비번, rating score, 이름
        public string email;
        public string password;
        public string nickname;
        public string ratingscore;

        public JoinDB(string email, string password,  string nickname, string ratingscore)
        {
            this.email = email;
            this.password = password;
            this.nickname = nickname;
            this.ratingscore = ratingscore;
        }
        
    }
}
