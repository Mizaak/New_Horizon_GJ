using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private int targetRoom;
    [SerializeField]
    private GameObject targetDoor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !RoomTransitionController.running)
        {
            RoomTransitionController.start(targetRoom, targetDoor);
        }
    }
}
