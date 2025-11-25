using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForPath : MonoBehaviour
{
    [SerializeField] private string groundTag;
    [SerializeField] private string OutOfBoundsTag;

    [SerializeField] private GameObject GameOverScreen;

    [SerializeField] public bool hasDied;


    private void Update()
    {
        if (hasDied)
        {
            GameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("OutOfBounds") || collision.collider.CompareTag("Obstacle"))
        {
            Debug.LogWarning("Player is out of bounds");
            hasDied = true;
        }

        if (collision.collider.CompareTag("Ground"))
        {
            hasDied = false;
        }
    }

    private void playerLost()
    {

    }

}
