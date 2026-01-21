using Player.Domain.Input;
using Player.Domain.PlayerStateMachine;
using UniRx;
using UnityEngine;
using Zenject;

namespace Player.Presentation
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMover : MonoBehaviour
    {
        public StateHandleChain StateHandleChain;

        private Rigidbody _rb;
        private PlayerStateMachineData _stateData;
        private IInputService _inputService;
        private Camera _mainCamera;

        [Inject]
        private void Construct(
            IInputService inputService,
            PlayerStateMachineData stateData,
            StateHandleChain stateHandleChain)
        {
            _inputService = inputService;
            _stateData = stateData;
            StateHandleChain = stateHandleChain;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _mainCamera = Camera.main;

            _inputService.OnDashPressed
                .Subscribe(_ => _stateData.DashRequested.Value = true)
                .AddTo(this);
        }

        private void Update()
        {
            _stateData.MovementInput = _inputService.MovementInput;

            if (_stateData.DashCooldownTimer > 0f)
                _stateData.DashCooldownTimer -= Time.deltaTime;
        }

        public void Move(Vector3 direction)
        {
            _rb.linearVelocity = new Vector3(direction.x, _rb.linearVelocity.y, direction.z);
        }

        public void Stop()
        {
            _rb.linearVelocity = new Vector3(0f, _rb.linearVelocity.y, 0f);
        }

        public Vector3 GetWorldMovementDirection(Vector2 input)
        {
            if (_mainCamera == null) return new Vector3(input.x, 0f, input.y);

            Vector3 camForward = _mainCamera.transform.forward;
            camForward.y = 0f;
            camForward.Normalize();

            Vector3 camRight = _mainCamera.transform.right;
            camRight.y = 0f;
            camRight.Normalize();

            Vector3 worldDir = camForward * input.y + camRight * input.x;
            return worldDir.normalized;
        }
    }
}