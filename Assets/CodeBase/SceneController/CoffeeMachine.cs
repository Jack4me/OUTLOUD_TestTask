using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeMachine : MonoBehaviour
{
    private Animator _animator;
    private void Awake() {
        _animator = GetComponent<Animator>();
    }


    public  void StartCoffeeMaking() {
        _animator.SetTrigger("CoffeeMaking");
    }

    public void StopCoffeeMaking() {
        _animator.SetTrigger("Idle");
    }
}
