using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Difficulty_Button_Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI descriptionText;
    public Animator textMeshProAnimator;

    public void OnPointerEnter(PointerEventData eventData)
    {
        string buttonName = transform.name; // Get the name of the button
        string description = GetDescriptionForButton(buttonName);
        descriptionText.text = description;

        textMeshProAnimator.SetBool("IsHovered", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textMeshProAnimator.SetBool("IsHovered", false);
    }

    private string GetDescriptionForButton(string buttonName)
    {
        // Define descriptions for each button based on their names
        switch (buttonName)
        {
            case "Easy":
                return "Default difficulty level. Once per checkpoint, when you get hit by a trap, you become invulnerable for a short period of time.";

            case "Hard":
                return "A real challenge. When you get hit by a trap you instantly get sent back to the checkpoint.";
            // Add more cases for additional buttons
            default:
                return "Default description.";
        }
    }
}