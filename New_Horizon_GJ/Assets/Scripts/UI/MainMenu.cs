using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject panel;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hide(int room, GameObject door)
    {
        panel.SetActive(false);
        RoomTransitionController.halfComplete -= hide;
    }
}