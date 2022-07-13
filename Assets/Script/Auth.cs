using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Auth : MonoBehaviour
{
    [SerializeField] InputField emailField;
    [SerializeField] InputField pwdField;
    [SerializeField] InputField join_emailField;
    [SerializeField] InputField join_pwdField;
    [SerializeField] InputField join_nickNameField;


    [SerializeField] string userId; // key

    public Button loginBtn;
    public Button joinBtn;
    public Button join_joinBtn;
    public Button join_XBtn;
    public DatabaseManager dbm;
    public Text monitoringText;
    public Text join_monitoringText;
    public GameObject joinPanel;

    FirebaseAuth auth; // firebase auth
    DatabaseReference reference; // firebase database



    bool loginFlag = false;
    bool joinFlag = false;

    Queue<string> queue = new Queue<string>();

    private void Awake()
	{
        auth = FirebaseAuth.DefaultInstance;

    }

    public void Login()
	{
        auth.SignInWithEmailAndPasswordAsync(emailField.text, pwdField.text).ContinueWith(
            task =>
            {
                if(task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
				{
                    FirebaseUser user = task.Result;
                    dbm.SetFirebaseReference(user.UserId);
                    Debug.Log(user.Email + " login complete");
                    loginFlag = true;
                    queue.Enqueue("LoginNext");
                }
				else
				{
                    Debug.Log("login fail");
                    queue.Enqueue("LoginNext");
                }
            });
	}
    
    public void Join()
	{
        auth.CreateUserWithEmailAndPasswordAsync(join_emailField.text, join_pwdField.text).ContinueWith(
            task =>
            {
                if(!task.IsFaulted && !task.IsCanceled)
				{

                    FirebaseUser newUser = task.Result;
                    Debug.Log("join complete");
                    CreateUserWithJson(new JoinDB(join_emailField.text, join_pwdField.text, join_nickNameField.text, "1000"), newUser.UserId);
                    joinFlag = true;
                    queue.Enqueue("JoinNext");

                }
                else
				{
                    Debug.Log("join fail");
                    queue.Enqueue("JoinNext");

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
                    Debug.Log("database setting is faulted");
                }
                if(task.IsCanceled)
                {
                    Debug.Log("database setting is canceled");
                }
                if (task.IsCompleted)
                {
                    Debug.Log("database setting is completed");
                }
            }); 
       
    }
    public void LoginNext()
    {
        if (loginFlag)
        {
            monitoringText.text = "로그인 성공 : 환영합니다!";
            SceneManager.LoadScene("Lobby_Scene");
        }
        else
        {
            monitoringText.text = " 로그인 실패 : 이메일과 비밀번호를 다시 확인해주세요.";
        }
    }
    public void JoinNext()
    {
        if (joinFlag)
        {
            join_monitoringText.text = "회원가입 성공 : 창을 닫고 로그인을 시도해 주세요.";
        }
        else
        {
            join_monitoringText.text = "회원가입 실패 : 이메일과 비밀번호를 정확히 입력해 주세요.";
        }
    }

    void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://presidentcq-4854b-default-rtdb.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;

        loginBtn.onClick.AddListener(() =>
        {
            Login();
        });
        joinBtn.onClick.AddListener(() => {
            StartCoroutine(Bigger());
        });
        join_joinBtn.onClick.AddListener(() =>
        {
            Join();
        });
        join_XBtn.onClick.AddListener(() =>
        {
            StartCoroutine(Smaller());
        });
    }
    private void FixedUpdate()
    {
        if (queue.Count > 0)
        {
            Invoke(queue.Dequeue(), 0.1f);
        }
    }

    public class JoinDB
    {
        public string email;
        public string password;
        public string nickName;
        public string rating;

        public JoinDB(string email, string password, string nickName, string rating)
        {
            this.email = email;
            this.password = password;
            this.nickName = nickName;
            this.rating = rating;
        }

    }
    IEnumerator Bigger()
    {
        joinPanel.SetActive(true);
        RectTransform rect = joinPanel.GetComponent<RectTransform>();
        float t = 0.0f;

        while (t<=0.2f)
        {
            rect.localScale = new Vector3(5*t,5*t,5*t);
            t += Time.deltaTime;
            yield return null;
        }

    }
    IEnumerator Smaller()
    {
        joinPanel.SetActive(true);
        RectTransform rect = joinPanel.GetComponent<RectTransform>();
        float t = 0.2f;

        while (t >= 0.0f)
        {
            rect.localScale = new Vector3(5 * t, 5 * t, 5 * t);
            t -= Time.deltaTime;
            yield return null;
        }

    }

}
