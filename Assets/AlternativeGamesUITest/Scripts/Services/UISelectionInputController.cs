using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AlternativeGamesTest.Service
{
    public class UISelectionInputController : MonoBehaviour
    {
        [SerializeField] private InputActionReference uiSelectionActions;

        public event Action<int> onInputPerfomed;
    
        private void OnEnable()
        {
            if (uiSelectionActions != null)
            {
                uiSelectionActions.action.performed += OnActionPerfomed;
            }
        }

        public void OnDisable()
        {
            if (uiSelectionActions != null)
            {
                uiSelectionActions.action.performed -= OnActionPerfomed;
            }
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void OnActionPerfomed(InputAction.CallbackContext inputAction)
        {
            var inputDirection = inputAction.ReadValue<float>();
          // Debug.Log($"Input with direction {inputDirection}");
            onInputPerfomed?.Invoke((int)inputDirection);
        }
    }
}