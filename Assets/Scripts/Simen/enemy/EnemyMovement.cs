using UnityEngine;

public class EnemyMovement : MonoBehaviour, IPooledObject
{
    // Movement towards player
    public float speed;
    public float rotateSpeed;
    public transformVariable target;
    public float range = 10f;

    public bool inCombat;
    public bool isActive;

    private bool affectingSpeed;

    private Rigidbody _RB;

    private void Start()
    {
        _RB = GetComponent<Rigidbody>();
    }

    public void OnObjectSpawn()
    {
        isActive = true;
    }
    
    private void Update()
    {
        if (isActive)
        {
            if (Vector3.Distance(transform.position, target.playerTransform.position) <= range)
            {
                // Move our position a step closer to the target
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, target.playerTransform.position, step);
                print("Player spotted, chase started");
                inCombat = true;
                
                //Rotates towards target
                var targetDir = target.playerTransform.position - transform.position;
                var rotateStep = rotateSpeed * Time.deltaTime;
            
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, targetDir, rotateStep, 0f));
            }
            else
            {
                print("Where are you player?");
                inCombat = false;
            }

            //Sets velocity to prevent impacts from affecting enemies' movement
            _RB.velocity = CompareTag("Wasp") ? Vector3.zero : new Vector3(0f, _RB.velocity.y, 0f);
        }
    }
}