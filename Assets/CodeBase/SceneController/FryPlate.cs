using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryPlate : MonoBehaviour {
    private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }


    public  void StartFrying() {
        _animator.SetTrigger("IsFrying");
    }

    public void StopFrying() {
        _animator.SetTrigger("Idle");
    }
}
