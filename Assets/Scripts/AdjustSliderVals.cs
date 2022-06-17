using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdjustSliderVals : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public void AdjustSliderValue(float val)
    {
        slider.value = val;
    }
}
