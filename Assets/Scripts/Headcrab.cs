using System.Collections.Generic;
using UnityEngine;

public class Headcrab : MonoBehaviour, IWatcher {

    private List<FieldOfVision> _fovs; 

    void Awake() {
        _fovs = new List<FieldOfVision>(GetComponentsInChildren<FieldOfVision>());
        foreach( FieldOfVision fov in _fovs ) {
            Debug.Log("Setting watcher of " + fov.transform.name + " to " + transform.name);
            fov.Watcher = this;
        }
    }

    public void onNotice( Collider2D other ) {
    }

    public void onSee( Collider2D other ) {
    }

    public void onLost( Collider2D other ) {
        Debug.Log( transform.name + " lost sight of " + other.transform.name );
    }
}
