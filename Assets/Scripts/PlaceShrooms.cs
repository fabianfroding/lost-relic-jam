using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceShrooms : MonoBehaviour
{
    public GameObject shroomPrefab;
    public Camera cam;


    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        InputManager.OnLeftMouseDown += OnLeftMouseDown;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnLeftMouseDown()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = cam.nearClipPlane;
        Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(mousePosition);

        // OverlapBox checks if there is collider overlapping box created
        Collider2D detectedObject = Physics2D.OverlapPoint(mouseWorldPosition);

        if (detectedObject)
        {
            Debug.Log("Place in an empty area.");
        }
        else {
            if (SetShroomNr.shroomNr > 0)
            {
                GameObject newShroom = Instantiate(shroomPrefab, mouseWorldPosition, Quaternion.identity);
                SetShroomNr.shroomNr--;
            }
        }
        
    }

    
}
