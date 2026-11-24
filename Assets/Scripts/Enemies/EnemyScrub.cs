using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Scrubs/NPC/Enemy")]
public class EnemyScrub : ScriptableObject
{
    public new string name;
    public EnemyType type;
    public int armour;
    public float health;
    public WeaponScrub WeaponScrub;
    
    public Weakness weakness;
    public Weakness weakness2;
    
    public Sprite sprite;
    public Animator animator;
    public BoxCollider2D hitBox;
    
    public enum Weakness
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
    
    public enum EnemyType
    {
        Light,
        Medium,
        Heavy,
    }
}
