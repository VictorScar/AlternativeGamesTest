using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInitializer : MonoBehaviour
{
    [SerializeField] private UISystem uiSystem;

    void Start()
    {
        uiSystem.Init();

        var screen = uiSystem.GetScreen<RatingScreen>();
        screen.Show();
    }
}