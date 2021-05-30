using TMPro;
using UnityEngine;

public class InteractionMenuManager : Singleton<InteractionMenuManager>
{
    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private TMP_Text interactionText;

    public void SetInteraction(bool active, string message = "Press F to interact")
    {
        interactionMenu.SetActive(active);
        interactionText.text = message;
    }

    public bool isActive()
    {
        return interactionMenu.active;
    }

    //TODO: Do something with the text bla bla
}
