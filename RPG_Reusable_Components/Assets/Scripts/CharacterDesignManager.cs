using System;
using System.Collections;
using System.Collections.Generic;
using RPGCharacters;
using UnityEngine;

public class CharacterDesignManager : MonoBehaviour
{
    private GameManager gameManager => GameManager.Instance();
    
    private CharacterBase characterBase;
    
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
    
    [Header("Colors")]
    [SerializeField] private List<Color> skinColors = new List<Color>();
    [SerializeField] private List<Color> hairColors = new List<Color>();
    [SerializeField] private List<Color> eyeColors = new List<Color>();

    private void Start()
    {
        characterBase = gameManager.getPlayer();
    }

    public void SwitchGender(int gender)
    {
        characterBase.ChangeGender(gender);
    }

    public void SwitchRace(int race)
    {
        characterBase.ChangeRace(race);
        characterBase.ChangeHairstyle(1);
        if (race == 2) { //Goblin
            characterBase.ChangeSkinColor(skinColors[7]);
            characterBase.ChangeHairstyle(6); //Bald
            characterBase.ChangeBeardstyle(6); //Bald
        } else characterBase.ChangeSkinColor(skinColors[2]);
    }

    public void SwitchHairStyles(bool increase)
    {
        currentHairStyles += increase ? 1 : -1;
        if (currentHairStyles < 0) currentHairStyles = maxHairStyles;
        if (currentHairStyles > maxHairStyles) currentHairStyles = 0;
        characterBase.ChangeHairstyle(currentHairStyles);
    }
    
    public void SwitchBeardStyles(bool increase)
    {
        currentBeardStyle += increase ? 1 : -1;
        if (currentBeardStyle < 0) currentBeardStyle = maxBeardStyle;
        if (currentBeardStyle > maxBeardStyle) currentBeardStyle = 0;
        characterBase.ChangeBeardstyle(currentBeardStyle);
    }

    public void SwitchEyeColor(bool increase)
    {
        currentEyeColor += increase ? 1 : -1;
        if (currentEyeColor < 0) currentEyeColor = eyeColors.Count;
        if (currentEyeColor >= eyeColors.Count) currentEyeColor = 0;
        characterBase.ChangeEyeColor(eyeColors[currentEyeColor]);
    }

    public void SwitchHairColor(bool increase)
    {
        currentHairColor += increase ? 1 : -1;
        if (currentHairColor < 0) currentHairColor = hairColors.Count;
        if (currentHairColor >= hairColors.Count) currentHairColor = 0;
        characterBase.ChangeHairColor(hairColors[currentHairColor]);
    }

    public void SwitchSkinColor(bool increase)
    {
        currentSkinColor += increase ? 1 : -1;
        if (currentSkinColor < 0) currentSkinColor = skinColors.Count;
        if (currentSkinColor >= skinColors.Count) currentSkinColor = 0;
        characterBase.ChangeSkinColor(skinColors[currentSkinColor]);
    }
}
