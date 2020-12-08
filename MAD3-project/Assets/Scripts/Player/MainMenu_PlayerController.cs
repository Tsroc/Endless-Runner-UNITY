using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu_PlayerController: MonoBehaviour
{
    /*
        PlayerController_MainMenu allows for player controls at the main sceen. Controls are limited to performing the rest animation.
    */

    private Animator animator;
    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update() {
        ProcessRest();
    }

    /*
        Player performs rest animation if space is pressed.
    */
    private void ProcessRest(){
        if(Input.GetKeyDown("space")){
            if (!animator.IsInTransition (0)) {
                animator.Play("Rest");
            }
        }
    }
}