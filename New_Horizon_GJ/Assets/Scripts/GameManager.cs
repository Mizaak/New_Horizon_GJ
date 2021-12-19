using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int currentRoom = 0;
    [SerializeField]
    private List<GameObject> roomsList;
    [SerializeField]
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        RoomTransitionController.halfComplete += changeRoom;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeRoom(int room,GameObject door)
    {
        if(room != -1 && roomsList[room] != null && door != null)
        {
            roomsList[room].SetActive(true);
            roomsList[currentRoom].SetActive(false);
            currentRoom = room;
            player.SetPosition(door.transform.parent.localPosition + door.transform.localPosition);
        }
        player.CanMove = true;
    }
}
