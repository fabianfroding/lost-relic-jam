using UnityEngine;


public class ExplosiveSelect : MonoBehaviour 
{
    public Camera cam;

    public GameObject selectedBarrel;

    void Start()
    {
        InputManager.OnLeftMouseDown += OnLeftMouseDown;
        InputManager.OnStartExplode += OnStartExplodeClicked;
    }

    void Update()
    {

    }
    
    private void OnStartExplodeClicked()
    {
        if (selectedBarrel != null)
        {
            Destroy(selectedBarrel);
        }
        else {
            Debug.Log("no selected barrel!");
        }
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
            if (detectedObject.GetComponent<Explosive>() != null)
            {
                // deselect previous
                if (selectedBarrel != null)
                {
                    selectedBarrel.GetComponent<Explosive>().SetDeselect();
                }
                // select new
                selectedBarrel = detectedObject.transform.gameObject;
                selectedBarrel.GetComponent<Explosive>().SetSelected();
            }
        }
        
    }
}