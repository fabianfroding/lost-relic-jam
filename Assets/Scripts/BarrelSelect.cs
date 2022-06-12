using UnityEngine;


public class BarrelSelect : MonoBehaviour 
{
    public Camera camera;
    public Vector2 detectionBoxSize = new Vector2(0.2f, 0.2f);
    public float rotationAngle = 0;

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
            selectedBarrel.GetComponent<Barrel>().Explosion();
        }
        else {
            Debug.Log("no selected barrel!");
        }
    }

    private void OnLeftMouseDown()
    {
        
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = camera.nearClipPlane;
        Vector2 mouseWorldPosition = camera.ScreenToWorldPoint(mousePosition);

        // OverlapBox checks if there is collider overlapping box created
        Collider2D detectedObject = Physics2D.OverlapPoint(mouseWorldPosition);

        if (detectedObject)
        {
            if (detectedObject.GetComponent<Barrel>() != null)
            {
                // deselect previous
                if (selectedBarrel != null)
                {
                    selectedBarrel.GetComponent<Barrel>().SetDeselect();
                }
                // select new
                selectedBarrel = detectedObject.transform.gameObject;
                selectedBarrel.GetComponent<Barrel>().SetSelected();
            }
        }
        
    }
}