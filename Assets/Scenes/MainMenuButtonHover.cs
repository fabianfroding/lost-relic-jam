using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonHover : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void PointerEnter()
    {
        image.color = new Color(0, 0, 0, 0.5f);
    }

    public void PointerExit()
    {
        image.color = new Color(1, 1, 1, 0);
    }
}
