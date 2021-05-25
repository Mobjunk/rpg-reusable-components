using UnityEngine;

public abstract class AbstractItemContainer : UIContainerbase<ItemData>
{
    
    public override void SetContainment(ItemData containment)
    {
        base.SetContainment(containment);
        UpdateItemContainer();
    }

    protected virtual void UpdateItemContainer()
    {
        if (ContainmentVisual == null) return;
        
        if (Containment == null || Containment.itemData == null)
        {
            ContainmentVisual.enabled = false;
            ContainmentValue.enabled = false;
            return;
        }

        ContainmentVisual.sprite = Containment.itemData.uiSprite;
        ContainmentVisual.enabled = true;
        
        ContainmentValue.text = $"{(Containment.amount > 1 ? Containment.amount.ToString() : "")}";
        if (!ContainmentValue.text.Equals("")) ContainmentValue.enabled = true;
        
        ContaintmentName = Containment.itemData.name;
        ContainmentOptions = Containment.itemData.options.ToArray();
    }
}
