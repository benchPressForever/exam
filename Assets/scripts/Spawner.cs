using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _delay = 3;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _min;
    [SerializeField] private Transform _max;
    [SerializeField] private Resource _prefab;


    private WaitForSeconds _wait;
    private Vector3 _spawnPosition;


    void Start()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            _spawnPosition = new Vector3(
                Random.Range(_min.position.x, _max.position.x), 
                0.26f, 
                Random.Range(_min.position.z, _max.position.z));

            Instantiate(_prefab, _spawnPosition, Quaternion.identity, _container);
            yield return _wait;
        }

    }


}
