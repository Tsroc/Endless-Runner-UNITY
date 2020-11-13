using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_MainMenu : MonoBehaviour
{
    private Animator animator;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() {
        ProcessRest();
    }

    private void ProcessRest(){
        if(Input.GetKeyDown("space")){
            if (!animator.IsInTransition (0)) {
                animator.Play("Rest");
            }
        }
    }
}