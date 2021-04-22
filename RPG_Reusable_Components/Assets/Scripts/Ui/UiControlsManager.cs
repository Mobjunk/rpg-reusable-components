using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiControlsManager : MonoBehaviour
{
    private CharacterControlsManager characterControlsManager;

    [SerializeField] private TMP_Text interactionText;
    
    private void Awake()
    {
        characterControlsManager = FindObjectOfType<CharacterControlsManager>();
        characterControlsManager.OnKeyCodeChange += OnKeyCodeChange;
    }

    private void OnKeyCodeChange()
    {
        interactionText.text = $"Press {characterControlsManager.interaction.ToString()} to interact";
    }
}
