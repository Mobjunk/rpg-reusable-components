using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapManager : MonoBehaviour
{
    
    [SerializeField] private Camera miniMapCamera;
    [SerializeField] private Transform playerTranform;
    [SerializeField] private Transform compassTransform;
    [SerializeField] private bool canZoom;
    
    private void Awake()
    {
        miniMapCamera = GetComponent<Camera>();
        playerTranform = GameObject.Find("Character").transform;
    }

    private void Update()
    {
        if (canZoom)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                miniMapCamera.orthographicSize -= 0.5f;
                if (miniMapCamera.orthographicSize <= 1)
                    miniMapCamera.orthographicSize = 1;
            } else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                miniMapCamera.orthographicSize += 0.5f;
                if (miniMapCamera.orthographicSize >= 10)
                    miniMapCamera.orthographicSize = 10;
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 newPosition = playerTranform.position;
        newPosition.y = transform.position.y;
        //newPosition.x = newPosition.x + xOffset;

        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, playerTranform.eulerAngles.y, 0f);
        compassTransform.rotation = Quaternion.Euler(0, 0, playerTranform.eulerAngles.y);
    }


    public void MouseEntered()
    {
        canZoom = true;
    }

    public void MouseLeaves()
    {
        canZoom = false;
    }
}
