using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Animator animator;

    private bool is_crouching = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        float move_amount = Mathf.Clamp01(Mathf.Abs(movement.x) + Mathf.Abs(movement.z));

        animator.SetFloat("MoveAmount", move_amount, .25f, Time.deltaTime);

        if (move_amount > 0f)
        {
            Quaternion target_rotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, target_rotation, 500 * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            is_crouching = !is_crouching;

            if(is_crouching == true)
            {
                animator.CrossFade("Locomotion_Crouch", .25f);
            }
            else
            {
                animator.CrossFade("Locomotion", .15f);
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && move_amount > 0f)
        {
            animator.SetBool("IsRunning", true);
            is_crouching = false;
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }
}
