using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetShroomNr : MonoBehaviour
{
    public const int InitialShroomNr = 6; 
    public static int shroomNr = InitialShroomNr;

    public TextMeshProUGUI shroomText;
    // Start is called before the first frame update
    void Start()
    {
        shroomText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        shroomText.text = shroomNr + "x";
    }
}
