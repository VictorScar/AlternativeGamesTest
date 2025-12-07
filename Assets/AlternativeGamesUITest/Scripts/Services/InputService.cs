using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class InputService : MonoBehaviour
{
    [FormerlySerializedAs("uiSelectionInputController")] [SerializeField] private UISelectionInputController uiSelectionController;

    public UISelectionInputController UISelectionController => uiSelectionController;
}
