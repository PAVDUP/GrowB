using System.Collections;
using Unity.Advertisement.IosSupport.Components;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.iOS;
#endif
using UnityEngine.SceneManagement;

namespace Unity.Advertisement.IosSupport.Samples
{
    public class ContextScreenManager : MonoBehaviour
    {
        public ContextScreenView contextScreenPrefab;

        void Start()
        {
            int setChecker = PlayerPrefs.GetInt("setChecker", 0);
            
#if UNITY_IOS
            // 현재 실행 환경이 실제 iOS 기기인지 확인
            if (!Device.iosAppOnMac && setChecker == 0)
            {
                var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

                if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                {
                    var contextScreen = Instantiate(contextScreenPrefab).GetComponent<ContextScreenView>();
                    contextScreen.sentTrackingAuthorizationRequest += () => Destroy(contextScreen.gameObject);
                }
            }
            else
            {
                Debug.Log("ATT status not checked, running on macOS.");
            }
#else
            Debug.Log("Unity iOS Support: App Tracking Transparency status not checked, because the platform is not iOS.");
#endif
            StartCoroutine(LoadNextScene());
        }

        private IEnumerator LoadNextScene()
        {
            int setChecker = PlayerPrefs.GetInt("setChecker", 0);
            
#if UNITY_IOS
            if (!Device.iosAppOnMac && setChecker == 0)
            {
                var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                while (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
                {
                    status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
                    yield return null;
                }
            }
#endif
            PlayerPrefs.SetInt("setChecker", 1);
            SceneManager.LoadScene(1);
            yield return null;
        }
    }   
}