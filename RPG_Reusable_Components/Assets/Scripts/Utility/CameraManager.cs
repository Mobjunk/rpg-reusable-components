using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float yRotation;
    [SerializeField] private float xRotation;
    [Header("Camera")]
    [SerializeField] private Camera cam;
    [Header("Camera Rotation")]
    [SerializeField] private bool autoRotation;
    [SerializeField] private TMP_Text rotationText;
    [Header("Zoom")]
    [SerializeField] private bool zoom;
    [SerializeField] private Image zoomSprite;
    [SerializeField] private Sprite[] zoomSprites;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yRotation, xRotation), 10 * Time.deltaTime);
        if (autoRotation) yRotation += 100 * Time.deltaTime;

        float fov = Mathf.Lerp(cam.fieldOfView, zoom ? 10 : 21, 5 * Time.deltaTime);
        cam.fieldOfView = fov;
        cam.transform.localRotation = Quaternion.Slerp(cam.transform.localRotation, Quaternion.Euler(22, 90, 0), 5 * Time.deltaTime);
    }

    public void ToggleRotation()
    {
        autoRotation = !autoRotation;
        rotationText.text = autoRotation ? "On" : "Off";
    }

    public void ToggleZoom()
    {
        zoom = !zoom;
        zoomSprite.sprite = zoomSprites[zoom ? 1 : 0];
    }

    public void RotateLeft()
    {
        yRotation -= 45;
    }

    public void RotateRight()
    {
        yRotation += 45;
    }
}
