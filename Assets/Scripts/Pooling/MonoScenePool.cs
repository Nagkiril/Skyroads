using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Pooling
{
    //We only expect to pool MonoBehaviours, thus we don't make a *completely* abstract pool
    //It may not be fastest implementation (and it limits poolable objects by using inheritance), but between speed, reusability, complexity I found this to be best suited solution
    public class MonoScenePool<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected PoolableBehaviour pooledPrefab;
        //We really don't need LIFO here, even a simple list could do, but just so that
        Queue<PoolableBehaviour> _pooledObjects;

        private void Awake()
        {
            SetupPool();
        }

        private void SetupPool()
        {
            if (_pooledObjects == null)
                _pooledObjects = new Queue<PoolableBehaviour>();
        }

        private void AcceptNewObstacle(PoolableBehaviour poolable)
        {
            _pooledObjects.Enqueue(poolable);
        }


        public T Get()
        {
            SetupPool();
            if (_pooledObjects.Count > 0)
            {
                //Not a big fan of having to cast each time we pull something, but at least it's not reflection...
                return (_pooledObjects.Dequeue() as T);
            }
            else
            {
                //We could potentially free up inheritance by using label components, but that might be overkill here
                PoolableBehaviour newInstance = Instantiate(pooledPrefab, transform);
                newInstance.OnDisposed += AcceptNewObstacle;
                return (newInstance as T);
            }
        }

    }
}