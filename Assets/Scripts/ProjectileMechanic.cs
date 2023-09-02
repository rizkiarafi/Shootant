using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMechanic : MonoBehaviour
{
    [SerializeField] LayerMask whatIsSolid;
    [SerializeField] float distance;

    [SerializeField] float projectileSpeed;

    CameraShake camShake;
    ExplosionParticle explosionParticle;
    private void Start()
    {
        camShake = FindAnyObjectByType<CameraShake>();
        explosionParticle = FindAnyObjectByType<ExplosionParticle>();
       
    }
    void Update()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.tag == "Enemy")
            {
                hitInfo.collider.transform.GetComponent<EnemyLife>().TakeDamage();
                camShake.CamShake();
                hitInfo.collider.transform.GetComponent<EnemyMovement>().StartCoroutine("KnockBack");
                hitInfo.collider.transform.GetComponent<Animator>().SetTrigger("KnockBack");
                explosionParticle.PlayParticle(this.transform.position);
            }
            DestroyProjectile(0f);
        }
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        Vector3 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.Translate(Vector2.up * Time.deltaTime * projectileSpeed);
        DestroyProjectile(2f);
    }

    private void DestroyProjectile(float time)
    {
        Destroy(this.gameObject, time);
    }
}
