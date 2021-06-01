using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInteractionManager : MonoBehaviour
{
    private CharacterManager characterManager;
    private Transform[] rayCastPositions = new Transform[3];

    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private InteractionManager interactionManager;

    private void Awake()
    {
        characterManager = GetComponent<CharacterManager>();
        for (int index = 0; index < 3; index++) rayCastPositions[index] = transform.GetChild(0).transform.GetChild(index);
    }
    

    private void Update()
    {
        interactionMenu.SetActive(false);
        if (HandleRayCast(rayCastPositions[0])) return;
        if (HandleRayCast(rayCastPositions[1])) return;
        if (HandleRayCast(rayCastPositions[2])) return;
    }

    public void OnCharacterInteraction()
    {
        interactionManager?.OnInteraction(characterManager);
    }

    public bool HandleRayCast(Transform raycastPosition)
    {
        float maxDistance = 1f;
        RaycastHit hit;
        bool succefullHit = Physics.BoxCast(raycastPosition.position, transform.localScale / 2, transform.forward, out hit, transform.rotation, maxDistance);
        if (succefullHit && hit.collider != null)
        {
            interactionManager = hit.collider.GetComponent<InteractionManager>();
            if (interactionManager != null)
            {
                interactionMenu.SetActive(true);
                return true;
            }
        } else if (!succefullHit || hit.collider == null) interactionManager = null;

        return false;
    }
}
