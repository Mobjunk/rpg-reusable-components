using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DynamicOptionMenu;

[System.Serializable]
public abstract class UIContainerbase<T> : MonoBehaviour, IPointerClickHandler
{
    private DynamicOptionMenu dynamicMenu => DynamicOptionMenu.Instance();
    
    /// <summary>
    /// The containment
    /// </summary>
    [SerializeReference] protected T containment;

    public T Containment => containment;
    
    /// <summary>
    /// Image of the containment
    /// </summary>
    protected Image containmentVisual;
    
    public Image ContainmentVisual => containmentVisual;
    
    /// <summary>
    /// Text value of the containment
    /// </summary>
    protected Text containmentValue;
    
    public Text ContainmentValue => containmentValue;
    
    /// <summary>
    /// The slot id of the containment
    /// </summary>
    protected int containmentSlot;
    
    /// <summary>
    /// Containment options
    /// </summary>
    protected string[] containmentOptions;
    
    public string[] ContainmentOptions
    {
        get => containmentOptions;
        set => containmentOptions = value;
    }
    
    /// <summary>
    /// Name of the containment
    /// </summary>
    protected string containtmentName;

    public string ContaintmentName
    {
        get => containtmentName;
        set => containtmentName = value;
    }

    public virtual void SetContainment(T containment)
    {
        this.containment = containment;
    }

    public virtual void ClearContainment()
    {
        containment = default;
        containmentValue.text = "";
    }

    private void Awake()
    {
        var canvas = transform.GetChild(0).transform;
        var sprite = canvas.GetChild(0);
        var textComp = sprite.GetChild(0);
        containmentVisual = sprite.GetComponent<Image>();
        if(containmentVisual == null) Debug.Log("containmentVisual is null");
        containmentValue = textComp.GetComponent<Text>();
        if(containmentValue == null) Debug.Log("containmentValue is null");
        Debug.Assert(containmentValue != null);
    }

    private void Start()
    {
        containmentSlot = int.Parse(gameObject.name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left || eventData.button == PointerEventData.InputButton.Middle) HandleLeftClick();
        else if (eventData.button == PointerEventData.InputButton.Right) HandleRightClick();
    }

    private void HandleLeftClick()
    {
        Debug.Log("Left clicking...");
    }

    private void HandleRightClick()
    {
        Debug.Log("Right clicking...");
        if (containmentVisual.sprite == null) return;
        
        //Checks if you clicked the slot you have opened
        if (dynamicMenu.slotOpened == containmentSlot)
        {
            dynamicMenu.Close();
            return;
        }

        //If there is already a menu opened first close the old one
        if (dynamicMenu.slotOpened != -1) dynamicMenu.Close();

        //Opens the dynamic option menu
        dynamicMenu.Open(ContainmentOptions, gameObject, MenuType.ITEM, ContaintmentName);

        //Sets what slot you have opened
        dynamicMenu.slotOpened = containmentSlot;
    }
}
