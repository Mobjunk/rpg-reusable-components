using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RedstoneManager : MonoBehaviour
{
    private int currentRedstoneActive = 3;
    
    [SerializeField] private Sprite offSprite;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Image[] images;

    private void Awake()
    {
        for (int index = 0; index < images.Length; index++) images[index].sprite = index == currentRedstoneActive ? onSprite : offSprite;
    }

    public void SwitchRedstone(int newIndex)
    {
        if (currentRedstoneActive == newIndex)
        {
            images[newIndex].sprite = offSprite;
            TabBackManager.Instance().Toggle();
            return;
        }
        
        if(!TabBackManager.Instance().currentlyOpen) TabBackManager.Instance().Toggle();
        images[newIndex].sprite = onSprite;
        if(currentRedstoneActive != -1) images[currentRedstoneActive].sprite = offSprite;
        currentRedstoneActive = newIndex;
    }
}
