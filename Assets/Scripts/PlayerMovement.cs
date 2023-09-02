using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem runParticle;
    [SerializeField] Transform shotPoint;

    [SerializeField] float offset;
    [SerializeField] float speedMovement;

    Rigidbody2D rb;
    Animator animator;
    void Start()
    {
        rb = FindAnyObjectByType<Rigidbody2D>();
        animator = transform.GetComponent<Animator>();
        runParticle = transform.GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, vertical, 0f);

        if (movement.magnitude > 1f)
        {
            movement = movement.normalized;
        }
        else
        {
            movement = movement;
        }

        AnimationActivation(horizontal, vertical);

        transform.Translate(movement * speedMovement * Time.deltaTime);

        if (animator.GetBool("isRunning") == true)
        {
            if (runParticle.isPlaying == false)
            {
                runParticle.Play();
            }
        }
        else
        {
            runParticle.Stop();
        }
    }

    private void AnimationActivation(float horizontal, float vertical)
    {
        if (Mathf.Abs(vertical) > 0f)
        {
            animator.SetBool("isRunning", true);
        }

        if (horizontal > 0f)
        {
            PlayerAnimation(true, Mathf.Abs(transform.localScale.x));
            
        }
        else if (horizontal < 0f)
        {
            PlayerAnimation(true, -Mathf.Abs(transform.localScale.x));
           

            
        }
        else if (horizontal == 0f && vertical == 0f)
        {
            PlayerAnimation(false, Mathf.Abs(transform.localScale.x));
            
        }
    }

    private void PlayerAnimation(bool isAnimating, float scaleX)
    {
        animator.SetBool("isRunning", isAnimating);
        transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
    }
}
