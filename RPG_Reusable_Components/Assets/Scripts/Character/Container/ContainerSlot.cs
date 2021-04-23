using UnityEngine;
using UnityEngine.UI;

public class ContainerSlot : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }

    public void SetSpriteVisible(bool visible)
    {
        image.enabled = visible;
    }

    [SerializeField] private Text text;

    public void SetText(string tekst)
    {
        text.text = tekst;
    }

    public void SetTextVisible(bool visible)
    {
        text.enabled = visible;
    }

    private void Awake()
    {
        Transform canvas = transform.GetChild(0).transform;
        Transform sprite = canvas.GetChild(0);
        Transform textComp = sprite.GetChild(0);
        image = sprite.GetComponent<Image>();
        text = textComp.GetComponent<Text>();

    }

}
