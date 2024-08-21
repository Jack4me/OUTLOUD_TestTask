using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase {
    public class FrogTrigger : MonoBehaviour {
        public List<TargetTransform> targetTransforms = new List<TargetTransform>();
        private Player player;

        private void Start() {
            player = FindObjectOfType<Player>();

        }

        private void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent(out FrogBehavior frogBehavior)) {
                foreach (var t in targetTransforms) {
                    if (!t.isOccupied) {
                        t.isOccupied = true;
                        frogBehavior.StartMove(t.transform);
                        if (player != null) {
                            player.frogs.Add(frogBehavior);
                        }
                        break;
                    }
                }
            }
        }

        public void FreeTransform(TargetTransform target) {
            target.isOccupied = false;
        }
    }
}