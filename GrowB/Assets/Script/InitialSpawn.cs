using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpawn : MonoBehaviour
{
    public GameObject[] initialDonglePrefabs;
    private GameObject _dongle;
    public float[] initialPossibility = new float[4] {25, 25, 25, 25};

    // Start is called before the first frame update
    void Start()
    {
        if (initialDonglePrefabs == null)
        {
            Debug.LogError("There is no initialDonglePrefabs");
            return;
        }
        
        for (int i = 0; i < 10; i++)
        {
            RandomSpawn(initialPossibility);
        }
    }

    void RandomSpawn(float[] possibility)
    {
        int rand = Random.Range(0, 1000);
        float temp = 0;

        for (int i = 0; i < possibility.Length; i++)
        {
            temp += possibility[i] * 10;
            
            if (rand < temp)
            {
                float initialPositionBound = Random.Range(-1.5f, 1.5f);
                
                GameObject myDongle = Instantiate(initialDonglePrefabs[i], transform.position + Vector3.right * initialPositionBound, Quaternion.identity);
                myDongle.transform.parent = GameObject.Find("Dongles").transform;
                StartCoroutine(MergingDelay(myDongle));
                
                break;
            }
        }
    }

    IEnumerator MergingDelay(GameObject delayedDongle)
    {
        yield return new WaitForSeconds(1f);
        delayedDongle.GetComponent<DongleFeature>().isMerging = false;
    }
}
