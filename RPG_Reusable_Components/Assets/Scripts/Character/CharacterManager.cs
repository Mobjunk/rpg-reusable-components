using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(CharacterArmatureManager), typeof(CharacterDesignManager))]
[RequireComponent(typeof(CharacterAttackManager), typeof(CharacterStateManager), typeof(CharacterEquipmentManager))]
public class CharacterManager : MonoBehaviour
{
    [SerializeField] protected CharacterInputManager characterInputManager;
    [SerializeField] private CharacterAttackManager characterAttackManager;
    [SerializeField] private GameObject nameCanvas;
    [SerializeField] private TMP_Text nameText;
    
    public virtual void Awake()
    {
        //Debug.Log("Character wake base");
    }
    
    public virtual void Start()
    {
        //Debug.Log("Character start base");
        characterAttackManager = GetComponent<CharacterAttackManager>();
        
        if(GetType() == typeof(Player))
            characterInputManager.OnCharacterAttack = characterAttackManager.Attack;
    }

    /// <summary>
    /// Handles activating the UI canvas for the player name
    /// And also sets the text to the correct name
    /// </summary>
    /// <param name="name"></param>
    public void SetNameUI(string name)
    {
        nameCanvas.SetActive(true);
        nameText.text = $"{name}";
    }
}
