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
    private float _positionResY = 0.26f;


    void Start()
    {
        _wait = new WaitForSeconds(_delay);
        StartCoroutine(SpawnResources());
    }

    private IEnumerator SpawnResources()
    {
        while (enabled)
        {
            _spawnPosition = new Vector3(
                Random.Range(_min.position.x, _max.position.x), 
                _positionResY, 
                Random.Range(_min.position.z, _max.position.z));

            Instantiate(_prefab, _spawnPosition, Quaternion.identity, _container);
            yield return _wait;
        }

    }





}
