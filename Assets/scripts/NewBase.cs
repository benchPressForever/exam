using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NewBase : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private NewDron[] _drones;
    [SerializeField] private Text _text;


    //-----------------
    [SerializeField] private Ground _ground;
    [SerializeField] private Transform _containerDrones;
    [SerializeField] private NewDron _dronPrefab;
    [SerializeField] private Transform[] _waysPoints;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    //----------------




    private const KeyCode scanKey = KeyCode.E;
    private Queue<NewDron> dronesQueue = new Queue<NewDron>();
    private Queue<Resource> resourcesQueue = new Queue<Resource>();
    private Transform target;
    private int countRes = 0;



    //--------------
    private Vector3 _spawnPosition;
    private float _positionDronY = 0.119f;
    private bool _isBuildFlag = false;
    //---------------




    private void Awake()
    {
        for (int i = 0; i < _drones.Length; i++)
        {
            dronesQueue.Enqueue(_drones[i]);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(scanKey))
        {
            resourcesQueue = _scaner.Scan(resourcesQueue);
        }
        if (dronesQueue.Count > 0)
        {
            target = resourcesQueue.Count > 0 ? resourcesQueue.Dequeue().transform : null;

            if (target != null)
            {
                sendDrone(dronesQueue.Dequeue(), target);
            }
        }



        //--------------------------
        if (countRes >= 3 && _isBuildFlag == false  /*&& Input.GetKeyDown(addDronKey)*/)
        {
            SpawnDron();
        }

        if (countRes >= 5 && _isBuildFlag)
        {
            _isBuildFlag = false;
            NewDron dronDel = dronesQueue.Dequeue();
            _ground.addClickBase(dronDel);
            countRes -= 5;
            _text.text = "Resources :" + countRes.ToString();

        }
        //-----------------------------


    }


    private void sendDrone(NewDron dron, Transform resource)
    {
        dron.takeComand(resource);
    }

    public void AddDron(NewDron dron)
    {
        dronesQueue.Enqueue(dron);
    }

    public void AddCountResource()
    {
        countRes++;
        _text.text = "Resources :" + countRes.ToString();
    }




    //--------------------------------------------------
    private void SpawnDron()
    {
        _spawnPosition = new Vector3(
                                _waysPoints[0].position.x,
                                _positionDronY,
                                _waysPoints[0].position.z);

        NewDron dron = Instantiate(_dronPrefab, _spawnPosition, Quaternion.identity, _containerDrones);

        dron.Initialized(this.transform, _waysPoints);

        countRes = countRes - 3;
        _text.text = "Resources : " + countRes.ToString();
        dronesQueue.Enqueue(dron);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (dronesQueue.Count > 1)
        {
            _ground.AddFlag(this);
            _isBuildFlag = true;
        }
    }


    public void replacePoints()
    {
        for (int i = 0; i < _waysPoints.Length; i++)
        {
            if (_startPoint.position.x > _waysPoints[i].position.x)
            {
                _waysPoints[i].position = new Vector3(_startPoint.position.x, _waysPoints[i].position.y, _waysPoints[i].position.z);
            }
            else if (_endPoint.position.x < _waysPoints[i].position.x)
            {
                _waysPoints[i].position = new Vector3(_endPoint.position.x, _waysPoints[i].position.y, _waysPoints[i].position.z);

            }


            if (_startPoint.position.z > _waysPoints[i].position.z)
            {
                _waysPoints[i].position = new Vector3(_waysPoints[i].position.x, _waysPoints[i].position.y, _startPoint.position.z);
            }
            else if (_endPoint.position.z < _waysPoints[i].position.z)
            {
                _waysPoints[i].position = new Vector3(_waysPoints[i].position.x, _waysPoints[i].position.y, _endPoint.position.z);

            }
        }
    }

    public Transform[] getWayPoints()
    {
        return _waysPoints;
    }

    public void Initialized(Ground ground, Transform containerDrones, Transform startPoint, Transform endPoint)
    {
        _ground = ground;
        _containerDrones = containerDrones;
        _startPoint = startPoint;
        _endPoint = endPoint;
    }

    public void BuildFlag()
    {
        _isBuildFlag = true;
    }
    //--------------------------------------------------------

}
