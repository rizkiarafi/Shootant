using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour
{
    [SerializeField] int enemyLife;

    Animator enyAnimator;
    EnemyMovement enyMovement;

    private void Start()
    {
        enyAnimator = GetComponent<Animator>();
        enyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        Die();
    }

    private void Die()
    {
        if (enemyLife <= 0)
        {
            Destroy(enyMovement);
            Destroy(transform.GetComponent<BoxCollider2D>());
            enyAnimator.SetBool("isDead", true);
            Destroy(this.gameObject, 1f);
        }
    }

    public void TakeDamage()
    {
        enemyLife -= 1;
    }

    
}
