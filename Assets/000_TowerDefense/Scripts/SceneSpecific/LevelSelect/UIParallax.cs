using UnityEngine;

public class UIParallax : MonoBehaviour
{

    [Header("Parallax Properties")]
    public float parallaxFactor = 0.05f;  // Control the intensity of the effect
    public float maxXOffset;  // X distance from center this object can travel
    public float maxYOffset;  // Y distance from center this object can travel

    private RectTransform background;
    private Vector2 originalPosition;

    private void Awake()
    {
        background = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Save the original position of the background
        originalPosition = background.anchoredPosition;
    }

    void Update()
    {
        // Get the mouse position relative to the screen dimensions
        Vector2 mousePosition = Input.mousePosition;
        float width = Screen.width;
        float height = Screen.height;

        // Normalize mouse position between -0.5 and 0.5
        Vector2 normalizedMousePosition = new Vector2(
            (mousePosition.x / width) - 0.5f,
            (mousePosition.y / height) - 0.5f
        );

        // Apply parallax effect based on mouse position
        Vector2 parallaxOffset = new Vector2(
            normalizedMousePosition.x * parallaxFactor * width,
            normalizedMousePosition.y * parallaxFactor * height
        );

        parallaxOffset.x = Mathf.Clamp(parallaxOffset.x, -maxXOffset, maxXOffset);
        parallaxOffset.y = Mathf.Clamp(parallaxOffset.y, -maxYOffset, maxYOffset);

        // Update the background position
        background.anchoredPosition = originalPosition + parallaxOffset;
    }
}