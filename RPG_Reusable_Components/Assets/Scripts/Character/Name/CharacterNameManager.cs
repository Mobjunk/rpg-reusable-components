using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterNameManager : MonoBehaviour
{
    [SerializeField] private GameObject nameCanvas;
    [SerializeField] private TMP_Text nameText;

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
