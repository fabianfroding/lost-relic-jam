using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance; // singleton

    public bool isStartExplode { get; private set; }

    public static event Action OnStartExplode;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GetStartExplode();
    }

    private void GetStartExplode()
    {
        isStartExplode = Input.GetKeyDown(KeyCode.Space);
        if (isStartExplode == true) { OnStartExplode?.Invoke(); }
    }
}