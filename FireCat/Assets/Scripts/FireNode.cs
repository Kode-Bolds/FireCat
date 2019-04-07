using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FireNode : MonoBehaviour
{
    [Header("Particle Effects")]
    public GameObject FireEffectPrefab;
    public GameObject SteamEffectPrefab;
    public float fireMaxScale = 1;
    public float fireMinScale = 0;

    [Header("Node Interations")]
    //public string TagForWaterCollider;
    public float TimeToSpread = 5;
    public float TimeToExtiguish = 2;
    public float WaitTimeReignite = 5;

    [Header("Point Scoring")]
    public GameObject PointObject;

    public bool OnFire
    {
        get { return _onFire; }
    }

    private bool _onFire = false;
    private float _timeSinceSpread = 0;
    private float _timeExtiguishing = 0;
    private bool _isBeingExtiguished = false;
    private GameObject _myFireObject = null;
    private GameObject _mySteamObject = null;
    private List<FireNode> _neighbors = new List<FireNode>();
    private bool _waitExtinguish = false;
    private float _timeSinceExtinguish = 0;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(_waitExtinguish)
        {
            _timeSinceExtinguish += Time.deltaTime;
            _mySteamObject.transform.localScale = Vector3.Lerp(new Vector3(fireMaxScale, fireMaxScale, fireMaxScale), new Vector3(fireMinScale, fireMinScale, fireMinScale), _timeSinceExtinguish / WaitTimeReignite);
            if (_timeSinceExtinguish > WaitTimeReignite)
            {
                _waitExtinguish = false;
                _timeSinceExtinguish = 0;
                Destroy(_mySteamObject);
            }
        }
        else if(_onFire)
        {
            _timeSinceSpread += Time.deltaTime;
            if(_timeSinceSpread > TimeToSpread)
            {
                _neighbors[Random.Range(0, _neighbors.Count)].SetOnFire();
                _timeSinceSpread = 0;
            }
            if(_isBeingExtiguished)
            {
                _timeExtiguishing += Time.deltaTime;
                if (_timeExtiguishing > TimeToExtiguish)
                {
                    Extinguish();
                }
            }
            else
            {
                if(_timeExtiguishing > 0)
                {
                    _timeExtiguishing -= Time.deltaTime;
                }
                else
                {
                    _timeExtiguishing = 0;
                }
            }
            _myFireObject.transform.localScale = Vector3.Lerp(new Vector3(fireMaxScale, fireMaxScale, fireMaxScale), new Vector3(fireMinScale, fireMinScale, fireMinScale), _timeExtiguishing / TimeToExtiguish);
        }
        _isBeingExtiguished = false;//resets so if the extiguisher stops colliding it stops 
    }

    public void SetOnFire()
    {
        if(_onFire || _waitExtinguish)
        {
            return;
        }
        _onFire = true;
        _myFireObject = Instantiate(FireEffectPrefab, transform);
        _timeExtiguishing = TimeToExtiguish - 0.1f;
        
        _myFireObject.transform.localScale = new Vector3(fireMaxScale, fireMaxScale, fireMaxScale);
    }

    public void Extinguish()
    {
        _onFire = false;
        _isBeingExtiguished = false;
        _waitExtinguish = true;
        Destroy(_myFireObject);
        _timeExtiguishing = 0;
        _timeSinceSpread = 0;
        _mySteamObject = Instantiate(SteamEffectPrefab, transform);
        _mySteamObject.transform.localScale = new Vector3(fireMaxScale, fireMaxScale, fireMaxScale);


        var p = Instantiate(PointObject, null);
        p.transform.position = transform.position;
        var rb = p.GetComponent<Rigidbody>();
        if(rb != null)
        {
            rb.AddForce(new Vector3(Random.Range(-100.0f, 100.0f), 100, Random.Range(-100.0f, 100.0f)));
        }
    }

    public void OnHit()
    {
        //print("I'm HIT!, Medic!");
        _isBeingExtiguished = true;
    }

    public void AddNeighbor(FireNode neighbor)
    {
        _neighbors.Add(neighbor);
        neighbor.AddNeighborOnce(this);
    }

    private void AddNeighborOnce(FireNode neighbor)
    {
        _neighbors.Add(neighbor);
    }
}
