using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetShroomNr : MonoBehaviour
{
    [Tooltip("Number of placeable mushrooms")]
    public int InitialShroomNr = 6;
    public static int shroomNr;

    public TextMeshProUGUI shroomText;
    // Start is called before the first frame update
    void Start()
    {
        shroomNr = InitialShroomNr;
        shroomText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        shroomText.text = shroomNr + "x";
    }
}
