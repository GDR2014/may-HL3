using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Animator animator;
    public float attackDelay;
    public bool canAttack = true;

    void Update() {
        if( Input.GetMouseButtonDown( 0 ) && canAttack ) attack();

    }

    void attack() {
        animator.SetTrigger( "doAttack" );
    }

    IEnumerator beginCooldown() {
        canAttack = false;
        yield return new WaitForSeconds( attackDelay );
        canAttack = true;
    }
}
