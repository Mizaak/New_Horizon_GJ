using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;

    private bool canMove = false;
    public bool CanMove { get => canMove; set => canMove = value; }

    private Vector3 playerVelocity;
    private float playerSpeed = 2.5f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public void SetPosition(Vector3 position)
    {
        position.y = 0;
        Debug.Log(position);
        controller.enabled = false;
        controller.transform.localPosition = position;
        controller.enabled = true;
    }

    private void Movement()
    {
        if (CanMove)
        {
            playerVelocity.y = 0;

            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(move * Time.deltaTime * playerSpeed);

            if (move != Vector3.zero && canMove)
            {
                gameObject.transform.forward = move;
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!RoomTransitionController.running && other.tag == "Exit")
        {
            CanMove = false;
        }
    }
}
