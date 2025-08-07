using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class Dron : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _base;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private Transform _cargoPlace;


    private int currentWayPoint = 0;
    private bool isHaveCommands = false;
    private bool isHaveResourse = false;
    private Transform target;
    private Resource tempResource;


    private void Update()
    {
        if (isHaveCommands)
        {
            moveTarget(target);
        }
        else if (isHaveResourse)
        {
            moveTarget(_base);
        }
        else
        {
            freeMove();
        }
    }

    private void moveTarget(Transform target)
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed);
        transform.LookAt(target.position);
    }



    private void freeMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, _wayPoints[currentWayPoint].position,_speed);
        transform.LookAt(_wayPoints[currentWayPoint]);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out Point point))
        {
            currentWayPoint = ++currentWayPoint % _wayPoints.Length;

        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Base baseCenter))
        {
            UnloadCargo(baseCenter);
            baseCenter.AddDron(this);
        }
        else if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            if(target != null)
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
        isHaveCommands = true;
    }

    private void UnloadCargo(Base baseCenter)
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.TryGetComponent(out Resource resource))
            {
                tempResource = resource;

                tempResource.transform.parent = null;
                isHaveResourse = false;
                target = null;
                Destroy(tempResource);

                baseCenter.AddResource();
            }
        }
       
    }

    private void LoadCargo(Resource resource)
    {
        resource.transform.SetParent(this.transform);
        resource.transform.position = _cargoPlace.position;
        isHaveResourse = true;
        isHaveCommands = false;
    }
}
