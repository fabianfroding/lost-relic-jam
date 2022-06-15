using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetShroomNr : MonoBehaviour
{
    public static int shroomNr = 6;

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
