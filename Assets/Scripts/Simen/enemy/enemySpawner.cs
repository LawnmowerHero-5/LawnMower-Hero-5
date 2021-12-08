using UnityEngine;
using Random = UnityEngine.Random;

public class enemySpawner : MonoBehaviour
{
    public int poolUsed;
    
    [SerializeField] private int minSpawnCount = 1;
    [SerializeField] private int maxSpawnCount = 3;
    [SerializeField] private Collider spawnTrigger;
    [SerializeField] private Collider exitSpawnTrigger;

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

        if (!canSpawn || pooler.pools[poolUsed].activeObjects >= pooler.pools[poolUsed].size)
        {
            triggerCollider = null;
            return;
        }
            
        canSpawn = false;
        var count = Random.Range(minSpawnCount, maxSpawnCount);
        var tag = "";
        if (poolUsed == 0) tag = "gnome";
        else tag = "wasp";
        
        
        for (var i = 0; i < count; i++)
        {
            pooler.SpawnFromPool(tag,
                transform.position + new Vector3(Random.Range(-0.5f, 0.5f), 2f, Random.Range(-0.5f, 0.5f)),
                Quaternion.identity);
            
            pooler.pools[poolUsed].activeObjects++;
            
            if (pooler.pools[poolUsed].activeObjects >= pooler.pools[poolUsed].size) break; //Exits loop if all objects in pool are activated
        }
        
        triggerCollider = null;
    }

    private void ExitedTrigger()
    {
        print("EXIT");
        triggerExit = false;
        canSpawn = true;
    }

    //TODO: Destroy spawner on different attacks
}
