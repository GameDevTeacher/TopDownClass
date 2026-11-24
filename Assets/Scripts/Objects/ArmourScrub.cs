using UnityEngine;

[CreateAssetMenu(fileName = "New Armour", menuName = "Scrubs/Tools/Armour")]
public class ArmourScrub : ScriptableObject
{
    [Header("Data")]
    [Space(5)]
    [Tooltip("This is a string that defines the name of the weapon")]
    public new string name;
    [TextArea(1,10)] public string description;
    
    [Space(5)]public float health;
    [Range(0,10)]public float damageSoak;
    public DamageType strongAgainst;
    public DamageType weakAgainst;
    
    [Header("Visuals")]
    public Sprite icon;
    public Sprite armourSprite;
    public GameObject prefab;

    public enum DamageType
    {
        Normal,
        Poison,
        Fire,
        Bleeding,
        Electric,
        Magic,
        Cursed,
        Holy,
        Vakaris,
        Necrotic,
    }
}