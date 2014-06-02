using Assets.Scripts.Non_behaviours;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public Animator animator;
    public float attackDelay;
    public bool canAttack = true;

    public int DamageAmount = 99999;
    public float HitForce = 1000000;

    private Collider2D col;

    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        col.enabled = false;
    }

    void Update() {
        if (gameManager.gameOver) return;
        if( Input.GetMouseButtonDown( 0 ) && canAttack ) attack();
    }

    void attack() {
        col.enabled = true;
        animator.SetTrigger( "doAttack" );
    }

    void stopAttack()
    {
        col.enabled = false;
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
