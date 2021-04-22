using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private CharacterManager characterManager;
    private CharacterDesignManager characterDesignManager;
    private CharacterNameManager characterNameManager;

    [SerializeField] private InputField characterNameInput;

    void Awake()
    {
        characterManager = GameObject.Find("Character").GetComponent<CharacterManager>();
        characterDesignManager = characterManager.GetComponent<CharacterDesignManager>();
        characterNameManager = characterManager.GetComponent<CharacterNameManager>();
    }

    public void SwitchRace(int race)
    {
        characterDesignManager.SwitchRace(race);
    }
    
    public void SwitchGender(int gender)
    {
        characterDesignManager.SwitchGender(gender);
    }

    public void SwitchHairStyle(bool increase)
    {
        characterDesignManager.SwitchHairStyles(increase);
    }

    public void SwitchBeardStyle(bool increase)
    {
        characterDesignManager.SwitchBeardStyles(increase);
    }

    public void SwitchEyeColor(bool increase)
    {
        characterDesignManager.SwitchEyeColor(increase);
    }

    public void SwitchHairColor(bool increase)
    {
        characterDesignManager.SwitchHairColor(increase);
    }

    public void SwitchSkinColor(bool increase)
    {
        characterDesignManager.SwitchSkinColor(increase);
    }

    public void RandomizeCharacter()
    {
        characterDesignManager.RandomizeCharacter();
    }

    public void FinishCharacter()
    {
        string currentName = characterNameInput.text;
        if (currentName.Length < 3)
        {
            Debug.Log("Name needs to be atleast 3 characters long...");
            return;
        }

        Player player = (Player) characterManager;
        player.Username = currentName;
        
        characterNameManager.SetNameUI(player.Username);
        Utility.AddSceneIfNotLoaded("Plain");
        Utility.UnloadScene("Character Customization");
    }
}
