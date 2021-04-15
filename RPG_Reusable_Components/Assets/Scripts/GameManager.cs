using System.Collections;
using System.Collections.Generic;
using RPGCharacters;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Player Information")]
    [SerializeField] private CharacterBase player;

    public CharacterBase getPlayer()
    {
        return player;
    }

    void Start()
    {
        Utility.AddSceneIfNotLoaded("Character Customization");
    }
}
