using UnityEngine;
using UnityEngine.InputSystem;

namespace SimpleInteractionSystem
{
    [AddComponentMenu("Interaction System/Interactor3D")]
    public class Interactor3D : MonoBehaviour
    {
        [Header("Input")]
        [Tooltip("Input Action used to trigger interactions (e.g. Player/Interact).")]
        [SerializeField] private InputActionReference interactKey;

        [Header("Interaction Settings")]
        [Tooltip("Maximum distance to search for interactable objects.")]
        [SerializeField] private float interactRadius = 5f;
        
        [Header("Gizmos Settings")]
        [Tooltip("Draw the Gizmos for the interactable objects.")]
        [SerializeField] private bool showGizmos = true;
        [SerializeField] private Color gizmosColor = Color.white;

        private Interactable nearestInteractable;

        private void Update()
        {
            UpdateNearestInteractable();

            if (interactKey != null && interactKey.action.IsPressed() && nearestInteractable != null)
            {
                Debug.Log("Interacting with " + interactKey.action.name);
                nearestInteractable.Interact();
            }
        }

        protected virtual void UpdateNearestInteractable()
        {
            Vector3 spherePos = transform.position - new Vector3(0, transform.localScale.y / 2, 0);
            Collider[] colliders = Physics.OverlapSphere(spherePos, interactRadius);
            
            Debug.Log(colliders.Length);

            float nearestDist = Mathf.Infinity;
            Interactable found = null;

            foreach (Collider col in colliders)
            {
                if (col.TryGetComponent(out Interactable interactable) && interactable.CanInteract)
                {
                    float dist = Vector3.Distance(col.transform.position, transform.position);

                    if (dist < nearestDist)
                    {
                        nearestDist = dist;
                        found = interactable;
                    }
                }
            }

            if (found != nearestInteractable)
            {
                Debug.Log("Found One!");
                nearestInteractable = found;
                OnNearestInteractableChanged(found);
            }
        }

        protected virtual void OnNearestInteractableChanged(Interactable interactable)
        {
            if (PopupManager.Instance != null)
                PopupManager.Instance.ChangeInteractableObject(nearestInteractable);
        }

        private void OnDrawGizmosSelected()
        {
            if (!showGizmos)
                return;
            
            Gizmos.color = gizmosColor;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
    }
}