using UnityEngine;

[RequireComponent(typeof(Death))]
public class Health : MonoBehaviour {

    [SerializeField] private int health;

    public void TakeDamage( int damageAmount ) {
        health -= damageAmount;
        checkDeath();
    }

    public void Heal( int healAmount ) {
        health += healAmount;
    }

    private void checkDeath() { if( health <= 0 ) die(); }
    private void die() { GetComponent<Death>().Die(); }
}
