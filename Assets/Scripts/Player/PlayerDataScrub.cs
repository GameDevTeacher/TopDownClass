using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerData",  menuName = "Scrubs/Player", order = 1)]
public class PlayerDataScrub : ScriptableObject
{
    public float health;
    public Vector2 playerTransform;

    public void GildThePig(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            // Gild the Pig
        }
    }
}