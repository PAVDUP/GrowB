using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Dongle"))
        {
            DongleFeature otherDongle = other.GetComponent<DongleFeature>();

            if (otherDongle.overDongleCheck)
            {
                StartCoroutine(GameOver());
                FindObjectOfType<TouchLaunch>().CanLaunch = false;
            }
        }
    }

    IEnumerator GameOver()
    {
        Image blackImage = GameObject.Find("GameOverUI").transform.GetChild(0).GetComponent<Image>();
        Color imageColor = blackImage.color;
        blackImage.gameObject.SetActive(true);

        for (float i = 0; i < 1; i += 0.01f)
        {
            imageColor.a += 0.01f;
            blackImage.color = imageColor;
            yield return new WaitForSeconds(0.01f);
        }
        
        SceneManager.LoadScene("GameOver");
    }
}
