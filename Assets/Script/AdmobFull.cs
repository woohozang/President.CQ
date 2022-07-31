using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdmobFull : MonoBehaviour
{
    private InterstitialAd interstitial;
    public void Start()
    {
        //광고 초기화
        MobileAds.Initialize(initStatus =>
        {
            RequestInterstitial();
        });
    }
    private void RequestInterstitial()
    {
        //여러 OS에서 공통된 코드를 사용할 경우 이렇게 하면 편리
            //여기 들어가는 ID는 /가 들어간 쪽의 광고 단위 ID
            //이 ID들은 Google이 지원하는 테스트 ID이므로 제한 없이 사용 가능
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
            string adUnitId = "unexpected_platform";
        #endif
 
        //단일 OS일 경우 여기서 바로 스트링으로 꽂아줘도 가능
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
 
    //광고를 시작해야 할 때에 외부에서 이 함수를 호출
    public void AdStart()
    {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        }
    }
}