using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

#region Enums
enum DogState
{
    Sleeping,
    HalfAsleep
}
#endregion

public class Dog : MonoBehaviour
{
    #region Variables
    //actual dog state
    [ReadOnly] [SerializeField] [BoxGroup("AI State")] private DogState actualState;
    [Space(10)]
    //actual timer value used to update the AI status
    [ReadOnly] [SerializeField] [BoxGroup("AI State")] private float actualTimerAIValue;

    //pool containing all timer values when the dog is sleeping
    [SerializeField] [BoxGroup("AI State Timers")] private float[] DogSleepingTimePool;
    //pool containing all timer values when the dog is hals asleep
    [SerializeField] [BoxGroup("AI State Timers")] private float[] DogHalfAsleepTimePool;

    //ONLY FOR TESTING
    [Space(10)]
    [SerializeField] [BoxGroup("JUST FOR TESTING")] private Material sleepMaterial;
    [SerializeField] [BoxGroup("JUST FOR TESTING")] private Material halfAsleepMaterial;
    [Space(10)]
    [SerializeField] [BoxGroup("JUST FOR TESTING")] private bool simulatePlayerMovement;
    #endregion
    #region MonoBehaviour
    private void Start()
    {
        //pools checks
        if (DogSleepingTimePool.Length == 0)
            Debug.LogError("DogSleepingTimePool has no values");
        if (DogHalfAsleepTimePool.Length == 0)
            Debug.LogError("DogHalfAsleepTimePool has no values");

        InitializeDogState();

        //JUST FOR TESTING
        simulatePlayerMovement = false;
    }
    private void Update()
    {
        ExecuteAI();
    }
    #endregion
    #region Function
    //function used to execute the AI code
    private void ExecuteAI()
    {
        actualTimerAIValue -= Time.deltaTime;

        switch (actualState)
        {
            case DogState.Sleeping:
                //DO NOTHING
                //animations? 

                //EXIT CONDITIONS
                if (actualTimerAIValue <= 0)
                {
                    ChangeState(DogState.HalfAsleep);
                    actualTimerAIValue = DogHalfAsleepTimePool[Random.Range(0, DogHalfAsleepTimePool.Length)];
                    GetComponentInChildren<MeshRenderer>().material = halfAsleepMaterial;
                }
                break;

            case DogState.HalfAsleep:
                //ACTIONS
                //check if the player moves
                if (simulatePlayerMovement)
                {
                    //tp the Player out of the kichen
                    simulatePlayerMovement = false;
                    InitializeDogState();
                }

                //walky talky conditions

                //EXIT CONDITIONS
                if (actualTimerAIValue <= 0)
                {
                    ChangeState(DogState.Sleeping);
                    actualTimerAIValue = DogSleepingTimePool[Random.Range(0, DogSleepingTimePool.Length)];
                    GetComponentInChildren<MeshRenderer>().material = sleepMaterial;
                }
                break;
        }
    }
    //function used to change the current dog state
    private void ChangeState(DogState newState)
    {
        actualState = newState;
    }
    //function used to initialize the dog state
    private void InitializeDogState()
    {
        //SENTIRE STECCA COME PARTE IL DOGGO
        //dog state initialization
        actualState = DogState.Sleeping;
        actualTimerAIValue = DogSleepingTimePool[Random.Range(0, DogSleepingTimePool.Length)];
        GetComponentInChildren<MeshRenderer>().material = sleepMaterial;
    }
    #endregion
}
