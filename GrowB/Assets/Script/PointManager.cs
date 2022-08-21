using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointManager : MonoBehaviour
{
    #region Singleton Pattern

    private static PointManager _instance = null;

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
    
    public static PointManager Instance
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

    private int _point;
    private TextMeshProUGUI _pointText;

    public int Point
    {
        get => _point;
        set
        {
            _point = + value;
            _pointText.text = _point.ToString();

            if (_point >= 7000)
            {
                FindObjectOfType<TouchLaunch>().myLevel = TouchLaunch.Level.L3;
            }
            else if (_point >= 3000)
            {
                FindObjectOfType<TouchLaunch>().myLevel = TouchLaunch.Level.L2;
            }
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _pointText = GameObject.Find("ScorePoint").GetComponent<TextMeshProUGUI>();
        Point = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
