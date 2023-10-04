using UnityEngine;

//I have decided to make some changes to the originally provided script for several reasons:
// 1) I really don't need it in my global namespace; it's not a big issue (unless there are many scripts like that), and it's also trivial to fix.
// 2) Having to tinker with distance and height feels exceptionally clunky, especially when they're rather obvious to calculate; this will make script much faster to reuse in other projects (altho consider Cinemachine for cameras, maybe?)
// 3) It is usually recommended that public fields\properties are written in PascalCase* (even if Unity often has properties in camelCase); as this is a singular script in a team of one, I have decided to adjust it.
// 4) I think other scripts have no business accessing some data from here (Distance/Height - perhaps only via possible methods such as AdjustFollowOffset, Damping via AdjustLinearDamping, AdjustAngularDamping).

// There are still some issues and improvements this script would want, but I decided not to spend more time here than I deemed necessary for this project.
// * Referencing official https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity
namespace ProvidedAssets.Utility
{
    public class SmoothFollow : MonoBehaviour
    {
        //Previously named Distance, it was not true distance between target and our transform, but rather Z-projected difference in their coordinates; thus, name is corrected
        [SerializeField] private float _forwardDistance = 10.0f;
        [SerializeField] private float _height = 5.0f;
        [SerializeField] private float _heightDamping = 2.0f;
        [SerializeField] private float _rotationDamping = 3.0f;
        //If we want old behaviour when everything was hardset via inspector, we can still have it by disabling this flag
        [SerializeField] private bool _adjustPerTarget = true;
        //Class name implies that we can follow a transform from the sides/front; original functionality did not reflect that, as such script could only be used specifically to move camera; disable checkbox for original functionality
        [SerializeField] private bool _keepRotaiton;
        [SerializeField] private bool _lookAtTarget = true;
        [SerializeField] private Transform _target;

        //Caching precalculated target allows us to avoid unneeded calculation; however, they are very light as of this introduction, thus caching may be optionally removed
        private Transform _calculatedTarget;

        private void Awake()
        {
            CalculateTargetOffsets();
        }

        private void CalculateTargetOffsets()
        {
            if (_adjustPerTarget && _target && _target != _calculatedTarget)
            {
                _forwardDistance =  transform.position.z - _target.position.z;
                if (!_keepRotaiton)
                    _forwardDistance = Mathf.Abs(transform.position.z - _target.position.z);
                _height = transform.position.y - _target.position.y;
                _calculatedTarget = _target;
            }
        }

        private void LateUpdate()
        {

            // Early out if we don't have a target
            if (!_target)
            {
                return;
            }

            float currentRotationAngle = transform.eulerAngles.y;
            float currentHeight = transform.position.y;
            float wantedHeight = _target.position.y + _height;

            if (!_keepRotaiton)
            {
                // Calculate the current rotation angles
                float wantedRotationAngle = _target.eulerAngles.y;

                // Damp the rotation around the y-axis
                currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);
            }

            // Damp the height
            currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

            // Convert the angle into a rotation
            Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

            // Set the position of the camera on the x-z plane to:
            // distance meters behind the target
            var newPosition = transform.position;
            newPosition = _target.position - currentRotation * Vector3.forward * _forwardDistance;
            newPosition.y = currentHeight;
            transform.position = newPosition;

            // Always look at the target
            if (_lookAtTarget)
                transform.LookAt(_target);
        }
    }
}