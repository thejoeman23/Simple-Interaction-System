using System;
using UnityEngine;

namespace SimpleInteractionSystem3DSample
{
    public class LookAtCameraAndHover : MonoBehaviour
    {
        [Header("Hover Settings")]
        [SerializeField] private float hoverAmplitude = 0.5f; // Max vertical displacement
        [SerializeField] private float hoverFrequency = 1f; 
        
        private Vector3 _startPosition;
        private Camera _camera;

        void Start()
        {
            _startPosition = transform.position;
            _camera = Camera.main;
        }

        private void Update()
        {
            if (_camera == null)
            {
                Debug.LogError("No Main Camera");
                return;
            }
            
            transform.LookAt(_camera.transform);
            
            float offsetY = Mathf.Sin(Time.time * hoverFrequency * Mathf.PI * 2f) * hoverAmplitude;
            transform.localPosition = _startPosition + new Vector3(0f, offsetY, 0f);
        }
    }
}