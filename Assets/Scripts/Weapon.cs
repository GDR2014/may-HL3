using Assets.Scripts.Non_behaviours;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Animator animator;
    public float attackDelay;
    public bool canAttack = true;

    public int DamageAmount = 99999;
    public float HitForce = 1000000;

    private Collider2D collider;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        collider.enabled = false;
    }

    void Update() {
        if( Input.GetMouseButtonDown( 0 ) && canAttack ) attack();
    }

    void attack() {
        collider.enabled = true;
        animator.SetTrigger( "doAttack" );
    }

    void stopAttack()
    {
        collider.enabled = false;
    }

    IEnumerator beginCooldown() {
        canAttack = false;
        yield return new WaitForSeconds( attackDelay );
        canAttack = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var other = collider.gameObject;
        AbstractEnemy enemy = other.GetComponent<AbstractEnemy>();
        if (enemy == null) return;
        var otherRB = other.GetComponent<Rigidbody2D>();
        if (otherRB != null) otherRB.AddForce((other.transform.position - transform.position) * HitForce);
        Debug.Log("Crowbar hit " + other.name);
        enemy.TakeDamage(DamageAmount);
    }
}
