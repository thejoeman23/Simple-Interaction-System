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
        
        void Start() => _startPosition = transform.position;
        
        private void Update()
        {
            transform.LookAt(Camera.main.transform);
            
            float offsetY = Mathf.Sin(Time.time * hoverFrequency * Mathf.PI * 2f) * hoverAmplitude;
            transform.localPosition = _startPosition + new Vector3(0f, offsetY, 0f);
        }
    }
}