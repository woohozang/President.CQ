using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;

public class Auth : MonoBehaviour
{
    [SerializeField] string email;
    [SerializeField] string password;
    [SerializeField] InputField emailField;
    [SerializeField] InputField passwordField;

    public Button login_button;
    public Button register_button;

    FirebaseAuth auth;
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
                    Debug.Log("register complete");
				}                    
                else
				{
                    Debug.Log("register fail");
				}
            });
    }
    

	void Start()
    {
        FirebaseApp.DefaultInstance.Options.DatabaseUrl = new System.Uri("https://presidentcq-4854b-default-rtdb.firebaseio.com/");

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
}
