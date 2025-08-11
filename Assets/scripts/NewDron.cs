using UnityEngine;

public class NewDron : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _base;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private Transform _cargoPlace;


    private int currentWayPoint = 0;
    private bool isHaveCommand = false;
    private bool isHaveResourse = false;
    private Transform target;
    private Resource tempResource;


    private void Update()
    {

        if (isHaveCommand == true)
        {
            moveTarget(target);

        }
        else if (isHaveResourse == true)
        {
            moveTarget(_base);
        }
        else
        {
            freeMove();
        }
    }

    public void moveTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        transform.LookAt(target.position);
    }



    private void freeMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[currentWayPoint].position, _speed);
        transform.LookAt(_wayPoints[currentWayPoint]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Point point))
        {
            currentWayPoint = ++currentWayPoint % _wayPoints.Length;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out NewBase baseCenter))
        {
            if (isHaveResourse)
            {
                UnloadCargo();
                baseCenter.AddCountResource();
                baseCenter.AddDron(this);
            }


        }
        else if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            if (target != null)
            {
                if (resource.transform.position == target.position)
                {
                    LoadCargo(resource);
                }
            }


        }
    }

    public void takeComand(Transform resource)
    {
        target = resource;
        isHaveCommand = true;
    }

    private void UnloadCargo()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out Resource resource))
            {
                tempResource = resource;
                tempResource.transform.parent = null;
                isHaveResourse = false;
                target = null;
                resource.Destroyed();
                tempResource = null;
                break;

            }
        }

    }

    private void LoadCargo(Resource resource)
    {
        resource.transform.SetParent(this.transform);
        resource.transform.position = _cargoPlace.position;
        isHaveResourse = true;
        isHaveCommand = false;
    }

    public void Initialized(Transform bases, Transform[] ways)
    {
        _base = bases;
        _wayPoints = ways;
    }

}
