using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Admob : MonoBehaviour
{
    public static Admob instance;

    BannerView bannerView; // ±¤°í¹è³Ê

    public static Admob Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);

        RequestBanner(); // ¹è³Ê ±¤°í ¼³Á¤
    }

    private void RequestBanner()
    {
#if UNITY_EDITOR
        string adUnitId = "unused";
#elif UNITY_ANDROID
    string adUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
    string adUnitId = "INSERT_IOS_BANNER_AD_UNIT_ID_HERE";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopRight);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the banner with the request.

        bannerView.LoadAd(request);
    }

    public void Load_ad()
    {
        Debug.Log("load_ad");
        bannerView.Show();
    }

    public void Exit_ad()
    {
        Debug.Log("exit_ad");
        bannerView.Hide();
    }
}
