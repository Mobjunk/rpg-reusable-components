using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    private Transform rayCastPosition;

    [SerializeField] private GameObject interactionMenu;

    private void Awake()
    {
        rayCastPosition = transform.GetChild(0);
    }
    

    private void Update()
    {
        interactionMenu?.SetActive(false);
        float maxDistance = 1f;
        RaycastHit hit;
        bool isHit = Physics.BoxCast(rayCastPosition.position, transform.localScale / 2, transform.forward, out hit, transform.rotation, maxDistance);
        if (isHit)
        {
            ObjectInteractionManager interactionManager = hit.collider.GetComponent<ObjectInteractionManager>();
            if (interactionManager != null)
            {
                Debug.Log($"Hit a interactable object: {hit.collider.gameObject.name}");
                interactionMenu.SetActive(true);
            }
        }
    }
}
