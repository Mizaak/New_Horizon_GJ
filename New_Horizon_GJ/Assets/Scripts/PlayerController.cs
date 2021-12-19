using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Vector3 targetPosition;

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

            targetPosition = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            controller.Move(targetPosition * Time.deltaTime * playerSpeed);

            if (targetPosition != Vector3.zero)
            {
                gameObject.transform.forward = targetPosition;
                animator.SetBool("Walking", true);
            }
            else
            {
                animator.SetBool("Walking", false);
            }
            controller.Move(playerVelocity * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walking", false);
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
