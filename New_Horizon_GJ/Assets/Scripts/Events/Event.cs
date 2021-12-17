using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionTypeEnum
{
    Dialogue,
    Choiche,
    Teleport,
    SetNewEvent,
    Delay
}

public enum Side
{
    Left,
    Right
}

public class Event : ScriptableObject
{
    [SerializeField]
    ActionTypeEnum actionType;
    public ActionTypeEnum ActionType { get { return actionType; } }

    [Header("Dialogue Variables")]
    [SerializeField]
    string nameToShow;      // The name to show in the dialogue
    public string NameToShow { get { return nameToShow; } }
    [SerializeField]
    string dialogueToShow;      // The text to show in the dialogue
    public string DialogueToShow { get { return dialogueToShow; } }
    [SerializeField]
    Sprite speakerSprite;       // The sprite of the person who is speaking
    public Sprite Speaker { get { return speakerSprite; } }
    [SerializeField]
    [Tooltip("Where the face appears during the dialogue")]
    Side side;              // The side where the face appears
    public Side Side { get { return side; } }

    [Header("Choiches Variables")]
    [SerializeField]
    ChoicheValues[] choicheValues;
    public ChoicheValues[] ChoicheValues { get { return choicheValues; } }

    [Header("Run New Event Variables")]
    [SerializeField]
    Event newEventToSet;
    public Event NewEventToSet { get { return newEventToSet; } }
    [SerializeField]
    string interactableToChange;
    public string InteractableToChange { get { return interactableToChange; } }

    [Header("Teleport Variables")]
    [SerializeField]
    string objectToTeleportName;
    public string ObjectToTeleportName { get { return objectToTeleportName; } }
    [SerializeField]
    Vector3 targetPosition;
    public Vector2 TargetPosition { get { return targetPosition; } }
    //[SerializeField]
    //GameAreas gameAreaTarget;
    //public GameAreas GameAreaTarget { get { return gameAreaTarget; } }

    [Header("Delay Variables")]
    [SerializeField]
    float delayTime;
    public float DelayTime { get { return delayTime; } }
}

[System.Serializable]
public struct ChoicheValues
{
    [SerializeField]
    string choicheText;
    public string ChoicheText { get { return choicheText; } }
    [SerializeField]
    Event choicheEvent;
    public Event ChoicheEvent { get { return choicheEvent; } }
    [SerializeField]
    bool choicheAvaiable;
    public bool ChoicheAvaiable { get { return choicheAvaiable; } }
}