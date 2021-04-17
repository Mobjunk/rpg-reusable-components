using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterArmatureManager : MonoBehaviour
{
    private ItemManager itemManager => ItemManager.Instance();
    
    private CharacterDesignManager characterDesignManager;
    private CharacterEquipmentManager characterEquipmentManager;
    
    [SerializeField] private GameObject armature;
    
    [SerializeField] private List<Armature> armatures = new List<Armature>();

    [SerializeField] private Transform equippedItemsParent; // to hold all loaded items

    public Transform EquippedItemsParent
    {
        get => equippedItemsParent;
        set => equippedItemsParent = value;
    }
    
    [SerializeField] private Transform bodyPartsParent; // to hold all body parts, hair etc.

    public Transform BodyPartsParent
    {
        get => bodyPartsParent;
        set => bodyPartsParent = value;
    }

    [SerializeField] private List<GameObject> bodyParts = new List<GameObject>();

    public List<GameObject> GetBodyParts()
    {
        return bodyParts;
    }
    
    [Header("Animator")]
    public Animator animator;
    [Header("AnimatorController you want to use for this character")]
    [SerializeField] private RuntimeAnimatorController animatorController;
    
    void Awake()
    {
        if (equippedItemsParent == null) {
            equippedItemsParent = new GameObject("SkinnedItems").transform;
            equippedItemsParent.SetParent(transform);
        }
        if (bodyPartsParent == null) {
            bodyPartsParent = new GameObject("BodyParts").transform;
            bodyPartsParent.SetParent(transform);
        }
        
        characterDesignManager = GetComponent<CharacterDesignManager>();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
        
        SetupArmature();
        UpdateRace();
        SetupSlots();
        SetupBody();
    }
    
    void SetupArmature() {
        if (armatures.Count != 0) return;
        
        int counter = 0;
        foreach (GameObject prefab in Resources.LoadAll<GameObject>("CustomizableCharacters/Armatures/")) {
            GameObject instantiate = Instantiate(prefab, transform);
            armatures.Add(new Armature() { race = counter++, armature = instantiate, referenceMesh = instantiate.GetComponentInChildren<SkinnedMeshRenderer>() });
            instantiate.SetActive(false);
        }

    }

    public void UpdateRace() {
        foreach (Armature a in armatures) {
            a.armature.SetActive(false);
        }
        armature = armatures.SingleOrDefault(s => s.race == characterDesignManager.CharacterRace)?.armature;
        if (armature != null) armature.SetActive(true);
            
        animator = armature.GetComponent<Animator>();

        if(animatorController != null)
            animator.runtimeAnimatorController = animatorController;
    }

    public void SetupSlots() {
        characterEquipmentManager.GetEquipmentSlots().Clear();
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.body });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.feet });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.hair, container = armature.transform.FindDeepChild("EQ_Head") });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.head, container = armature.transform.FindDeepChild("EQ_Head") });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.legs });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.mainhand, container = armature.transform.FindDeepChild("EQ_MainHand") });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.offhand, container = armature.transform.FindDeepChild("EQ_OffHand") });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.hands });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.beard });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.eyebrow });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.neck });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.accessory });
        characterEquipmentManager.GetEquipmentSlots().Add(new EquipmentSlot() { slot = EquipmentSlots.back, container = armature.transform.FindDeepChild("EQ_Back") });
    }

    public void SetupBody()
    {
        ClearBody();

        if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.head).inUse) {
            if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.head).item.showHead) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, 4, ItemType.BodyParts); // head 
            
            if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.head).item.showHair) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, characterDesignManager.CharacterHairStyle, ItemType.Hair);
            else characterEquipmentManager.UnloadSlot(EquipmentSlots.hair);
            
            if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.head).item.showEyebrow) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, characterDesignManager.CharacterEyebrowStyle, ItemType.Eyebrow);
            else characterEquipmentManager.UnloadSlot(EquipmentSlots.eyebrow);
            
            if (characterDesignManager.CharacterGender == 0) {
                if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.head).item.showBeard) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, characterDesignManager.CharacterBeardStyle, ItemType.Beard);
                else characterEquipmentManager.UnloadSlot(EquipmentSlots.beard);
            } else characterEquipmentManager.UnloadSlot(EquipmentSlots.beard);
        } else {
            itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, 4, ItemType.BodyParts); // head 
            itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, characterDesignManager.CharacterHairStyle, ItemType.Hair);
            itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, characterDesignManager.CharacterEyebrowStyle, ItemType.Eyebrow);
            if (characterDesignManager.CharacterGender == 0) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, characterDesignManager.CharacterBeardStyle, ItemType.Beard);
            else characterEquipmentManager.UnloadSlot(EquipmentSlots.beard);
        }
        
        if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.body).inUse == false) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, 3, ItemType.BodyParts); // torso 
        if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.hands).inUse == false) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, 5, ItemType.BodyParts); // hands
        if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.legs).inUse == false) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, 1, ItemType.BodyParts); // legs
        if (characterEquipmentManager.GetEquipmentSlot(EquipmentSlots.feet).inUse == false) itemManager.LoadItem(this, characterDesignManager, characterEquipmentManager, 2, ItemType.BodyParts); // feet
    }
    
    void ClearBody() {
        foreach (GameObject g in bodyParts)
            Destroy(g);
    }
    
    public SkinnedMeshRenderer GetReferenceMesh() {
        return armatures.SingleOrDefault(s => s.race == characterDesignManager.CharacterRace)?.referenceMesh;
    }
}

[System.Serializable]
public class Armature {
    public int race;
    public SkinnedMeshRenderer referenceMesh;
    public GameObject armature;
}
