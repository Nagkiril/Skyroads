using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Skyroad
{
    public class TransformAxisSpinner : MonoBehaviour
    {
        [SerializeField] SpinAxis _spinAxis;
        [SerializeField] float spinOffset;

        private enum SpinAxis
        {
            X,
            Y,
            Z
        }

        //We're using FixedUpdate so that transform won't spin faster\slower due to framerate
        private void FixedUpdate()
        {
            //We're not using DOTween here, because we need the spin to be infinite
            var eulerRotations = transform.rotation.eulerAngles;
            switch (_spinAxis)
            {
                case SpinAxis.X:
                    eulerRotations.x += spinOffset;
                    break;
                case SpinAxis.Y:
                    eulerRotations.y += spinOffset;
                    break;
                case SpinAxis.Z:
                    eulerRotations.z += spinOffset;
                    break;
            }
            transform.rotation = Quaternion.Euler(eulerRotations);
        }
    }
}