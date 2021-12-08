using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    private ObjectPooler pooler;

    private void Start()
    {
        pooler = ObjectPooler.Instance;
    }

    private void FixedUpdate()
    {
        pooler.SpawnFromPool("Gnome", transform.position + new Vector3(0f, 2f, 0f), Quaternion.identity);
    }
}
