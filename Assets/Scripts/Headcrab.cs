using Assets.Scripts.Non_behaviours;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headcrab : AbstractEnemy, IWatcher {

    private List<FieldOfVision> _fovs;
    public Animator anim;

    public int minDmg = 2, maxDmg = 6;
    public int Damage {get{return Random.Range(minDmg, maxDmg+1);}}

    public float decayDelay = 0f;
    public float decayTime = 2f;

    public float Speed = 10f;

    public Transform target;

    public bool gavePoints = false;

    private static GameManager gameManager;

    void Awake() {
        Health = 1;
        _fovs = new List<FieldOfVision>(GetComponentsInChildren<FieldOfVision>());
        foreach( FieldOfVision fov in _fovs ) {
            fov.Watcher = this;
        }
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        if(Health > 0) transform.position = Vector2.MoveTowards(transform.position, target.position, Speed * Time.deltaTime);
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
        GivePoints();
        StartCoroutine(DecayAndDestroy());
    }

    private void GivePoints()
    {
        if (gavePoints) return;
        gavePoints = true;
        gameManager.Score++;
    }

    private IEnumerator DecayAndDestroy()
    {
        Material m = transform.Find("Sprite").renderer.material;
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
