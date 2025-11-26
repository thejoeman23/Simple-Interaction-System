using System.Collections;
using UnityEngine;

namespace SimpleInteractionSystem
{
    public class PopupManager : MonoBehaviour
    {
        public static PopupManager Instance;

        [Header("Interact Popup Settings")]
        [SerializeField] private GameObject _interactButtonPrefab;
        private GameObject _currentInteractButton;

        [Header("Animation Settings")]
        [SerializeField] private float _animationDuration = .15f;
        [SerializeField] private AnimationCurve _animationCurve = AnimationCurve.EaseInOut(0,0,1,1);

        private Vector3 _originalButtonScale;

        private void Awake()
        {
            // Singleton pattern
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            _originalButtonScale = _interactButtonPrefab.transform.localScale;
        }

        public virtual void ChangeInteractableObject(Interactable interactable)
        {
            // If there is nothing to interact with then animate out/clear the interact button
            if (interactable == null)
            {
                AnimateOut();
                return;
            }

            // If there's an old button, destroy it
            if (_currentInteractButton != null)
            {
                Destroy(_currentInteractButton);
            }

            // Create and activate the new button
            _currentInteractButton = CreateInteractButton(interactable);
            _currentInteractButton.SetActive(true);

            // Animate in
            AnimateIn();
        }

        protected virtual GameObject CreateInteractButton(Interactable interactable)
        {
            GameObject button = Instantiate(_interactButtonPrefab, transform.parent);
            button.transform.position = interactable.transform.position + new Vector3(0, interactable.InteractPromptHeight, 0);
            button.SetActive(false); // will be enabled by ChangeInteractableObject
            button.transform.localScale = Vector3.zero; // start hidden if animating
            return button;
        }

        protected virtual void AnimateIn()
        {
            if (_currentInteractButton != null)
                StartCoroutine(AnimateInCoroutine());
        }

        private IEnumerator AnimateInCoroutine()
        {
            _currentInteractButton.transform.localScale = Vector3.zero;
            float timeElapsed = 0f;
            while (timeElapsed < _animationDuration)
            {
                timeElapsed += Time.deltaTime;
                float t = Mathf.Clamp01(timeElapsed / _animationDuration);
                float scaleT = _animationCurve.Evaluate(t);
                _currentInteractButton.transform.localScale = Vector3.Lerp(Vector3.zero, _originalButtonScale, scaleT);
                yield return null;
            }
            // Ensure final value
            _currentInteractButton.transform.localScale = _originalButtonScale;
        }

        protected virtual void AnimateOut()
        {
            if (_currentInteractButton != null)
                StartCoroutine(AnimateOutCoroutine());
        }

        private IEnumerator AnimateOutCoroutine()
        {
            Vector3 startScale = _currentInteractButton.transform.localScale;
            float timeElapsed = 0f;
            while (timeElapsed < _animationDuration)
            {
                timeElapsed += Time.deltaTime;
                float t = Mathf.Clamp01(timeElapsed / _animationDuration);
                float scaleT = _animationCurve.Evaluate(1f - t); // reverse curve
                _currentInteractButton.transform.localScale = Vector3.Lerp(Vector3.zero, startScale, scaleT);
                yield return null;
            }
            // Ensure scale is zero and destroy
            _currentInteractButton.transform.localScale = Vector3.zero;
            Destroy(_currentInteractButton);
            _currentInteractButton = null;
        }

        private void ClearInteractableButton()
        {
            // This method is now only needed if you want instant destruction w/o animation
            if (_currentInteractButton != null)
            {
                Destroy(_currentInteractButton);
                _currentInteractButton = null;
            }
        }
    }
}