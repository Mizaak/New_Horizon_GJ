using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTest : MonoBehaviour
{
    public Interaction interaction;

    public void RunInteraction()
    {
        DialogueManager.instance.RunEvent(interaction);
    }
}
