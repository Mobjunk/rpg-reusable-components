using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RPGCharacters;
using UnityEngine;

public class CharacterDesignManager : MonoBehaviour
{
    private GameManager gameManager => GameManager.Instance();
    private ItemManager itemManager => ItemManager.Instance();
    private CharacterArmatureManager characterArmatureManager;
    private CharacterEquipmentManager characterEquipmentManager;
    
    /// <summary>
    /// Hair style variables
    /// </summary>
    private int currentHairStyles = 1;
    private int maxHairStyles = 13;

    /// <summary>
    /// Beard style variables
    /// </summary>
    private int currentBeardStyle = 5;
    private int maxBeardStyle = 6;

    /// <summary>
    /// Eye color variable
    /// </summary>
    private int currentEyeColor = 0;

    /// <summary>
    /// Hair color variable
    /// </summary>
    private int currentHairColor = 1;
    
    /// <summary>
    /// Skin color variable
    /// </summary>
    private int currentSkinColor = 5;
    
    [Header("Race")]
    [SerializeField] private int characterRace;

    public int CharacterRace
    {
        get => characterRace;
        set => characterRace = value;
    }
    
    [Header("Gender")]
    [SerializeField] private int characterGender;

    public int CharacterGender
    {
        get => characterGender;
        set => characterGender = value;
    }
    
    [Header("Styles")]
    [SerializeField] private int characterHairStyle;

    public int CharacterHairStyle
    {
        get => characterHairStyle;
        set => characterHairStyle = value;
    }
    
    [SerializeField] private int characterBeardStyle;

    public int CharacterBeardStyle
    {
        get => characterBeardStyle;
        set => characterBeardStyle = value;
    }
    
    [SerializeField] private int characterEyebrowStyle;

    public int CharacterEyebrowStyle
    {
        get => characterEyebrowStyle;
        set => characterEyebrowStyle = value;
    }

    [Header("Colors")]
    [SerializeField] private Color characterSkinColor;

    public Color CharacterSkinColor
    {
        get => characterSkinColor;
        set => characterSkinColor = value;
    }

    [SerializeField] private Color characterEyeColor;

    public Color CharacterEyeColor
    {
        get => characterEyeColor;
        set => characterEyeColor = value;
    }

    [SerializeField] private Color characterHairColor;

    public Color CharacterHairColor
    {
        get => characterHairColor;
        set => characterHairColor = value;
    }

    [SerializeField] private Color characterMouthColor;

    public Color CharacterMouthColor
    {
        get => characterMouthColor;
        set => characterMouthColor = value;
    }

    private void Awake()
    {
        CharacterManager characterManager = GetComponent<CharacterManager>();
        if (characterManager != null && characterManager.GetType() == typeof(Npc))
        {
            NpcData npcData = ((Npc) characterManager).GetNpcData();
            Debug.Log("Handling setting up: " + npcData.name);
            
            //Handles randomizing a npc when it needs to be randomized
            if (npcData.randomizeValues)
                RandomizeCharacter();
            else //Handles setting the needed values
                Setup(npcData.characterRace, npcData.characterGender, npcData.characterHairStyle,
                    npcData.characterBeardStyle, npcData.characterEyebrowStyle, npcData.characterSkinColor,
                    npcData.characterEyeColor, npcData.characterHairColor, npcData.characterMouthColor);
        }
    }
    
    private void Start()
    {
        characterArmatureManager = GetComponent<CharacterArmatureManager>();
        characterEquipmentManager = GetComponent<CharacterEquipmentManager>();
    }

    /// <summary>
    /// Handles switching the character his gender
    /// </summary>
    /// <param name="gender"></param>
    public void SwitchGender(int gender)
    {
        CharacterGender = gender;
        characterEquipmentManager.UnEquipAll();
        characterArmatureManager.SetupBody();
    }

    /// <summary>
    /// Handles switching the character his race
    /// </summary>
    /// <param name="race"></param>
    public void SwitchRace(int race)
    {
        if (race == 2) //Goblin
        {
            CharacterSkinColor = gameManager.skinColors[7];
            CharacterHairStyle = 6; //Bald
            CharacterBeardStyle = 6; //Bald
        }
        else
        {
            CharacterSkinColor = gameManager.skinColors[2];
            CharacterHairStyle = 1;
            CharacterEyebrowStyle = 0;
        }
        CharacterRace = race;
        CharacterBeardStyle = 0;
        characterEquipmentManager.UnEquipAll();
        characterArmatureManager.UpdateRace();
        characterArmatureManager.SetupSlots();
        characterArmatureManager.SetupBody();
    }

