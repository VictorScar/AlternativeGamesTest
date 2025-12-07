using UnityEngine;

namespace AlternativeGamesTest.UI.Base
{
    public class UISystem : MonoBehaviour
    {
        [SerializeField] private UIScreen[] screens;
        [SerializeField] private UIAnimationsRunner animationsRunner;
        
        public void Init()
        {
            if (screens != null)
            {
                foreach (var screen in screens)
                {
                    screen.Init(animationsRunner);
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