using UnityEngine;

public class PopupTimer : MonoBehaviour
{
    public GameObject popupPanel;

    void Start()
    {
        Invoke("ShowPopup", 3f);
    }

    void ShowPopup()
    {
        SetActiveRecursively(popupPanel, true);
    }


    void SetActiveRecursively(GameObject obj, bool state)
    {
        obj.SetActive(state);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, state);
        }
    }
}
