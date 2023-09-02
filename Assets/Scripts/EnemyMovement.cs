using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Transform player;

    [SerializeField] float speed;
    [SerializeField] float movementThreshold = 0.01f;
    [SerializeField] float knockBackRate = 0.2f;

    private Vector3 previousPosition;
    private Animator enemyAnim;

    float speedTemp;

    void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>().transform;
        previousPosition = transform.position;
        enemyAnim = transform.GetComponent<Animator>();

        speedTemp = speed;
    }

    void Update()
    {
        MoveEnemy();

        EnemyFacing();
    }

    private void EnemyFacing()
    {
        Vector3 directionToTarget = player.position - transform.position;

        bool isFacingRight = directionToTarget.x > 0;

        if (isFacingRight)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), 
            transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), 
            transform.localScale.y, transform.localScale.z);
        }
    }

    private void MoveEnemy()
    {
        float step = speed * Time.deltaTime;

        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, step);

        if (Vector3.Distance(transform.position, player.position) > 1.5f)
        {
            transform.position = newPosition;
        }

        float distanceMoved = Vector3.Distance(transform.position, previousPosition);
        bool isMoving = distanceMoved > movementThreshold;

        previousPosition = transform.position;

        enemyAnim.SetBool("isMoving", isMoving);
    }


    public IEnumerator KnockBack()
    {
        if (enemyAnim.GetBool("isDead") == false)
        {
            speed = 0f;
            yield return new WaitForSeconds(knockBackRate);
            speed = speedTemp;
        }
        
    }
}
