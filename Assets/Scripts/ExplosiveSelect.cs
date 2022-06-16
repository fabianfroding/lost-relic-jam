using UnityEngine;


public class ExplosiveSelect : MonoBehaviour 
{
    public Camera cam;

    public GameObject selectedBarrel;
    public bool hasTriggeredBarrel;   // allow only exploding 1 explosive 
    public bool hasInfiniteTriggers;  // "Q" - allow exploding all the explosives

    void Start()
    {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        InputManager.OnLeftMouseDown += OnLeftMouseDown;
        InputManager.OnStartExplode += OnStartExplodeClicked;
        InputManager.OnInfiniteSelectsAllowed += OnInfiniteSelectsAllowed;
    }

    void Update()
    {

    }
    
    private void OnStartExplodeClicked()
    {
        if (selectedBarrel != null)
        {
            if (hasInfiniteTriggers || !hasTriggeredBarrel)
            {
                selectedBarrel.GetComponent<Explosive>().Explode();
                hasTriggeredBarrel = true;
            }
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

        if (hasInfiniteTriggers || !hasTriggeredBarrel)
        {
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

    private void OnInfiniteSelectsAllowed()
    {
        hasInfiniteTriggers = !hasInfiniteTriggers;
        Debug.Log("Infinite Triggers: " + hasInfiniteTriggers);
    }

    private void OnDestroy()
    {
        InputManager.OnLeftMouseDown -= OnLeftMouseDown;
        InputManager.OnStartExplode -= OnStartExplodeClicked;
        InputManager.OnInfiniteSelectsAllowed -= OnInfiniteSelectsAllowed;
    }
}