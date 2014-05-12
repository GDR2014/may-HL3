using System.Collections;
using UnityEngine;

public class Crosshair : MonoBehaviour {

    private Camera camera;

    // Cursor stuff
    public Texture2D CursorTexture;
    public CursorMode CursorMode = CursorMode.Auto;
    public Vector2 HotSpot = new Vector2( .5f, .5f );

    // Pew pew stuff
    public int BulletDamage = 1;
    public float BulletRange = 100.0f;


    private void Start() {
        camera = Camera.main;
        StartCoroutine( SetCursor() );
    }

    private void Update() {
        if( !Input.GetMouseButtonDown( 0 ) ) return;
        Ray ray = camera.ScreenPointToRay( Input.mousePosition );
        RaycastHit hit;

        if( !Physics.Raycast( ray, out hit, BulletRange ) ) return;
        Health health = hit.transform.gameObject.GetComponent<Health>();
        if( health == null ) return;
        health.TakeDamage(BulletDamage);
    }

    private IEnumerator SetCursor() {
        yield return new WaitForSeconds( .1f );
        Cursor.SetCursor( CursorTexture, HotSpot, CursorMode );
    }
}