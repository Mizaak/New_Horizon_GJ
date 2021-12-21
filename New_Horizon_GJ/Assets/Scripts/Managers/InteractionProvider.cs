using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionProvider", menuName = "ScriptableObjects/Data", order = 1)]
public class InteractionProvider : ScriptableObject
{
    // Start is called before the first frame update
    [SerializeField]
    private Interaction introBeforeCutscene;
    [SerializeField]
    private Interaction introBeforeGame_1;

    public Interaction IntroBeforeCutscene { get => introBeforeCutscene; set => introBeforeCutscene = value; }
    public Interaction IntroBeforeGame_1 { get => introBeforeGame_1; set => introBeforeGame_1 = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}