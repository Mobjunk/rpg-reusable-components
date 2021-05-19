using UnityEngine;

public class GroundItemData
{
    public float defaultTime = 2;
    
    public ItemData item;
    public GameObject gObject;
    public bool respawns;
    public float currentTime;
    public State state;

    public GroundItemData(ItemData item, GameObject gObject, bool respawns = false)
    {
        this.item = item;
        this.gObject = gObject;
        this.respawns = respawns;
        if (!respawns) currentTime = defaultTime;
        this.state = State.PRIVATE;
    }

    public GroundItemData(ItemData item, GameObject gObject, State state, bool respawns = false)
    {
        this.item = item;
        this.gObject = gObject;
        this.respawns = respawns;
        if (!respawns) currentTime = defaultTime;
        this.state = state;
    }
    
}

public enum State
{
    HIDDEN,
    PRIVATE,
    PUBLIC
}
