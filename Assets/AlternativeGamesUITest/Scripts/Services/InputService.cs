using UnityEngine;

namespace AlternativeGamesTest.Service
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private UISelectionInputController uiSelectionController;

        public UISelectionInputController UISelectionController => uiSelectionController;

        public void Init()
        {
            uiSelectionController.Disable();
        }
    }
}