using Assets.Scripts.Non_behaviours;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    public float moveForce = 300f;
    public int maxHealth = 100;
    private int _health = 100;
    public int health {
        get{
            return _health;
        }
        set
        {
            _health = value;
            healthText.text = "Health: " + _health;
        }
    }

    public GUIText healthText;

    private Rigidbody2D rb;
    private Animator anim;

    public float animationSpeedMultiplier = 0.5f;

    public float dmgCooldownTime = .33f;
    private bool canTakeDamage = true;

    private GameManager gameManager;

    private void Awake() { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update() {
        if (gameManager.gameOver) return;
        handleFlip();
    }

    private void FixedUpdate() {
        if (gameManager.gameOver) return;
        handleMove();
    }

    private void handleFlip() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint( Input.mousePosition );
        Vector2 scale = transform.localScale;
        if( mousePos.x < transform.position.x ) scale.x = -1;
        else scale.x = 1;
        transform.localScale = scale;
    }

    private void handleMove() {
        Vector2 direction = getMoveVector();
        updateAnimation();
        move( direction );
    }

    private Vector2 getMoveVector() {
        Vector2 velocity = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
        velocity = Vector2.ClampMagnitude( velocity, 1 );
        return velocity;
    }

    private void move( Vector2 direction ) {
        Vector2 force = direction * moveForce;
        rb.AddForce( force );
    }

    private void updateAnimation() {
        anim.SetFloat("speed", rb.velocity.magnitude);
    }

    public void TakeDamage(int dmgAmount)
    {
        if (!canTakeDamage) return;
        anim.SetTrigger("doHurt");
        health -= dmgAmount;
        if (health <= 0) Die();
        canTakeDamage = false;
        StartCoroutine(dmgCooldown());
    }

    public void Die()
    {
        gameManager.gameOver = true;
        StartCoroutine(DecayAndDestroy());
        GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>().Invoke("StopAllCoroutines", 10);
    }

    private IEnumerator dmgCooldown()
    {
        yield return new WaitForSeconds(dmgCooldownTime);
        canTakeDamage = true;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        GameObject other = col.collider.gameObject;
        if (!other.tag.Equals("Enemy")) return;
        TakeDamage(other.GetComponent<Headcrab>().Damage);
    }

    private IEnumerator DecayAndDestroy()
    {
        DisableCollisions();
        Destroy(transform.Find("Weapon").gameObject);
        Material m = renderer.material;
        float endTime = Time.time + 2;
        while (m.color.a > 0)
        {
            float t = 1 - (endTime - Time.time) / 2;
            m.color = Color.Lerp(m.color, Color.clear, t);
            yield return null;
        }
    }

    private void DisableCollisions()
    {
        foreach (var col in GetComponents<Collider2D>()) col.isTrigger = true;
    }
}