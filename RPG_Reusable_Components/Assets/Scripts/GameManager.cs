using System.Collections;
using System.Collections.Generic;
using RPGCharacters;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Main camera attachted to the player")]
    public Camera camera;
    
    [Header("Possible Colors")]
    [SerializeField] public List<Color> skinColors = new List<Color>();
    [SerializeField] public List<Color> hairColors = new List<Color>();
    [SerializeField] public List<Color> eyeColors = new List<Color>();
    
    void Start()
    {
        Application.targetFrameRate = 60;
        Utility.AddSceneIfNotLoaded("Character Customization"); //Character Customization
    }
    
}
