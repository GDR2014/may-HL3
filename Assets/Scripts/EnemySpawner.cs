using UnityEngine;
using System.Collections;
using Assets.Scripts.Non_behaviours;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

    public Vector2[] spawnPoints;
    public Bounds spawnAreaSize;

    public AbstractEnemy[] enemies;
    public Transform target;

    public float spawnInterval = 2f;
    public float minDelay = .4f;

    private Camera cam;
    private Plane[] planes;

    private List<Vector2> spawnPointBuffer;

	private void Start () {
        Debug.Log("Start called!");
        spawnPointBuffer = new List<Vector2>(spawnPoints.Length);
        cam = Camera.main;
        planes = GeometryUtility.CalculateFrustumPlanes(cam);
        StartCoroutine(EnemySpawnRoutine());
    }

    private IEnumerator EnemySpawnRoutine()
    {
        Debug.Log("Coroutine started!");
        spawnPointBuffer.Clear();
        yield return new WaitForSeconds(spawnInterval);
        foreach(var point in spawnPoints ) if( !IsInView(point) ) spawnPointBuffer.Add(point);
        if (spawnPointBuffer.Count == 0) StartCoroutine(EnemySpawnRoutine());
        int idx = Random.Range(0, spawnPointBuffer.Count);
        SpawnAt(spawnPointBuffer[idx], enemies[0]);
        spawnInterval *= .9999f;
        spawnInterval = Mathf.Max(spawnInterval, minDelay);
        StartCoroutine(EnemySpawnRoutine());
    }

    private bool IsInView(Vector2 spawner)
    {
        spawnAreaSize.center = spawner;
        return GeometryUtility.TestPlanesAABB(planes, spawnAreaSize);
    }

    private void SpawnAt(Vector2 position, AbstractEnemy enemy)
    {
        Headcrab crab = (Headcrab)Instantiate(enemy, position, Quaternion.identity);
        crab.target = target;
    }
}
