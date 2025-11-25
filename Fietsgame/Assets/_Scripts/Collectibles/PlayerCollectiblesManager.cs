using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCollectiblesManager : MonoBehaviour
{
    public CollectibleManager colManager;
    public AudioClip collectSound;
    public AudioSource audioSource;

    public GameObject BikeProgress;
    public GameObject[] BikeParts;
    public GameObject[] Transparant;
    public TextMeshProUGUI bikePart;

    void Start()
    {
        BikeProgress.SetActive(false);
    }

    void BikePartCollected()
    {
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        colManager.OnCollectibleCollected();
        colManager.OnCollectiblePassed();
        StartCoroutine(ShowBikeProgress());
    }

    IEnumerator ShowBikeProgress()
    {
        BikeProgress.SetActive(true);
        yield return new WaitForSeconds(4);
        BikeProgress.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Banden")) { CollectPart(0, "BANDEN", other); }
        if (other.CompareTag("Trappers")) { CollectPart(3, "TRAPPERS", other); }
        if (other.CompareTag("Remmen")) { CollectPart(4, "REMMEN", other); }
        if (other.CompareTag("Verlichting")) { CollectPart(5, "VERLICHTING", other); }
        if (other.CompareTag("Frame")) { CollectPart(2, " FIETS FRAME", other); }
        if (other.CompareTag("Fietsbel")) { CollectPart(1, "FIETSBEL", other); }
    }

    private void CollectPart(int partIndex, string partName, Collider other)
    {
        bikePart.text = partName;
        other.gameObject.GetComponentInParent<Collider>().enabled = false;
        BikeParts[partIndex].SetActive(true);
        Transparant[partIndex].SetActive(false);
        BikePartCollected();
        Destroy(other.gameObject);
    }
}
