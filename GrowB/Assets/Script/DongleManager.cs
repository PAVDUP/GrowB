using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DongleManager : MonoBehaviour
{
    #region Singleton Pattern

    private static DongleManager _instance = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; // if _instance is null, assign this gameObject's component's GameFlowManager
        }
        else
        {
            // If Scene Change, there are maybe have that scene GameManager Object.
            // In that case, _instance isn't null (because already assign and _instance is static - share all GameFlowManager Instance)
            // In that case, destroy Awake gameObject (scene changed gameObject is not "Awake", so don't destroy) 
            Destroy(this.gameObject);
        }
    }
    
    public static DongleManager Instance
    {
        get
        {
            if (null == _instance)
            {
                return null;
            }
            return _instance;
        }
    }
    
    #endregion

    [FormerlySerializedAs("DonglePrefabs")] public GameObject[] donglePrefabs;
    [Range(0, 2)] public float mergeDelayTime = 0.5f;
    [Range(0.5f, 3)] public float lastDongleTime = 1f;
    
    // Particle
    public GameObject featherParticleSource;
    [HideInInspector] public List<GameObject> featherParticles = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCollisionDongle(Vector2 spawnPosition, DongleFeature.DongleKind collisionDongleKind)
    {
        Debug.Log("Merge Occur");
        
        int toMakeDongleKind = (int) collisionDongleKind + 1;

        if (toMakeDongleKind > (int)DongleFeature.DongleKind.Four)
        {
            // Final Effect and Score Up
            GameObject.Find("4_5").GetComponent<AudioSource>().Play();
            GameObject tempDongle = Instantiate(donglePrefabs[toMakeDongleKind], spawnPosition, Quaternion.identity);
            StartCoroutine(EndDongle(tempDongle));
        }
        else
        {
            Debug.Log("Spawn");
            GameObject.Find("1_4").GetComponent<AudioSource>().Play();

            
            // Feather Create
            bool featherPlayed = false;

            for (int i = 0; i < featherParticles.Count; i++)
            {
                GameObject tempFeather = featherParticles[i];
                
                if (!tempFeather.GetComponent<ParticleSystem>().isPlaying)
                {
                    tempFeather.transform.position = spawnPosition;
                    tempFeather.GetComponent<ParticleSystem>().Play();
                    featherPlayed = true;
                    
                    break;
                }
            }

            if (!featherPlayed)
            {
                GameObject newTempFeather = Instantiate(featherParticleSource, spawnPosition, Quaternion.identity,GameObject.Find("Feathers").transform);
                        
                featherParticles.Add(newTempFeather);
                newTempFeather.transform.position = spawnPosition;
                newTempFeather.GetComponent<ParticleSystem>().Play();
            }
            
            
            GameObject tempDongle = Instantiate(donglePrefabs[toMakeDongleKind], spawnPosition, Quaternion.identity);
            // have to change rotation?? -> Nope!

            StartCoroutine(DelayMerging(tempDongle));
        }
    }

    IEnumerator EndDongle(GameObject lastDongle)
    {
        yield return new WaitForSeconds(lastDongleTime);
        
        PointManager.Instance.Point += 100;
        Destroy(lastDongle);
    }

    IEnumerator DelayMerging(GameObject delayedDongle)
    {
        yield return new WaitForSeconds(mergeDelayTime);
        
        if (delayedDongle != null) delayedDongle.GetComponent<DongleFeature>().isMerging = false;
    }
}
