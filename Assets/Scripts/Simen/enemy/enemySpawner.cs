using UnityEngine;
using Random = UnityEngine.Random;

public class enemySpawner : MonoBehaviour
{
    [SerializeField] private int minSpawnCount = 1;
    [SerializeField] private int maxSpawnCount = 3;
    [SerializeField] private BoxCollider spawnTrigger;
    [SerializeField] private BoxCollider exitSpawnTrigger;

    [HideInInspector] public bool triggerEnter;
    [HideInInspector] public bool triggerExit;
    [HideInInspector] public Collider triggerCollider;
    
    private ObjectPooler pooler;
    private bool canSpawn = true;
    
    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        if (triggerEnter) EnteredTrigger(triggerCollider);
        else if (triggerExit) ExitedTrigger();
    }

    private void EnteredTrigger(Collider other)
    {
        triggerEnter = false;

        if (!canSpawn || pooler.pools[0].activeObjects >= pooler.pools[0].size)
        {
            triggerCollider = null;
            return;
        }
            
        canSpawn = false;
        var count = Random.Range(minSpawnCount, maxSpawnCount);
        
        for (var i = 0; i < count; i++)
        {
            pooler.SpawnFromPool("gnome",
                transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 2f, Random.Range(-0.5f, 0.5f)),
                Quaternion.identity);
            
            pooler.pools[0].activeObjects++;
            
            if (pooler.pools[0].activeObjects >= pooler.pools[0].size) break; //Exits loop if all objects in pool are activated
        }
        
        triggerCollider = null;
    }

    private void ExitedTrigger()
    {
        print("EXIT");
        triggerExit = false;
        canSpawn = true;
    }
}
