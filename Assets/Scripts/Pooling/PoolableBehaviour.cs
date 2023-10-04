using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Pooling
{
    //Poolable behaviour could be replaced by a component, attached to useful prefab\object; this would, however, lead to more casting and make code slightly slower and significantly more complex
    //A Component, however, would free us up from inheritance while maintaining flexibility and reuse of our code without breaking inspector
    //Thus I choose balance between speed and code reuse, which is supported by this class
    public abstract class PoolableBehaviour : MonoBehaviour
    {
        public event Action<PoolableBehaviour> OnDisposed;


        public virtual void PlaceInWorld(Vector3 position)
        {
            gameObject.SetActive(true);
            transform.position = position;
        }

        public virtual void Dispose()
        {
            gameObject.SetActive(false);
            OnDisposed?.Invoke(this);
        }
    }
}