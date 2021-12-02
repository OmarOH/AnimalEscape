using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    private Animator anim;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void SetAnimation(GameObject animObj, string boolName)
    {
        TurnOffBools(animObj.GetComponent<Animator>());
        animObj.GetComponent<Animator>().SetBool(boolName, true);
    }

    private void TurnOffBools(Animator anim)
    {
        foreach(AnimatorControllerParameter parameter in anim.parameters) 
        {            
            anim.SetBool(parameter.name, false);            
        }
    }
}
