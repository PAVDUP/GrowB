using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TouchLaunch : MonoBehaviour
{
    // About Dongle
    [FormerlySerializedAs("DonglePrefab")] public GameObject[] donglePrefab;
    private GameObject _myDongle;
    private Rigidbody2D _myDongleRigidbody;

    // About Launch
    private bool _canLaunch;
    private Transform _startPosTransform;
    [Range(0, 10)] public float impulseStrength = 5;

    public bool CanLaunch
    {
        get => _canLaunch; 
        set => _canLaunch = value;
    }
    
    // About Random Dongle - Level Design
    public enum Level {L1, L2, L3};
    [Space (10)]
    [Header("Level Design")]
    public Level myLevel = Level.L1;
    [Space (5)]
    public float[] level1 = new float[6];
    public float[] level2 = new float[6];
    public float[] level3 = new float[6];
    
    void Start()
    {
        // Dongle -> Launched Dongle need variability (처음 생성되는 Dongle의 다변화 필요)
        _canLaunch = true;
        _startPosTransform = GameObject.Find("StartPos").transform;

        // Launch
        SpawnDongle();
    }

    void SpawnDongle()
    {
        // Random Spawn
        switch (myLevel)
        {
            case Level.L1 :
                RandomSpawn(level1);
                break;
            case Level.L2 :
                RandomSpawn(level2);
                break;
            case Level.L3 :
                RandomSpawn(level3);
                break;
        }

        // Initial Setting
        _myDongleRigidbody = _myDongle.GetComponent<Rigidbody2D>();
        _myDongleRigidbody.bodyType = RigidbodyType2D.Kinematic;

        _myDongle.transform.parent = GameObject.Find("Dongles").transform;
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
                _myDongle = Instantiate(donglePrefab[i], _startPosTransform.position, Quaternion.identity);
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_canLaunch)
        {
            Vector2 touchPosition;
            
            if (TryGetTouchPosition(out touchPosition))
            {
                LaunchDongle(touchPosition);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                touchPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Debug.Log("Mouse Dongle : x - " + Input.mousePosition.x +" / y - " + Input.mousePosition.y);
                
                LaunchDongle(touchPosition);
            }
        }
    }

    private bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        // only one touch 
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }
        touchPosition = default;
        return false;
    }
    
    void LaunchDongle(Vector2 touchPosition)
    {
        if (_myDongle == null) return;
        
        GameObject.Find("Click").GetComponent<AudioSource>().Play();
        
        touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        
        _canLaunch = false;
        _myDongleRigidbody.bodyType = RigidbodyType2D.Dynamic;
        
        var position = _startPosTransform.position;
        Vector2 launchdirection = touchPosition - new Vector2(position.x, position.y);
        _myDongle.GetComponent<DongleFeature>().isMerging = false;
                
        _myDongle.GetComponent<Rigidbody2D>().AddForce(launchdirection.normalized * impulseStrength, ForceMode2D.Impulse);
        StartCoroutine(GameOverDongleCheck(_myDongle));
        
        _myDongle = null;
        _myDongleRigidbody = null;

        StartCoroutine(WaitAndSpawnDongle());
    }

    IEnumerator GameOverDongleCheck(GameObject toOverDongle)
    {
        yield return new WaitForSeconds(2f);
        
        if (toOverDongle != null) toOverDongle.GetComponent<DongleFeature>().overDongleCheck = true;
    }

    IEnumerator WaitAndSpawnDongle()
    {
        yield return new WaitForSeconds(0.3f);
        
        SpawnDongle();
        _canLaunch = true;
    }
}
