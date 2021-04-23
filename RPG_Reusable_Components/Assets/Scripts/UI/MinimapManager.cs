using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    [SerializeField] private Transform playerTranform;
    [SerializeField] private Transform compassTransform;
    [SerializeField] private float xOffset = -0.56f;
    
    private void Awake()
    {
        playerTranform = GameObject.Find("Character").transform;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = playerTranform.position;
        newPosition.y = transform.position.y;
        newPosition.x = newPosition.x + xOffset;

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, playerTranform.eulerAngles.y, 0f);
        compassTransform.rotation = Quaternion.Euler(0, 0, playerTranform.eulerAngles.y);
    }
}
