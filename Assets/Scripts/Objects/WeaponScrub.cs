using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Scrubs/Tools/Weapons")]
public class WeaponScrub : ScriptableObject
{
    [Header("Data")]
    [Tooltip("This is a string that defines the name of the weapon")]
    public new string name;
    [TextArea(1,10)] public string description;
    
    [Space(5)]public float damage;
    [Range(0,20)]public float range;
    public DamageType damageType;
    
    [Header("Visuals")]
    public Sprite icon;
    public Sprite weaponSprite;
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