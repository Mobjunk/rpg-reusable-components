using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class ChatManager : Singleton<ChatManager>
{
    private bool requiresUpdate = false;
    private float updateTime;
    
    [SerializeField] private TMP_Text chatTextArea;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Scrollbar scrollbar;

    private void Update()
    {
        if (requiresUpdate)
        {
            updateTime -= Time.deltaTime;
            if (updateTime <= 0)
            {
                scrollRect.normalizedPosition = Vector2.zero;
                scrollbar.value = 0;
                requiresUpdate = false;
                updateTime = 0;
            }
        }
    }

    public void AddMessage(string message)
    {
        chatTextArea.text += message + "\n";
        requiresUpdate = true;
        updateTime = 0.025f;
    }
}
