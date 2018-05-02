using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    SimplePlayerController playerController;
    PlayerInteractionController interactionController;

    Animator animator;
    Transform myTransform;

    bool movingVertically;
    bool movingHorizontally;

    private void Awake()
    {
        playerController = GetComponent<SimplePlayerController>();
        interactionController = GetComponentInChildren<PlayerInteractionController>();
        animator = GetComponent<Animator>();
        myTransform = transform;
    }

    private void Update()
    {
        if (movingVertically || movingHorizontally)
        {
            animator.SetBool("walk", true);

            if (playerController.Direction != playerController.PreviousDirection)
            {
                if (rotateSpriteCoroutine != null)
                {
                    StopCoroutine(rotateSpriteCoroutine);
                }
                rotateSpriteCoroutine = StartCoroutine(RotateSprite(playerController.Direction == 1));
            }
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    public void ReqMove(Vector3 movementDirection)
    {
        if (CanMove())
        {
            playerController.Move(movementDirection);
            movingHorizontally = movementDirection.x != 0;
            movingVertically = movementDirection.z != 0;
        }
        else
        {
            playerController.Move(Vector3.zero);
        }
    }

    bool CanMove()
    {
        return !UIManager.instance.ShowingPausePanel() &&
            !UIManager.instance.ShowingTravelMap() &&
            !InteractionManager.Instance.PlayingInteraction;
    }

    bool CanInteract()
    {
        return !UIManager.instance.ShowingPausePanel() &&
            !UIManager.instance.ShowingTravelMap() &&
            !InteractionManager.Instance.PlayingInteraction
            && interactionController.HasInteractionsInRange();
    }

    public void ReqInteract()
    {
        if (CanInteract())
        {
            Interactive interactive = interactionController.GetInteractive();
            InteractionManager.Instance.PlayInteraction(interactive);
        }
    }

    Coroutine rotateSpriteCoroutine;

    IEnumerator RotateSprite(bool rotateRight)
    {
        float startXScale = myTransform.localScale.x;

        float targetTime = (1 + Mathf.Abs(startXScale)) / 6;

        float elapsedTime = 0f;

        while (elapsedTime < targetTime)
        {
            startXScale = Mathf.Lerp(startXScale, rotateRight ? -1f : 1f, elapsedTime / targetTime);
            elapsedTime += Time.deltaTime;

            myTransform.localScale = new Vector3(startXScale,
            myTransform.localScale.y,
            myTransform.localScale.z);

            yield return null;
        }

        myTransform.localScale = new Vector3(rotateRight ? -1f : 1f,
            myTransform.localScale.y,
            myTransform.localScale.z);
    }
}
