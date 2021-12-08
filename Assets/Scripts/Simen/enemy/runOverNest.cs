using UnityEngine;

public class runOverNest : MonoBehaviour
{
    [SerializeField] private Collider deathTrigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LawnMower"))
        {
            print("Meesa Dedsa");
            Destroy(gameObject);
        }
        else print("Not lawnmower");
    }
}
