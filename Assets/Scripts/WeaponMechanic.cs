using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponMechanic : MonoBehaviour
{

    [SerializeField] GameObject projectile;
    [SerializeField] Transform shotPoint;
    [SerializeField] Transform player;

    float timeBtwShots;

    [SerializeField] float startTimeBtwShots;
    [SerializeField] float rotationOffset;
    [SerializeField] float posYOffset;

    void Update()
    {
        float rotZ = RotateWeapon();

        ShootProjectile(rotZ);

    }

    private float RotateWeapon()
    {
        transform.position = new Vector3(player.position.x, player.position.y + posYOffset, player.position.z);

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        return rotZ;
    }

    private void ShootProjectile(float rotZ)
    {
        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotZ + rotationOffset));
                timeBtwShots = startTimeBtwShots;
            }

        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
}
