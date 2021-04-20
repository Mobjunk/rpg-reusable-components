using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    private CharacterManager characterManager;
    private Transform rayCastPosition;

    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private ObjectInteractionManager interactionManager;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        rayCastPosition = transform.GetChild(0);
    }
    

    private void Update()
    {
        interactionMenu.SetActive(false);
        float maxDistance = 1f;
        RaycastHit hit;
        bool isHit = Physics.BoxCast(rayCastPosition.position, transform.localScale / 2, transform.forward, out hit, transform.rotation, maxDistance);
        if (isHit)
        {
            interactionManager = hit.collider.GetComponent<ObjectInteractionManager>();
            if (interactionManager != null) interactionMenu.SetActive(true);
        }
    }

    public void OnCharacterInteraction()
    {
        interactionManager?.OnInteraction(characterManager);
    }
}
