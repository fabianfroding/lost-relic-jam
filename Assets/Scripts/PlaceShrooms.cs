using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlaceShrooms : MonoBehaviour
{
    public GameObject shroomPrefab;
    public Camera cam;
    public ExplosiveSelect explosiveSelect;

    public GameObject shroomPlacementErrorUI;


    public static event Action OnShroomPlaceError;
    public static event Action OnShroomPlaced;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        explosiveSelect = GetComponent<ExplosiveSelect>();
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
            shroomPlacementErrorUI.SetActive(true);
            OnShroomPlaceError?.Invoke();
            // shroomPlacementErrorUI.GetComponent<FadeUI>().ShowUI();
            // shroomPlacementErrorUI.GetComponent<FadeUI>().FadeOutUI();
        }
        else {
            if (explosiveSelect.hasInfiniteTriggers || !explosiveSelect.hasTriggeredBarrel)
            {
                if (SetShroomNr.shroomNr > 0)
                {
                    GameObject newShroom = Instantiate(shroomPrefab, mouseWorldPosition, Quaternion.identity);
                    SetShroomNr.shroomNr--;
                    OnShroomPlaced?.Invoke();
                }
            }
        }
        
    }

    // public void DelayedUIDisable(GameObject targetUI)
    // {
    //     StopCoroutine(DisableUIAfterDelay(targetUI));
    //     StartCoroutine(DisableUIAfterDelay(targetUI));
    // }

    // private IEnumerator DisableUIAfterDelay(GameObject targetUI)
    // {
    //     yield return new WaitForSeconds(2);
    //     targetUI.SetActive(false);
    // }

    private void OnDestroy()
    {
        InputManager.OnLeftMouseDown -= OnLeftMouseDown;
    }

    
}
