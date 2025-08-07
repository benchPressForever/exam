using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private Dron[] _drones;
    [SerializeField] private Text _text;


    private const KeyCode scanKey = KeyCode.E;
    private Queue<Dron> dronesQueue = new Queue<Dron> ();
    private Queue<Resource> resourcesQueue = new Queue<Resource> ();
    private Transform target;
    private int countRes = 0;
    


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
                sendDrone(dronesQueue.Dequeue(),target);
            }
        }
        
    }


    private void sendDrone(Dron dron,Transform resource)
    {
        dron.takeComand(resource);
    }

    public void AddDron(Dron dron)
    {
        dronesQueue.Enqueue(dron);
    }

    public void AddResource()
    {
        countRes++;
        _text.text = "Resources :" + countRes.ToString();
    }
}
