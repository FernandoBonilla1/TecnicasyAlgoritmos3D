using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //Variable de tipo animator
    [SerializeField] private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.E)){
            animator.SetTrigger("Rise");
        }
        if(Input.GetKeyDown(KeyCode.R)){
            animator.SetTrigger("Fall");
        }
    }

    public void DropTarget()
    {
        animator.SetTrigger("Fall");
    }
}
