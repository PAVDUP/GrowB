using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class DongleFeature : MonoBehaviour
{
    [HideInInspector] public bool isMerging;
    public enum DongleKind {One, Two, Three, Four, ThreeA, ThreeB};
    public DongleKind myDongleKind;

    [HideInInspector] public bool overDongleCheck;

    private void Start()
    {
        isMerging = true;
        overDongleCheck = false;
    }

    protected void OnCollisionStay2D(Collision2D collision)
    {
        // Debug.Log("Collision Occur");

        if (collision.transform.CompareTag("Dongle"))
        {
            DongleFeature otherDongle = collision.gameObject.GetComponent<DongleFeature>();

            if (myDongleKind == otherDongle.myDongleKind && !isMerging && !otherDongle.isMerging)
            {
                Debug.Log("merging Start");
                
                isMerging = true;
                otherDongle.isMerging = true;
                
                var position = transform.position;
                var position1 = otherDongle.transform.position;
                Vector2 spawnVector = new Vector2((position.x + position1.x)/2, 
                                                    (position.y + position1.y)/2);
                
                DongleManager.Instance.SpawnCollisionDongle(spawnVector, myDongleKind);
                Destroy(otherDongle.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
