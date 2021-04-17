using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClampName : MonoBehaviour
{
    private GameManager gameManger => GameManager.Instance();
    
    [SerializeField] private TMP_Text nameText;

    private void Update()
    {
        if (gameManger == null || gameManger.camera == null) return;
        
        Vector3 v = gameManger.camera.transform.position - transform.position;
        
        v.x = v.z = 0.0f;
        nameText.transform.LookAt( gameManger.camera.transform.position - v ); 
        nameText.transform.Rotate(0,180,0);
    }
}
