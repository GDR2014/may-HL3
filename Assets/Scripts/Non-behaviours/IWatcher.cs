using UnityEngine;

public interface IWatcher {
    void onNotice( Collider2D other );
    void onSee( Collider2D other );
    void onLost( Collider2D other );
}