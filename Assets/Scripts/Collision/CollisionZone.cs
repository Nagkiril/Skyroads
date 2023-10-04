using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad.Collision
{
    public class CollisionZone<T> : MonoBehaviour
    {
        public event Action<T> OnEnter;
        public event Action<T> OnExit;

        //We don't necessarily need to use the List, it could potentially be a Dictionary, but List will allow us to modify behaviour easier and there's not much in favor of anything specific
        //I would still prefer to use collection to allow for more utility (will likely make this component reusable), and to validate collision system which I found sometimes unreliable
        protected List<T> _objectsInCollider;

        //We're using IEnumerable to expose what's inside this collider: that way collection should not be modifiable
        public IEnumerable<T> ObjectsInside => _objectsInCollider;

        protected virtual void Awake()
        {
            _objectsInCollider = new List<T>();
        }

        private void OnDisable()
        {
            _objectsInCollider = new List<T>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            //We're using attached rigidbody as a starting point, allowing us to utilize this even with complex colliders
            var potentialEntrant = other.attachedRigidbody.GetComponent<T>();
            if (potentialEntrant != null && !_objectsInCollider.Contains(potentialEntrant))
            {
                _objectsInCollider.Add(potentialEntrant);
                NotifyObjectEntry(potentialEntrant);
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            var potentialExitor = other.attachedRigidbody.GetComponent<T>();
            if (potentialExitor != null && _objectsInCollider.Contains(potentialExitor))
            {
                _objectsInCollider.Remove(potentialExitor);
                NotifyObjectExit(potentialExitor);
            }
        }

        //We'er using special methods to invoke our events, because this way subclasses can fire them as well (while maintaining benefits of having)
        protected virtual void NotifyObjectEntry(T target)
        {
            OnEnter?.Invoke(target);
        }

        protected virtual void NotifyObjectExit(T target)
        {
            OnExit?.Invoke(target);
        }
    }
}