using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedstoneManager : MonoBehaviour
{
    private int currentRedstoneActive = 0;
    
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Image[] images;

    private void Awake()
    {
        for (int index = 0; index < images.Length; index++) images[index].sprite = index == currentRedstoneActive ? onSprite : offSprite;
    }

    public void SwitchRedstone(int newIndex)
    {
        images[newIndex].sprite = onSprite;
        images[currentRedstoneActive].sprite = offSprite;
        currentRedstoneActive = newIndex;
    }
}
