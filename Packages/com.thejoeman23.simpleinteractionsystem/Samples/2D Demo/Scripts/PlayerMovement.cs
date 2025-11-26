using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleInteractionSystem2DSample
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        [SerializeField] private float leanAmount = 20f; // Degrees of lean, adjust as needed
        [SerializeField] private float leanSpeed = 10f; // How quickly to lean

        [SerializeField] private InputActionReference movementAction;
        
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Vector2 input = movementAction.action.ReadValue<Vector2>();
            input.y = 0;
            _rigidbody2D.linearVelocity = input * speed;
            
            // Calculate desired lean (-leanAmount when moving left, +leanAmount when right, 0 when idle)
            float targetLean = 0f;
            if (input.x != 0)
            {
                targetLean = leanAmount * Mathf.Sign(input.x);
            }

            // Smoothly interpolate Z rotation for lean
            float currentZ = transform.eulerAngles.z;
            // Lerp angle for better interpolation around 360 deg
            float newZ = Mathf.LerpAngle(currentZ, targetLean, Time.fixedDeltaTime * leanSpeed);

            Vector3 euler = transform.eulerAngles;
            euler.z = newZ;
            transform.eulerAngles = euler;
        }
    }
}