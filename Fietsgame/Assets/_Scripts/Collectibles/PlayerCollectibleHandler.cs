using UnityEngine;

public class PlayerCollectibleHandler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Player collided with: {other.name}, tag: {other.tag}");

        if (other.CompareTag("PassedZone"))
        {
            CollectibleManager collectibleItem = other.GetComponentInParent<CollectibleManager>();
            if (collectibleItem != null)
            {
                collectibleItem.OnCollectiblePassed();
            }
        }
    }
}