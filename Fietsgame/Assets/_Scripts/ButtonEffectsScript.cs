using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffectsScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    // BUTTON SPRITES
    [Header("Sprites")]
    public Sprite defaultSprite;
    public Sprite clickedSprite;
    public Sprite highlightedSprite;

    // BOOLEAN SETTINGS FOR EFFECTS
    [Header("Effects")]
    public bool enableHighlight = true;
    public bool enableClickChange = true;
    public bool enableClickSound = true;

    // COMPONENTS
    private Image buttonImage;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip clickSound;

    void Start()
    {
        buttonImage = GetComponent<Image>();

        if (buttonImage && defaultSprite)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enableHighlight && highlightedSprite)
        {
            buttonImage.sprite = highlightedSprite;
            _audioSource.PlayOneShot(clickSound);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (enableHighlight && defaultSprite)
        {
            buttonImage.sprite = defaultSprite;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (enableClickChange && clickedSprite)
        {
            buttonImage.sprite = clickedSprite;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (enableClickChange && defaultSprite)
        {
            buttonImage.sprite = defaultSprite;
        }
    }
}
