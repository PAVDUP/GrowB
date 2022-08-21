using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleFeatureB : DongleFeature
{
    [Range(0,1)]
    public float explossionRange = 0.5f;

    new void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("Collision Occur");
        
        if (collision.transform.CompareTag("Dongle"))
        {
            if (!isMerging)
            {
                LayerMask mask = LayerMask.GetMask("Dongle");

                Collider2D[] _collider2Ds = new Collider2D[50];
                var size = Physics2D.OverlapCircleNonAlloc(transform.position, explossionRange, _collider2Ds, mask);
                for (int i = 0; i < size; i++)
                {
                    Destroy(_collider2Ds[i].gameObject);
                }

                PointManager.Instance.Point += size * 100;
                GameObject.Find("Bomb").GetComponent<AudioSource>().Play();
                
                // Explosion Effect
                GameObject tempExplosion = GameObject.Find("Bomb Particle");
                tempExplosion.transform.position = transform.position;
                tempExplosion.GetComponent<ParticleSystem>().Play();

                Destroy(gameObject);
            }
        }
        
    }
}