    /// <summary>
    /// Handle switching the character his hair style
    /// </summary>
    /// <param name="increase"></param>
    public void SwitchHairStyles(bool increase)
    {
        Utility.HandleChange(increase, ref currentHairStyles, maxHairStyles);
        CharacterHairStyle = currentHairStyles;
        characterArmatureManager.SetupBody();
    }
    
    /// <summary>
    /// Handles switching the character his beard style
    /// </summary>
    /// <param name="increase"></param>
    public void SwitchBeardStyles(bool increase)
    {
        Utility.HandleChange(increase, ref currentBeardStyle, maxBeardStyle);
        CharacterBeardStyle = currentBeardStyle;
        characterArmatureManager.SetupBody();
    }
    
    

    /// <summary>
    /// HHandles switching the character his eye color
    /// </summary>
    /// <param name="increase"></param>
    public void SwitchEyeColor(bool increase)
    {
        Utility.HandleChange(increase, ref currentEyeColor, gameManager.eyeColors.Count);
        CharacterEyeColor = gameManager.eyeColors[currentEyeColor];
        characterArmatureManager.SetupBody();
    }

    /// <summary>
    /// Handles switching the character his hair color
    /// </summary>
    /// <param name="increase"></param>
    public void SwitchHairColor(bool increase)
    {
        Utility.HandleChange(increase, ref currentHairColor, gameManager.hairColors.Count);
        CharacterHairColor = gameManager.hairColors[currentHairColor];
        characterArmatureManager.SetupBody();
    }

    /// <summary>
    /// Handles switching the character his skin color
    /// </summary>
    /// <param name="increase"></param>
    public void SwitchSkinColor(bool increase)
    {
        Utility.HandleChange(increase, ref currentSkinColor, gameManager.skinColors.Count);
        CharacterSkinColor = gameManager.skinColors[currentSkinColor];
        characterArmatureManager.SetupBody();
        foreach (var slot in characterEquipmentManager.GetEquipmentSlots().Where(slot => slot.inUse))
            itemManager.SetItemColor(this, slot.activeObject);
    }
    
    /// <summary>
    /// Handles setting up a character with the correct values
    /// </summary>
    /// <param name="race">The race of the character</param>
    /// <param name="gender">The gender of the character</param>
    /// <param name="hairStyle">The hair style of the character</param>
    /// <param name="beardStyle">The beard style of the character</param>
    /// <param name="eyebrowStyle">The eyebrow style of the character</param>
    /// <param name="skinColor">The skin color of the character</param>
    /// <param name="eyeColor">The eye color of the character</param>
    /// <param name="hairColor">The hair color of the character</param>
    /// <param name="mouthColor">The mouth color of the character</param>
    void Setup(int race, int gender, int hairStyle, int beardStyle, int eyebrowStyle, Color skinColor, Color eyeColor, Color hairColor, Color mouthColor)
    {
        CharacterRace = race;
        
        CharacterGender = gender;
        CharacterHairStyle = hairStyle;
        CharacterBeardStyle = beardStyle;
        CharacterEyebrowStyle = eyebrowStyle;

        CharacterSkinColor = skinColor;
        CharacterEyeColor = eyeColor;
        CharacterHairColor = hairColor;
        CharacterMouthColor = mouthColor;
    }

    /// <summary>
    /// Randomizes a character
    /// </summary>
    public void RandomizeCharacter()
    {
        Setup(UnityEngine.Random.Range(0, 3), UnityEngine.Random.Range(0, 1), UnityEngine.Random.Range(0, maxHairStyles), UnityEngine.Random.Range(0, maxBeardStyle), UnityEngine.Random.Range(0, 1), gameManager.skinColors[UnityEngine.Random.Range(0, gameManager.skinColors.Count - 1)], gameManager.eyeColors[UnityEngine.Random.Range(0, gameManager.eyeColors.Count - 1)], gameManager.hairColors[UnityEngine.Random.Range(0, gameManager.hairColors.Count - 1)], Color.black);
        if (characterEquipmentManager != null)
        {
            characterEquipmentManager.UnEquipAll();
            characterArmatureManager.UpdateRace();
            characterArmatureManager.SetupSlots();
            characterArmatureManager.SetupBody();
        }
    }
}
