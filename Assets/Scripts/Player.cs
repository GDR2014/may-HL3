using UnityEngine;

public class Player : MonoBehaviour {


    public float moveForce = 300f;

    private Rigidbody2D rb;
    private Animator anim;

    public float animationSpeedMultiplier = 0.5f;

    private void Awake() { 
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        handleFlip();
    }

    private void FixedUpdate() {
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
        updateAnimationSpeed( direction );
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

    private void updateAnimationSpeed( Vector2 velocity ) {
        anim.speed = velocity.magnitude * animationSpeedMultiplier;
    }
}