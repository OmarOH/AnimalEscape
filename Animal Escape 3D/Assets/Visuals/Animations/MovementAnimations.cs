using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAnimations : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Mathf.Abs(rb.velocity.magnitude) > 0f && Mathf.Abs(rb.velocity.magnitude) <= 2f)
        {
            SetAnimation(gameObject, "Walk");
        }
        if(Mathf.Abs(rb.velocity.magnitude) > 2f)
        {
            SetAnimation(gameObject, "Run");
        }
        else
        {
            SetAnimation(gameObject, "Idle");
        }

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
