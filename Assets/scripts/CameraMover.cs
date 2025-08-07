using UnityEngine;

public class CameraMover : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";


    private float _directionX;
    private float _directionZ;
    
    void Start()
    {
        
    }

    void Update()
    {
        _directionX = Input.GetAxis(Horizontal);
        _directionZ = Input.GetAxis(Vertical);

        transform.Translate(new Vector3(
            _directionX*_speed*Time.deltaTime,0,
            _directionZ * _speed * Time.deltaTime));


        if(_startPoint.position.x > transform.position.x)
        {
            transform.position = new Vector3(_startPoint.position.x,transform.position.y, transform.position.z);
        }
        else if(_endPoint.position.x < transform.position.x)
        {
            transform.position = new Vector3(_endPoint.position.x, transform.position.y, transform.position.z);

        }


        if(_startPoint.position.z > transform.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y,_startPoint.position.z);
        }
        else if(_endPoint.position.z < transform.position.z)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y, _endPoint.position.z);

        }
    }
}
