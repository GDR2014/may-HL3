using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FieldOfVision : MonoBehaviour {

    public IWatcher Watcher { get; set; }

    void Start() {
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnTriggerEnter2D( Collider2D other ) {
        Watcher.onNotice(other);
    }

    void OnTriggerStay2D( Collider2D other ) {
        Watcher.onSee( other );
    }

    void OnTriggerExit2D( Collider2D other ) {
        Watcher.onLost( other ); // Not working properly because of Unity bugs
    }
}
