using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    THING, DOGGO, EXIT
}

public class Interactive : MonoBehaviour
{
    public string interactionId;
    public InteractionType type;
    public DoodleStudio95.DoodleAnimationFile doodleAnimation;
}
