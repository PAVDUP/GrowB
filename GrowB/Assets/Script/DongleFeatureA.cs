using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DongleFeatureA : DongleFeature
{
    new void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Dongle"))
        {
            
        }
    }
}
