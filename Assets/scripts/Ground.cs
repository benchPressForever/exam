using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _containerBases;
    [SerializeField] private Transform _containerDrons;
    [SerializeField] private Transform _containerPoints;
    [SerializeField] private NewBase _basePrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Flag _flagPrefab;


    private Vector3 _lastPositionClick;
    private bool _baseSave = false;
    private bool _flagInstall = false;
    private float _positionBaseY = 0.1796f;
    private float _positionFlagY = 0.586f;
    private NewDron _dron;
    private NewBase _parentBase;
    private Flag _flag;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _lastPositionClick = eventData.pointerCurrentRaycast.worldPosition;
        //постройка флага
        if (_baseSave && _flagInstall == false)
        {
            SpawnFlag();
            _parentBase.BuildFlag();
            _flagInstall = true;
        }
        else if (_flagInstall)
        {
            SwapPositionFlag();
        }
        

    }

    

    public void AddFlag(NewBase parentBase)
    {
        _baseSave = true;
        _parentBase = parentBase;
    }

    public void addClickBase(NewDron dron)
    {
        _flagInstall = false;
        if(_flag != null)
        {
            _flag.Destroed();
        }
        _dron = dron;
        SpawnBase();
       
    }

    private void SpawnBase()
    {

        _lastPositionClick.y = _positionBaseY;
        NewBase baseCenter = Instantiate(_basePrefab, _lastPositionClick, Quaternion.identity, _containerBases);
        baseCenter.Initialized(this, _containerDrons,_startPoint,_endPoint);
        baseCenter.replacePoints();
        baseCenter.AddDron(_dron);
        _dron.moveTarget(baseCenter.transform);
        _dron.Initialized(baseCenter.transform,baseCenter.getWayPoints());
        _flag.Destroed();
    }


    private void SpawnFlag()
    {
        _lastPositionClick.y = _positionFlagY;
        _flag = Instantiate(_flagPrefab, _lastPositionClick, Quaternion.identity, this.transform);
    }

    private void SwapPositionFlag()
    {
        _flag.transform.position = _lastPositionClick;
    }



}
