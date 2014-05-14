using UnityEngine;

public class Player : MonoBehaviour {

    public Animator animator;

    public float MOVE_SPEED = 1.0f;
    private const float MOVE_CONST = 4.0f;
    public float animationSpeedMultiplier = 0.5f;

    void Update() {
        Vector2 velocity = calculateVelocity();
        updateAnimationSpeed( velocity );
        move( velocity );
    }

    private void move( Vector2 velocity ) {
        Vector2 position = transform.position;
        position += velocity * Time.deltaTime;
        transform.position = position;
    }

    private void updateAnimationSpeed( Vector2 velocity ) {
        animator.speed = velocity.magnitude * animationSpeedMultiplier;
    }

    private Vector2 calculateVelocity() {
        Vector2 velocity = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
        velocity = Vector2.ClampMagnitude( velocity, 1 );
        velocity *= ( MOVE_CONST * MOVE_SPEED );
        return velocity;
    }
}

