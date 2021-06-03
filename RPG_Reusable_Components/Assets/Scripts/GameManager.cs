using System.Collections;
using System.Collections.Generic;
using RPGCharacters;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Main camera attachted to the player")]
    public Camera camera;

    [Header("Background image")] public GameObject backgroundImage;
    
    [Header("Possible Colors")]
    [SerializeField] public List<Color> skinColors = new List<Color>();
    [SerializeField] public List<Color> hairColors = new List<Color>();
    [SerializeField] public List<Color> eyeColors = new List<Color>();

    [Header("Slot prefab")] public GameObject slotPrefab;
    
    void Start()
    {
        Application.targetFrameRate = 60;
        Utility.AddSceneIfNotLoaded("Character Customization");
    }

    public void DisableBackgroundImage(bool active = false)
    {
        backgroundImage.SetActive(active);
    }
}
