using System;
using UnityEngine;

namespace SimpleInteractionSystem
{
    public class InteractButtonRotater : MonoBehaviour
    {
        Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            transform.LookAt(_camera.transform);
        }
    }   
}
