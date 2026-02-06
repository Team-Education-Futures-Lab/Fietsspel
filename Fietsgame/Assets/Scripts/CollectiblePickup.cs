using UnityEngine;

public class CollectiblePickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Counter.Instance != null)
            {
                Counter.Instance.AddItem();
            }

            Destroy(gameObject);
        }
    }
}