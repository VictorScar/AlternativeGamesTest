using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void OnActionPerfomed(InputAction.CallbackContext inputAction)
    {
        var inputDirection = inputAction.ReadValue<float>();
        Debug.Log($"Input with direction {inputDirection}");
        onInputPerfomed.Invoke((int)inputDirection);
    }
}
