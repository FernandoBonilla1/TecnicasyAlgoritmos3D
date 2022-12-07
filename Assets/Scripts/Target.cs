using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Variable de tipo animator
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject _colliders;

    private float _disableDelay = 0.5f; 

    private void Awake(){
        _colliders.SetActive(false);
        animator = GetComponent<Animator>();
    }

    public void DropTarget()
    {
        animator.SetTrigger("Fall");
        _colliders.SetActive(false);
    }

    public void RiseTarget()
    {
        _colliders.SetActive(true);
        animator.SetTrigger("Rise");
    }
}
