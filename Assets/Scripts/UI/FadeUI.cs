using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup targetUIGroup;
    private bool fadeIn;
    private bool fadeOut;

    private void Awake()
    {
        targetUIGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (fadeIn)
        {
            if (targetUIGroup.alpha < 1)
            {
                targetUIGroup.alpha += Time.deltaTime;
                if (targetUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if (fadeOut)
        {
            if (targetUIGroup.alpha >= 0)
            {
                targetUIGroup.alpha -= Time.deltaTime;
                if (targetUIGroup.alpha == 0)
                {
                    fadeOut = false;
                }
            }
        }
    }

    public void ShowUI()
    {
        targetUIGroup.alpha = 1;   
    }

    public void HideUI()
    {
        targetUIGroup.alpha = 0;   
    }

    public void FadeInUI()
    {
        fadeIn = true;
    }
    
    public void FadeOutUI()
    {
        fadeOut = true;
    }
}