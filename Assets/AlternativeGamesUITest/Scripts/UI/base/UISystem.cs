using UnityEngine;

namespace AlternativeGamesTest.UI
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private UIScreen[] screens;

        public void Init()
        {
            if (screens != null)
            {
                foreach (var screen in screens)
                {
                    screen.Init();
                }
            }
        }
    
        public T GetScreen<T>() where T : UIScreen
        {
            if (screens != null)
            {
                foreach (var screen in screens)
                {
                    if (screen is T typedScreen)
                    {
                        return typedScreen;
                    }
                }
            }

            return null;
        }

        [Button("Get Screens")]
        private void GetAllChildScreens()
        {
            screens = GetComponentsInChildren<UIScreen>();
        }
    }
}