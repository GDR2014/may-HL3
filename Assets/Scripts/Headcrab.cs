using Assets.Scripts.Non_behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headcrab : AbstractEnemy, IWatcher {

    private List<FieldOfVision> _fovs;
    public Animator anim;

    public float decayDelay = 0f;
    public float decayTime = 2f;

    public float Speed = 10f;

    public Transform target;

    void Awake() {
        _fovs = new List<FieldOfVision>(GetComponentsInChildren<FieldOfVision>());
        foreach( FieldOfVision fov in _fovs ) {
            fov.Watcher = this;
        }
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
    }

    public void onNotice( Collider2D other ) {
    }

    public void onSee( Collider2D other ) {
    }

    public void onLost( Collider2D other ) {
    }

    protected override void Hurt(int dmgAmount)
    {
        anim.SetTrigger("doHurt");
    }

    protected override void Die()
    {
        collider2D.isTrigger = true;
        StartCoroutine(DecayAndDestroy());
    }

    private IEnumerator DecayAndDestroy()
    {
        Material m = transform.FindChild("Sprite").renderer.material;
        yield return new WaitForSeconds(decayDelay);
        float endTime = Time.time + decayTime;
        while (m.color.a > 0)
        {
            float t = 1 - (endTime - Time.time) / decayTime;
            m.color = Color.Lerp(m.color, Color.clear, t);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
