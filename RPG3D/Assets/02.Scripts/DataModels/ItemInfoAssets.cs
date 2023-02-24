using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace ULB.RPG.DataModels
{
    public class ItemInfoAssets : MonoBehaviour
    {
        public static ItemInfoAssets instance
        {
            get
            {
                _instance = null;
                if (_instance)
                {
                    _instance = Instantiate(Resources.Load<ItemInfoAssets>("ItemInfoAssets"));
                    _instance.Init();
                }
                return _instance;
            }
        }
        private static ItemInfoAssets _instance;

        public ItemInfo this[int itemID] => _itemInfoDictionary[itemID];

        [SerializeField] private List<ItemInfo> _itemInfoList;
        private Dictionary<int, ItemInfo> _itemInfoDictionary = new Dictionary<int, ItemInfo>();

        private void Init()
        {
            foreach (var itemInfo in _itemInfoList)
            {
                _itemInfoDictionary.Add(itemInfo.id, itemInfo);
            }
        }
    }
}
