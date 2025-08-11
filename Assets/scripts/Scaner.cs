using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaner : MonoBehaviour
{

    [SerializeField] private float _scanRadius;



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,_scanRadius);
    }

    public Queue<Resource> Scan(Queue<Resource> resources)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _scanRadius);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].TryGetComponent(out Resource res))
            {
                if(res.CheckResources() == false)
                {
                    resources.Enqueue(res);
                    res.Checked();
                    Debug.Log(resources.Count);
                }
            }
        }

        return resources;
        
    }

}
