using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    private int currentRoom = 0;
    [SerializeField]
    private List<GameObject> roomsList;
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private MainMenu mainMenu;
    [SerializeField]
    private VideoPlayer videoPlayer;

    private GameState currentState;
    private bool roomChangeEnabled  = false;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.MAIN_MENU;
        RoomTransitionController.start += changeRoomStart;
        RoomTransitionController.halfComplete += changeRoom;
        RoomTransitionController.fullComplete += changeRoomComplete;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentState);
        switch (currentState)
        {
            case GameState.MAIN_MENU:
                player.CanMove = false;
                break;
            case GameState.INTRO:
                player.CanMove = true;
                roomsList[0].SetActive(true);
                //start dialog with brother
                currentState = GameState.VIDEO_REEL;
                break;
            case GameState.VIDEO_REEL:
                if(!videoPlayer.isPlaying)
                {
                    videoPlayer.Play();
                    videoPlayer.loopPointReached += ReelCompleted;
                    player.CanMove = false;
                }
                break;
            case GameState.MINIGAME_1:
                break;
            case GameState.INTRO_GAME_2:
                break;
            case GameState.MINIGAME_2:
                break;
            case GameState.INTRO_GAME_3:
                break;
            case GameState.MINIGAME_3:
                break;
            default:
                break;
        }
    }

    public void startGame()
    {
        Debug.Log("start");
        RoomTransitionController.start(-1,null);
        RoomTransitionController.halfComplete += mainMenu.hide;
        RoomTransitionController.halfComplete += startIntro;
    }

    private void startIntro(int room, GameObject door)
    {
        RoomTransitionController.halfComplete -= startIntro;
        currentState = GameState.INTRO;
    }

    private void ReelCompleted(VideoPlayer source)
    {
        videoPlayer.loopPointReached -= ReelCompleted;
        roomChangeEnabled = true;
        player.CanMove = true;
        currentState = GameState.MINIGAME_1;
        videoPlayer.gameObject.SetActive(false);
    }

    private void changeRoom(int room, GameObject door)
    {
        if(roomChangeEnabled)
        {
            if (room != -1 && roomsList[room] != null && door != null)
            {
                roomsList[room].SetActive(true);
                roomsList[currentRoom].SetActive(false);
                currentRoom = room;
                player.SetPosition(door.transform.parent.localPosition + door.transform.localPosition);
            }
        }
    }

    private void changeRoomStart(int room, GameObject door)
    {
        if (roomChangeEnabled)
        {
            player.CanMove = false;
        }
    }

    private void changeRoomComplete(int room, GameObject door)
    {
        if (roomChangeEnabled)
        {
            player.CanMove = true;
        }
    }
}

enum GameState
{
    MAIN_MENU,
    INTRO,
    VIDEO_REEL,
    MINIGAME_1,
    INTRO_GAME_2,
    MINIGAME_2,
    INTRO_GAME_3,
    MINIGAME_3,
}