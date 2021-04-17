using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
[System.Serializable]
public class NpcData : ScriptableObject
{
    [Header("Randomize")]
    public bool randomizeValues;
    
    [Header("Race - ONLY FILL THESE IN WHEN RANDOM VALUES IS NOT SET")]
    public int characterRace;
    [Header("Gender - ONLY FILL THESE IN WHEN RANDOM VALUES IS NOT SET")]
    public int characterGender;
    [Header("Styles - ONLY FILL THESE IN WHEN RANDOM VALUES IS NOT SET")]
    public int characterHairStyle;
    public int characterBeardStyle;
    public int characterEyebrowStyle;
    
    [Header("Colors - ONLY FILL THESE IN WHEN RANDOM VALUES IS NOT SET")]
    public Color characterSkinColor;
    public Color characterEyeColor;
    public Color characterHairColor;
    public Color characterMouthColor;
    
    [Header("")]
    public int spacer2 = 0;

    [Header("Items - ONLY FILL THESE IF YOU WANT THE NPC TO WEAR ITEMS")]
    public List<int> items = new List<int>();
    

    public void Randomize(int maxHairStyles, int maxBeardStyle, List<Color> skin, List<Color> hair, List<Color> eyeColors)
    {
        characterRace = Random.Range(0, 3);
        characterGender = Random.Range(0, 1);
                
        characterHairStyle = Random.Range(0, maxHairStyles);
        characterBeardStyle = Random.Range(0, maxBeardStyle);
        characterEyebrowStyle = Random.Range(0, 1);

        characterSkinColor = skin[Random.Range(0, skin.Count - 1)];
        characterEyeColor = hair[Random.Range(0, hair.Count - 1)];
        characterHairColor = eyeColors[Random.Range(0, eyeColors.Count - 1)];
    }
}
