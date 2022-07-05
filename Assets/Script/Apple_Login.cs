using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppleAuth;
using System.Security.Cryptography;
using System;
using System.Text;
using AppleAuth.Interfaces;
using AppleAuth.Enums;
using AppleAuth.Native;

public class Apple_Login : MonoBehaviour
{
    public static Apple_Login _instance { get; private set; }

    public static string IdToken { get; private set; }
    public static string AuthCode { get; private set; }
    public static string RawNonce { get; private set; }
    public static string Nonce { get; private set; }

    private IAppleAuthManager appleAuthManager;
    public bool IsLoginSuccess = false;
    public bool IsLoginDone = false;

    private void Awake()
    {
        if (_instance == null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        if (AppleAuthManager.IsCurrentPlatformSupported)
        {
            var deserializer = new PayloadDeserializer();
            appleAuthManager = new AppleAuthManager(deserializer);
        }
        else {
            Debug.Log("This Platform is not Supported");
        }
    }
    private void Update()
    {
        appleAuthManager?.Update();
    }

    // Nonce는 SHA256으로 만들어서 전달해야함
    private static string GenerateNonce(string _rawNonce)
    {
        SHA256 sha = new SHA256Managed();
        var sb = new StringBuilder();
        // Encoding은 반드시 ASCII여야 함
        byte[] hash = sha.ComputeHash(Encoding.ASCII.GetBytes(_rawNonce));
        // ToString에서 "x2"로 소문자 변환해야 함. 대문자면 실패함. ㅠㅠ
        foreach (var b in hash) sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
    // 내부적으로 사용하는 인터페이스 통일을 위해 Coroutine으로 구현
    // 기존 인터페이스가 아니었다면 Async를 사용했을 듯
    public IEnumerator LoginProcess()
    {
        IsLoginSuccess = false;
        IsLoginDone = false;

        // Nonce 초기화
        // Nonce는 Apple로그인 시 접속 세션마다 새로 생성
        RawNonce = System.Guid.NewGuid().ToString();
        Nonce = GenerateNonce(RawNonce);

        // QuickLogin을 먼저 수행
        // 이전 로그인 기록이 없다면 실패 처리됨
        var quickLoginArgs = new AppleAuthQuickLoginArgs(Nonce);
        var isQuickLoginDone = false;
        appleAuthManager.QuickLogin(
            quickLoginArgs,
            credential =>
            {
                try
                {
                    var appleIdCredential = credential as IAppleIDCredential;
                    AuthCode = Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode);
                    IdToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken);
                    IsLoginSuccess = true;
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                    IsLoginSuccess = false;
                }
                isQuickLoginDone = true;
            },
            error =>
            {
                IsLoginSuccess = false;
                isQuickLoginDone = true;
            });
        yield return new WaitUntil(() => isQuickLoginDone);
        // QuickLogin이 성공했다는 것은 이전 로그인 정보가 있었다는 의미
        // 일반 Login 과정을 진행할 필요가 없어짐.
        if (IsLoginSuccess)
        {
            IsLoginDone = true;
            yield break;
        }

        var loginArgs = new AppleAuthLoginArgs(LoginOptions.IncludeEmail, Nonce);
        appleAuthManager.LoginWithAppleId(
            loginArgs,
            credential =>
            {
                try
                {
                    var appleIdCredential = credential as IAppleIDCredential;
                    AuthCode = Encoding.UTF8.GetString(appleIdCredential.AuthorizationCode);
                    IdToken = Encoding.UTF8.GetString(appleIdCredential.IdentityToken);
                    IsLoginSuccess = true;
                }
                catch (System.Exception e)
                {
                    Debug.LogException(e);
                    IsLoginSuccess = false;
                }
                IsLoginDone = true;
            },
            error =>
            {
                IsLoginSuccess = false;
                IsLoginDone = true;
            });
        yield return new WaitUntil(() => IsLoginDone);
    }
}
