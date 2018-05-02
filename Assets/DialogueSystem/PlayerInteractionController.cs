using DoodleStudio95;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [Header("UI Display")]
    public Vector2 interactionUIDisplayOffset;
    public DoodleStudio95.DoodleAnimator interactionUIDisplay;

    [Header("Smell Display")]
    public Vector2 smellRectUIDisplayOffset;

    BoxCollider bc;
    Transform myTransform;

    LayerMask interactionLayerMask;

    int numberOfInteractions;
    Collider[] collidingInteractionTriggers = new Collider[1];

    Transform playerTransform;

    private void Awake()
    {
        myTransform = transform;
        bc = GetComponent<BoxCollider>();
        interactionLayerMask = 1 << LayerMask.NameToLayer("Interactions");
    }

    private void Start()
    {
        playerTransform = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        numberOfInteractions = CheckForInteractions();
        ChangeVisibilityInteractionUIDisplay();
    }

    public bool HasInteractionsInRange()
    {
        return numberOfInteractions > 0;
    }

    public Interactive GetInteractive()
    {
        Collider interactionCollider = collidingInteractionTriggers[0];
        Interactive interaction = interactionCollider.GetComponent<Interactive>();
        return interaction;
    }

    private int CheckForInteractions()
    {
        if (InteractionManager.Instance.PlayingInteraction)
        {
            return 0;
        }

        interactionUIDisplay.transform.position = new Vector3(
            myTransform.position.x - playerTransform.localScale.x * interactionUIDisplayOffset.x,
            myTransform.position.y + interactionUIDisplayOffset.y,
            myTransform.position.z);

        return Physics.OverlapBoxNonAlloc(
            myTransform.position + playerTransform.localScale.x * bc.center,
            bc.size / 2,
            collidingInteractionTriggers,
            Quaternion.identity,
            interactionLayerMask);
    }

    private void ChangeVisibilityInteractionUIDisplay()
    {
        if (numberOfInteractions > 0)
        {
            DoodleAnimationFile animation;
            
            switch (GetInteractive().type)
            {
                case InteractionType.DOGGO:
                    animation = Database.instance.doggoUIAnimation;
                    break;
                case InteractionType.THING:
                    animation = Database.instance.exclamationMarkUIAnimation;
                    break;
                case InteractionType.EXIT:
                    animation = Database.instance.exitUIAnimation;
                    break;
                default:
                    throw new Exception("Unknown type " + GetInteractive().type);
            }

            if (interactionUIDisplay.File != animation)
            {
                interactionUIDisplay.ChangeAnimation(animation);
            }
        }
        interactionUIDisplay.gameObject.SetActive(numberOfInteractions > 0);
    }
}
