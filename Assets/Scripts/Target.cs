using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Variable de tipo animator
    [SerializeField] private Animator animator;

    private float _disableDelay = 0.5f; 

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        animator.SetTrigger("Rise");
    }

    private void Update(){
        //if(Input.GetKeyDown(KeyCode.E)){
        //    //animator.SetTrigger("Rise");
        //    _minigameManager.RiseRandomTargets();
        //}
        //if(Input.GetKeyDown(KeyCode.R)){
        //    animator.SetTrigger("Fall");
        //}
    }

    public void DropTarget()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Fall");
        Invoke("DisableTarget", _disableDelay);
    }

    private void DisableTarget()
    {
        gameObject.SetActive(false);
    }
}
