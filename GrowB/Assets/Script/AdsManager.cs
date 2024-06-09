using Gley.MobileAds;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    private bool _adAvailable = true;
    private bool _interstitialAdAvailable = true;

    private int _adCount;
    
    public void InitializeAdsManagerWithoutIAP()
    {
        _adAvailable = true;
        
        if (!_adAvailable)
        {
            Debug.Log("Ads are disabled");
            return;
        }
        
        Debug.Log("Ads are enabled");
        API.Initialize();
    }
    
    public void ShowInterstitialAd()
    {
        if (!_adAvailable)
        {
            Debug.Log("Interstitial disabled");
            return;
        }
        
        if (_interstitialAdAvailable)
        {
            API.ShowInterstitial();
            _interstitialAdAvailable = false;
        }
        else
        {
            Debug.Log("Interstitial is not available");
        }
    }
}
