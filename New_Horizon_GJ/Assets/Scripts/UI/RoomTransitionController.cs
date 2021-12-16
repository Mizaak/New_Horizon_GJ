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

    public delegate void TransitionEvent();

    public TransitionEvent halfComplete;
    public TransitionEvent fullComplete;

    // Start is called before the first frame update
    void Start()
    {
        if (!TryGetComponent<Image>(out image))
        {
            Debug.LogWarning("Missing transition image maybe create it dynamically?");
        }
        halfComplete += TransitionEventDebug;
        fullComplete += TransitionEventDebug;
        StartTransition();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartTransition()
    {
        image.material.DOFade(1f, transitionDurationIn).SetEase(Ease.InOutSine).OnComplete(HalfTransitionCompleted);
    }

    private void HalfTransitionCompleted()
    {
        halfComplete();
        image.material.DOFade(0f, transitionDurationOut).SetEase(Ease.InOutSine).OnComplete(TransitionCompleted);
    }

    private void TransitionCompleted()
    {
        fullComplete();
    }

    private void TransitionEventDebug()
    {
        Debug.Log("TRANSITION EVENT");
    }
}
