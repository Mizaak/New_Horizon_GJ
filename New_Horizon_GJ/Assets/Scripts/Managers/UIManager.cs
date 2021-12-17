using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FadeState
{
    FadedIn,
    FadedOut,
    FadingIn,
    FadingOut
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    void Awake()
    {
        if (instance != this)
            instance = this;
    }

    // Private Variables

    bool isRunningDialogue = false;
    public bool IsRunningDialogue { get { return isRunningDialogue; } }

    [Header("Dialogue Variables")]
    [SerializeField]
    GameObject dialogueCanvas;
    [SerializeField]
    Text nameText;
    [SerializeField]
    Text dialogueText;
    [SerializeField]
    Image leftFaceSprite;
    [SerializeField]
    Image rightFaceSprite;

    [Header("Choiche Variables")]
    [SerializeField]
    GameObject choicheCanvas;
    [SerializeField]
    Text[] choicheTexts;
    [SerializeField]
    GameObject[] coichePanels;

    [Header("Pause Variables")]
    public GameObject pauseCanvas;
    public GameObject pausePanel;
    public GameObject optionsPanel;


    //Private Fade Variables
    float fadeValue = 0;
    float fadeTime = 1;

    private void Start()
    {
        pauseCanvas.SetActive(false);
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
    }

    private void Update()
    {

    }

    public void PlayDialogue(Event dialogueEvent)
    {
        if (!dialogueCanvas.activeSelf)
            dialogueCanvas.SetActive(true);

        if (dialogueEvent.ActionType == ActionTypeEnum.Dialogue)
        {
            dialogueText.text = dialogueEvent.DialogueToShow;
            nameText.text = dialogueEvent.NameToShow;

            switch (dialogueEvent.Side)
            {
                case Side.Left:
                    rightFaceSprite.enabled = false;
                    leftFaceSprite.enabled = true;
                    leftFaceSprite.sprite = dialogueEvent.Speaker;
                    break;
                case Side.Right:
                    leftFaceSprite.enabled = false;
                    rightFaceSprite.enabled = true;
                    rightFaceSprite.sprite = dialogueEvent.Speaker;
                    break;
                default:
                    break;
            }
        }
    }

    public void CloseDialogueCanvas()
    {
        dialogueCanvas.SetActive(false);
    }

    public void OpenChoicheCanvas(Event choicheEvent)
    {
        if (choicheEvent.ActionType == ActionTypeEnum.Choiche)
        {
            if (!choicheCanvas.activeSelf)
                choicheCanvas.SetActive(true);

            int actualPanelInt = 0;

            for (int i = 0; i < 3; i++)
            {
                if (choicheEvent.ChoicheValues.Length > i && choicheEvent.ChoicheValues[i].ChoicheAvaiable)
                {
                    coichePanels[actualPanelInt].SetActive(true);
                    choicheTexts[actualPanelInt].text = choicheEvent.ChoicheValues[i].ChoicheText;
                    actualPanelInt++;
                }

                if (actualPanelInt < 2)
                {
                    coichePanels[2].SetActive(false);
                }
            }
        }
    }

    public void CloseChoicheCanvas()
    {
        choicheCanvas.SetActive(false);
        for (int i = 0; i < 3; i++)
        {
            coichePanels[i].SetActive(false);
        }
    }

    #region PauseMenu
    public void UpdatePauseCanvasStatus()
    {
        /*if (GameManager.instance.GamePaused)
        {
            pauseCanvas.SetActive(true);
            pausePanel.SetActive(true);
            optionsPanel.SetActive(false);
        }
        else
        {
            pauseCanvas.SetActive(false);
            pausePanel.SetActive(false);
            optionsPanel.SetActive(false);
        }*/
    }
    #endregion

    #region OptionsPanel
    public void OnButtonOptionsClick()
    {
        //AudioManager.instance.gMasterSlider.value = AudioManager.instance.data.masterValue;
        //AudioManager.instance.gMusicSlider.value = AudioManager.instance.data.musicValue;
        //AudioManager.instance.gSfxSlider.value = AudioManager.instance.data.sfxValue;

        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OnQuitButtonOptionsClick()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
    #endregion
}
