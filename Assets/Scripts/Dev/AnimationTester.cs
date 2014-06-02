using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationTester : MonoBehaviour {

    public KeyCode triggerKeyCode = KeyCode.N;
    public KeyCode resetKeyCode = KeyCode.R;

    public String resetState = "Idle";
    public String triggerName = "";

    void Update() {
        bool shouldTrigger = Input.GetKeyDown( triggerKeyCode );
        bool shouldReset = Input.GetKeyDown( resetKeyCode );

        if( !(shouldReset || shouldTrigger)) return;
        Animator animator = GetComponent<Animator>();
        if( shouldTrigger ) animator.SetTrigger(triggerName);
        if( shouldReset ) animator.CrossFade(resetState, 5.0f);
    }
}
