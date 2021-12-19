using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;
public class RoomTransitionController : MonoBehaviour
{
    [SerializeField]
    private Image image;
    private float transitionDurationIn = 2f;
    private float transitionDurationOut = 2f;

    public delegate void TransitionEvent(int room,GameObject door);

    public static TransitionEvent halfComplete;
    public static TransitionEvent fullComplete;
    public static TransitionEvent start;

    public static bool running { get; internal set; }

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Image>(out image))
        {
            Debug.LogWarning("Missing transition image maybe create it dynamically?");
        }
        
        start += StartTransition;
        StartTransition(-1, null);
        image.material.SetColor("_Color", new Color(1, 1, 1, 1));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void StartTransition(int room, GameObject door)
    {
        running = true;
        Debug.Log("dofade");
        image.material.DOFade(1f, transitionDurationIn).SetEase(Ease.InOutSine).OnComplete(() => HalfTransitionCompleted(room,door));
    }

    private void HalfTransitionCompleted(int room, GameObject door)
    {
        Debug.Log("dofade_2");
        image.material.DOFade(0f, transitionDurationOut).SetEase(Ease.InOutSine).OnComplete(() => TransitionCompleted(room, door));
        halfComplete(room, door);
    }

    private void TransitionCompleted(int room, GameObject door)
    {
        running = false;
        fullComplete(room,door);
    }

}
