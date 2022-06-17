using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance; // singleton

    public bool isStartExplode { get; private set; }
    public bool isLeftMouseDown { get; private set; }
    // Debug Commands
    public bool infiniteSelectsAllowed { get; private set; }


    public static event Action OnStartExplode;
    public static event Action OnLeftMouseDown;
    // Debugs
    public static event Action OnInfiniteSelectsAllowed;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GetStartExplode();
        GetLeftMouseDown();
        GetInfiniteClicksAllowed();
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

    // Debug Commands
    private void GetInfiniteClicksAllowed()
    {
        infiniteSelectsAllowed = Input.GetKeyDown(KeyCode.Q);
        if (infiniteSelectsAllowed == true) { OnInfiniteSelectsAllowed?.Invoke(); }
    }
}