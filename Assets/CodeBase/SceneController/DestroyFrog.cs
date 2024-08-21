using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase;
using UnityEngine;

public class DestroyFrog : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out FrogBehavior frogBehavior)) {
            Destroy(frogBehavior.gameObject);
        }
    }
}
