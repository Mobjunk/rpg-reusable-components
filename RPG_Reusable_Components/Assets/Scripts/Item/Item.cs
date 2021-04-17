using UnityEngine;

public class Item : MonoBehaviour {
    public GameObject male;
    public GameObject female;
    public bool skinned; // static or rigged item?


    public bool showHead; // show head model, hair, beard, eyebrows under helmet?
    public bool showHair;
    public bool showBeard;
    public bool showEyebrow;

    public EquipmentSlots equipmentSlot;

}