using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove2D : MonoBehaviour
{

    private Animator animator;

    private Vector3 targetPosition;
    private Vector3 mousePosition;
    private Vector3 playerPosition;

    public Camera camera;
    public bool followMouse;
    public bool accelerates;
    public float speed = 5.0f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        if (camera == null)
        {
            //throw new InvalidOperationException("Camera not set");
        }
    }

    void Update()
    {
        playerPosition = gameObject.transform.position;
        if (followMouse || Input.GetMouseButton(0))
        {
            targetPosition = camera.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;

            if (targetPosition.x <= targetPosition.z)
            {
                //transform.localScale = new Vector3(1, 1, 1);
                animator.SetTrigger("Player1Left");
            }
            else if(targetPosition.x >= targetPosition.z)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                animator.SetTrigger("Player1Right");
            }
            else if (targetPosition.y >= targetPosition.x)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                animator.SetTrigger("Player1Up");
            }
            else if (targetPosition.y <= targetPosition.x)
            {
                //transform.localScale = new Vector3(-1, 1, 1);
                animator.SetTrigger("Player1Down");
            }
            else
            {
                animator.SetTrigger("Player1Idle");
            }

        }

        //// Attack ////
        if (Input.GetMouseButton(1))
        {
            animator.SetTrigger("Player1Attack");


        }

        var delta = speed * Time.deltaTime;
        if (accelerates)
        {
            delta *= Vector3.Distance(transform.position, targetPosition);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, delta);
    }
}
