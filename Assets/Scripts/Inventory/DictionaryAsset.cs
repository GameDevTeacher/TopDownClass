using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(menuName = "Scrubs/Data")]
    public class DictionaryAsset : ScriptableObject
    {
        [SerializeField] public Dictionary<PickupType, GameObject> prefabDictionary;
    }

    public enum PickupType
    {
        Gem,
        Coin,
        GoldCoin,
        Feathers
    }
}