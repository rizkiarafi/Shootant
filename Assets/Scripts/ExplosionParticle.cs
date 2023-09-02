using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionParticle : MonoBehaviour
{
    [SerializeField] GameObject explosionParticle;

    ParticleSystem particleSystem;

    public void PlayParticle(Vector3 pos)
    {
        GameObject particle = Instantiate(explosionParticle, pos, Quaternion.identity);

        StartCoroutine(DestroyParticle(particle));
    }

    IEnumerator DestroyParticle(GameObject particle)
    {
        yield return new WaitForSeconds(3f);
        Destroy(particle);
    }
}
