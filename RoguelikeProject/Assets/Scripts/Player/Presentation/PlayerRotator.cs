using Player.Domain.Input;
using UnityEngine;
using Zenject;

namespace Player.Presentation
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerRotator : MonoBehaviour
    {
        private Camera _mainCamera;
        private IInputService _inputService;
        private Rigidbody _rb;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Awake()
        {
            _mainCamera = Camera.main;
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            RotateTowardsMouse();
        }

        private void RotateTowardsMouse()
        {
            Plane playerPlane = new Plane(Vector3.up, _rb.position);
            Ray ray = _mainCamera.ScreenPointToRay(_inputService.MousePosition);

            if (playerPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 direction = hitPoint - _rb.position;
                direction.y = 0;

                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    
                    Quaternion nextRotation = Quaternion.Slerp(_rb.rotation, targetRotation, Time.fixedDeltaTime * 15f);
                    _rb.MoveRotation(nextRotation);
                }
            }
        }
    }
}