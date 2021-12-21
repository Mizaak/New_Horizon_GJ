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

    [SerializeField]
    private InteractionProvider interactionProvider;


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
                player.CanMove = false;
                //start dialog with brother
                break;
            case GameState.VIDEO_REEL:
                if(!videoPlayer.isPlaying)
                {
                    videoPlayer.Play();
                    videoPlayer.loopPointReached += VideoCompleted;
                    player.CanMove = false;
                }
                break;
            case GameState.INTRO_GAME_1:
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

        DialogueManager.instance.RunEvent(interactionProvider.IntroBeforeCutscene);
        DialogueManager.Stop += IntroCompleted;
        roomsList[0].SetActive(true);

        currentState = GameState.INTRO;
    }

    private void IntroCompleted()
    {
        DialogueManager.Stop -= IntroCompleted;

        currentState = GameState.VIDEO_REEL;
    }

    private void VideoCompleted(VideoPlayer source)
    {
        videoPlayer.loopPointReached -= VideoCompleted;

        roomChangeEnabled = true;
        player.CanMove = true;
        videoPlayer.gameObject.SetActive(false);
        DialogueManager.instance.RunEvent(interactionProvider.IntroBeforeGame_1);
        DialogueManager.Stop += GameIntroComplete;

        currentState = GameState.INTRO_GAME_1;
    }

    private void GameIntroComplete()
    {
        DialogueManager.Stop -= GameIntroComplete;
        currentState = GameState.MINIGAME_1;
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
    INTRO_GAME_1,
    MINIGAME_1,
    INTRO_GAME_2,
    MINIGAME_2,
    INTRO_GAME_3,
    MINIGAME_3,
}