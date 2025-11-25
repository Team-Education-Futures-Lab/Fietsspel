using UnityEngine;

[CreateAssetMenu(fileName = "New Traffic Sign", menuName = "Traffic/Traffic Sign")]
public class TrafficSignData : ScriptableObject
{
    public string signName;
    [TextArea] public string description;
    public Sprite signImage;
}
