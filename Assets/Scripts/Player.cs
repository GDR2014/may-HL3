using UnityEngine;

public class Player : MonoBehaviour {

    public bool isWalking = false;
    public Animator animator;
    public float MOVE_SPEED = 1.0f;
    private const float MOVE_CONST = 0.1f;

    void Update() {
        Vector2 velocity = new Vector2( Input.GetAxis( "Horizontal" ), Input.GetAxis( "Vertical" ) );
        velocity.Normalize();
        Vector2 position = transform.position;
        position += velocity * ( MOVE_CONST * MOVE_SPEED );
        transform.position = position;
    }
}
