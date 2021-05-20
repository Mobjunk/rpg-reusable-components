using UnityEngine;
using UnityEngine.UI;

public class ButtonHover : MonoBehaviour
{
    /// <summary>
    /// Handles setting the color of a hover (when hovering an option in the right click menu)
    /// </summary>
    public void SetHover()
    {
        //Grabs the image component of the current object
        Image image = GetComponent<Image>();
        //Sets the hover color
        image.color = new Color(1f, 1f, 1f, 0.25f);
    }

    /// <summary>
    /// Resets the color of a hover (when leaving the hover of an option in the right click menu)
    /// </summary>
    public void ResetHover()
    {
        //Grabs the image component of the current object
        Image image = GetComponent<Image>();
        //Resets to the default color
        image.color = new Color(1f, 1f, 1f, 0f);
    }
}