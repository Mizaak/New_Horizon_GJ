using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [HideInInspector] public bool canPause;

    private bool canProgress;

    // General vars
    bool waitingInput = false;
    int actualInteractionCount = 0;
    Interaction actualInteraction;
    Event actualEvent;

    // Move Player vars
    float timer = 0;
    float timeToMove;
    //TODO
    // public PlayerController player;

    void Awake()
    {
        if (instance != this)
            instance = this;
    }

    void Start()
    {
        //player = FindObjectOfType<PlayerController>();
        canPause = true;
        canProgress = false;
    }

    void Update()
    {
        /*if (waitingInput && canProgress)
        {
            if (Input.GetButtonDown("Interact") && !GameManager.instance.GamePaused)
            {
                waitingInput = false;
                canPause = true;
                ContinueEvent();
            }
        }

        if (movePlayer)
        {
            MovePlayer();
        }*/
    }

    public void RunEvent(Event eventToRun)
    {
        if (eventToRun)
        {
            /*player.StopPlayer();
            player.isInteracting = true;
            actualEventCount = 0;
            actualEvent = eventToRun;*/
            ContinueEvent();
        }
        else
        {
            StopEvent();
        }
    }

    public void ContinueEvent()
    {
        waitingInput = false;
        if (actualInteraction != null)
        {
            if (actualInteractionCount < actualInteraction.Events.Length)
            {
                actualEvent = actualInteraction.Events[actualInteractionCount];
                switch (actualEvent.ActionType)
                {
                    case ActionTypeEnum.Dialogue:

                        canPause = false;
                        canProgress = false;
                        StartCoroutine(DelayInput());
                        //UIManager.instance.PlayDialogue(actualEvent);
                        waitingInput = true;
                        actualInteractionCount++;

                        break;
                    case ActionTypeEnum.Choiche:

                        canPause = false;
                        canProgress = false;
                        StartCoroutine(DelayInput());
                        //UIManager.instance.OpenChoicheCanvas(actualAction);

                        break;
                    case ActionTypeEnum.Teleport:

                        //UIManager.instance.CloseDialogueCanvas();
                        //UIManager.instance.CloseChoicheCanvas();
                        Teleport(actualEvent);
                        //CameraRig.instance.SetArea(actualAction.GameAreaTarget);
                        actualInteractionCount++;
                        ContinueEvent();

                        break;
                    case ActionTypeEnum.SetNewEvent:

                        //UIManager.instance.CloseDialogueCanvas();
                        //UIManager.instance.CloseChoicheCanvas();

                        SetNewEventToInteractable(actualEvent.NewEventToSet, actualEvent.InteractableToChange);
                        actualInteractionCount++;
                        ContinueEvent();

                        break;
                    case ActionTypeEnum.Delay:

                        canPause = false;
                        //UIManager.instance.CloseDialogueCanvas();
                        //UIManager.instance.CloseChoicheCanvas();
                        StartCoroutine(DelayEvent(actualEvent));

                        break;
                    default:
                        break;
                }
            }
            else
            {
                StopEvent();
            }
        }
    }

    public void StopEvent()
    {
        //player.isInteracting = false;
        actualInteraction = null;
        actualInteractionCount = 0;
        waitingInput = false;
        //UIManager.instance.CloseDialogueCanvas();
        //UIManager.instance.CloseChoicheCanvas();
    }

    public void SetNewEventToInteractable(Event newEvent, string interactableName)
    {
        /*Interactable[] interactables = FindObjectsOfType<Interactable>();
        foreach (Interactable interact in interactables)
        {
            if (interact.InteractableName == interactableName)
            {
                interact.SetNewEvent(newEvent);
                break;
            }
        }*/
    }

    public void ChoicheMade(int choiche)
    {
        if (canProgress)
        {
            canPause = true;
            //UIManager.instance.CloseChoicheCanvas();
            RunEvent(actualInteraction.Events[actualInteractionCount].ChoicheValues[choiche].ChoicheEvent);
        }
    }

    public void Teleport(Event teleportEvent)
    {
        if (teleportEvent.ObjectToTeleportName == "Player")
        {
            //player.transform.position = teleportEvent.TargetPosition;
        }
        else
        {
            /*Interactable[] interactables = FindObjectsOfType<Interactable>();
            foreach (Interactable interact in interactables)
            {
                if (interact.InteractableName == teleportEvent.ObjectToTeleportName)
                {
                    interact.gameObject.transform.position = teleportEvent.TargetPosition;
                    break;
                }
            }*/
        }
    }
    public IEnumerator DelayEvent(Event delayEvent)
    {
        yield return new WaitForSeconds(delayEvent.DelayTime);
        actualInteractionCount++;
        ContinueEvent();
    }

    public IEnumerator DelayInput()
    {
        yield return new WaitForSeconds(0.1f);
        canProgress = true;
    }

    public void CloseEvent()
    {
        /*if (actualAction.Blockable)
        {
            canPause = true;
            UIManager.instance.CloseChoicheCanvas();
            UIManager.instance.CloseDialogueCanvas();
            StopEvent();
        }*/
    }
}
