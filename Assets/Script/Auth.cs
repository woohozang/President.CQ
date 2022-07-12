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
    
    [SerializeField] string userId; // key

    [SerializeField] string email;
    [SerializeField] string password;
    [SerializeField] string nickname = null;
    [SerializeField] string ratingscore = null;

    public Button login_button;
    public Button register_button;

    public Text monitoringText;

    FirebaseAuth auth; // firebase auth
    DatabaseReference reference; // firebase database
	// Start is called before the first frame update
	private void Awake()
	{
        auth = FirebaseAuth.DefaultInstance;

    }

    public void login()
	{
        monitoringText.text = "로그인 중 : 잠시만 기다려 주세요.. ";
        auth.SignInWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task =>
            {
                if(task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
				{
                    FirebaseUser user = task.Result;
                    monitoringText.text = "로그인 성공 : 환영합니다! "+user.UserId;
                    Debug.Log(user.Email+" login complete");
                }
				else
				{
                    monitoringText.text = "로그인 실패 : 아이디와 비밀번호를 확인해 주세요!";
                    Debug.Log("login fail");
				}
            });
	}
    
    public void register()
	{
        monitoringText.text = "회원가입 중 : 잠시만 기다려 주세요.. ";
        auth.CreateUserWithEmailAndPasswordAsync(emailField.text, passwordField.text).ContinueWith(
            task =>
            {
                if(!task.IsFaulted && !task.IsCanceled)
				{
                    email = emailField.text;
                    password = passwordField.text;

                    FirebaseUser newUser = task.Result;
                    monitoringText.text = "회원가입 완료 : 로그인 해주세요!";
                    Debug.Log("register complete");
                    CreateUserWithJson(new JoinDB(email, password, nickname, ratingscore), newUser.UserId);
				}                    
                else
				{
                    monitoringText.text = "회원가입 실패 : 아이디와 비밀번호를 정확하게 입력해 주세요!";
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
            }); //database�� ����
       
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

    public class JoinDB
    {//�̸���, ���, rating score, �̸�
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
