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
    [SerializeField] private TMP_Text chatTextArea;
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Scrollbar scrollbar;

    private void Update()
    {
        scrollRect.normalizedPosition = Vector2.zero;
        scrollbar.value = 0;
    }

    public void AddMessage(string message)
    {
        chatTextArea.text += message + "\n";
    }
}
