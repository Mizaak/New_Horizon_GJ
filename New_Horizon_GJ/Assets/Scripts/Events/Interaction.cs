using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction", menuName = "ScriptableObjects/Interaction", order = 1)]
public class Interaction : ScriptableObject
{
    [SerializeField]
    private Event[] events;
    public Event[] Events { get { return events; } }
}
