using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleInteractionSystem3DSample
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private InputActionReference movementAction;
        [SerializeField] private float speed = 5f;
        private Rigidbody _rigidbody;

        void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            // Movement
            Vector2 input = movementAction.action.ReadValue<Vector2>();
            Vector3 move = new Vector3(input.x, 0, input.y) * speed;
            _rigidbody.linearVelocity = transform.TransformDirection(move);
        }
    }
}