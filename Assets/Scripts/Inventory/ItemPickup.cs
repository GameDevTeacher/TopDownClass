using System;
using UnityEngine;

namespace Inventory
{
   public class ItemPickup : MonoBehaviour, IPickupable
   {
      // aD = assetDictionary
      public DictionaryAsset aD;
      public PickupType type;
      
      private ObjectPool _objectPool;

      private void Start()
      {
         _objectPool = ObjectPool.instance;
      }

      public void OnPickup()
      {
         if (!PlayerPickupManager.InventoryDictionary.TryAdd(aD.prefabDictionary[type], 1))
         {
            PlayerPickupManager.InventoryDictionary[aD.prefabDictionary[type]] += 1;
         }
         //Destroy(gameObject);
         _objectPool.ReturnToPool(gameObject);
      }
   }
}