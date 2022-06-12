using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance; // singleton

    public bool isStartExplode { get; private set; }
    public bool isLeftMouseDown { get; private set; }

    public static event Action OnStartExplode;
    public static event Action OnLeftMouseDown;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GetStartExplode();
        GetLeftMouseDown();
    }

    private void GetStartExplode()
    {
        isStartExplode = Input.GetKeyDown(KeyCode.Space);
        if (isStartExplode == true) { OnStartExplode?.Invoke(); }
    }
    private void GetLeftMouseDown()
    {
        isLeftMouseDown = Input.GetMouseButtonDown(0);
        if (isLeftMouseDown == true) { OnLeftMouseDown?.Invoke(); }
    }
}