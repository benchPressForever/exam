using UnityEngine;
using UnityEngine.EventSystems;

public class Ground : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform _containerBases;
    [SerializeField] private Transform _containerDrons;
    [SerializeField] private Transform _containerPoints;
    [SerializeField] private Base _basePrefab;
    [SerializeField] private Point _pointPrefab;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private Transform _flagPrefab;


    private Vector3 _lastPositionClick;
    private bool _baseSave = false;
    private float _positionBaseY = 0.1796f;
    private float _radiusBase = 0.24f;
    private Dron _dron;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _lastPositionClick = eventData.pointerCurrentRaycast.worldPosition;

        _lastPositionClick.y = _positionBaseY;

        Debug.Log($"������� � ����: {_lastPositionClick}");

        //��������� ����
        if (_baseSave)
        {
            
            SpawnBase();
            _baseSave = false;
        }
        

    }

    public void addClickBase(Dron dron)
    {
        _dron = dron;
        _baseSave = true;
        //���������� ���� � ������ �������� (��� ������� ������ ��������� ���� �� ������� ����� ����)
    }

    private void SpawnBase()
    {
        

        //����������� �����

        //��������� ����� 

        //�������� �����

        //��������� ����
        
        Base baseCenter = Instantiate(_basePrefab, _lastPositionClick, Quaternion.identity, _containerBases);
        baseCenter.Initialized(this, _containerDrons,_startPoint,_endPoint);
        baseCenter.replacePoints();
        baseCenter.AddDron(_dron);
        _dron.moveTarget(baseCenter.transform);
        _dron.Initialized(baseCenter.transform,baseCenter.getWayPoints());
        //������������� ���� 
        //��������� "���������� ����� � ���� ������ ��� ����������� ������ ���� ��� ����� , ����� , � ��"

        //���������� �����
           //�������� wayPoints � ��������� ������ �� �������
    }


}
