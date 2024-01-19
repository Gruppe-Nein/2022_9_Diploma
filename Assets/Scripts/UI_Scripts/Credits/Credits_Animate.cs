using UnityEngine;
using UnityEngine.UI;

public class CreditsScroller : MonoBehaviour
{
    public RectTransform creditsContent; // Assign the parent RectTransform that holds all credit elements.
    public float scrollSpeed = 30f; // Speed at which the credits will scroll.
    public float delay = 5.0f; // Delay before the .png moves and the scrolling starts.

    private float timeElapsed = 0f;
    private bool startScrolling = false;

    void Update()
    {
        // Wait for delay before starting to scroll
        if (timeElapsed < delay)
        {
            timeElapsed += Time.deltaTime;
        }
        else
        {
            startScrolling = true;
            // Optional: Start fading out the .png image if needed
        }

        // Scroll credits upwards
        if (startScrolling)
        {
            creditsContent.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
        }
    }
}