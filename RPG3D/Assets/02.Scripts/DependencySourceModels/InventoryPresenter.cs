using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ULB.RPG.Collections;
using ULB.RPG.DataModels;
using System.Linq;

namespace ULB.RPG.DataDependencySources
{
    public class InventoryPresenter
    {
        public InventorySource inventorysource;
        public ItemsEquippedSource itemsEquippedSource;
        public AddCommand addCommand;
        public RemoveCommand removeCommand;
        public SwapCommand swapCommand;
        public EquipCommand equipCommand;

        #region Inventory Source
        public class InventorySource : ObservableCollection<ItemData>
        {
            public InventorySource(IEnumerable<ItemData> data)
            {
                foreach (var item in data)
                {
                    Items.Add(item);
                }
            }
        }
        #endregion

        #region ItemsEquipped Source

        public class ItemsEquippedSource : ObservableCollection<int>
        {
            public ItemsEquippedSource(IEnumerable<int> data)
            {
                foreach (var item in data)
                {
                    Items.Add(item);
                }
            }
        }
        #endregion

        public InventoryPresenter()
        {
            inventorysource = new InventorySource(InventoryDataModel.instance);
            InventoryDataModel.instance.OnItemAdded += (slotID, item) =>
            {
                inventorysource.Add(item);
            };
            InventoryDataModel.instance.OnItemRemoved += (slotID, item) =>
            {
                inventorysource.Remove(item);
            };
            InventoryDataModel.instance.OnItemChanged += (slotID, item) =>
            {
                inventorysource.Change(slotID, item);
            };

            itemsEquippedSource = new ItemsEquippedSource(ItemsEquippedDataModel.instance);
            ItemsEquippedSource.instance.OnItemAdded += (slotID, item) =>
            {
                source.Add(item);
            };
            ItemsEquippedSource.instance.OnItemRemoved += (slotID, item) =>
            {
                source.Remove(item);
            };
            ItemsEquippedSource.instance.OnItemChanged += (slotID, item) =>
            {
                source.Change(slotID, item);
            };

            addCommand = new AddCommand();
            removeCommand = new RemoveCommand();
            swapCommand = new SwapCommand();
            equipCommand = new EquipCommand();
        }

        #region Add command
        public class AddCommand
        {
            public InventoryDataModel _dataModel;

            public AddCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(ItemData item)
            {
                return true;
            }

            public void Execute(ItemData item)
            {
                int index = _dataModel.FindIndex(x => x.id == item.id);
                if (index < 0)
                {
                    _dataModel.Add(item);
                }
                else
                {
                    _dataModel.Change(index, new ItemData(item.id, item.num + _dataModel.Items[index].num));
                }
            }

            public bool TryExecute(ItemData item)
            {
                if (CanExecute(item))
                {
                    Execute(item);
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Remove command
        public class RemoveCommand
        {
            public InventoryDataModel _dataModel;

            public RemoveCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(ItemData item)
            {
                int index = _dataModel.FindIndex(x => x.id == item.id);
                return index >= 0 && item.num <= _dataModel.Items[index].num;
            }

            public void Execute(ItemData item)
            {
                int index = _dataModel.FindIndex(x => x.id == item.id);
                //if (index < 0)
                //{
                //    throw new System.Exception($"[InventoryPresenter] : �������� �ʴ� �������� �����Ϸ��� �õ��߽��ϴ�. {item.id}");
                //}
                //else if (item.num > _dataModel.Items[index].num)
                //{
                //    throw new System.Exception($"[InventoryPresenter] : {item.id} �� {item.num} �� �����Ϸ��� �õ������� {_dataModel.Items[index].num} �� �ۿ� �������� �ʽ��ϴ�.");
                //}
                //else 
                if (item.num == _dataModel.Items[index].num)
                {
                    _dataModel.Remove(item);
                }
                else
                {
                    _dataModel.Change(index, new ItemData(item.id, item.num + _dataModel.Items[index].num));
                }
            }

            public bool TryExecute(ItemData item)
            {
                if (CanExecute(item))
                {
                    Execute(item);
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Swap command
        public class SwapCommand
        {
            public InventoryDataModel _dataModel;

            public SwapCommand()
            {
                _dataModel = InventoryDataModel.instance;
            }

            public bool CanExecute(int slot1, int slot2)
            {
                return true;
            }

            public void Execute(int slot1, int slot2)
            {
                ItemData tmp = _dataModel.Items[slot1];
                _dataModel.Change(slot1, _dataModel.Items[slot2]);
                _dataModel.Change(slot2, tmp);
            }

            public bool TryExecute(int slot1, int slot2)
            {
                if (CanExecute(slot1, slot2))
                {
                    Execute(slot1, slot2);
                    return true;
                }

                return false;
            }
        }
        #endregion

        #region Equip command

        public class EquipCommand
        {
            InventoryDataModel _inventoryDataModel;
            ItemsEquippedDataModel _itemsEquippedDataModel;

            public EquipCommand()
            {
                _inventoryDataModel = InventoryDataModel.instance;
                _itemsEquippedDataModel = ItemsEquippedDataModel.instance;
            }

            public bool CanExecute(int slotID, int itemID)
            {
                // -> �������� �´���, ������ �����ϴ���, ������ �����ϴ���,
                // �Ѽչ���� ���и� �����ϰ��ִµ� �κ��丮 ��������� ��չ��� �����Ϸ��� �ߴ��� ���..üũ�ؾ���
                return ItemInfoAssets.instance[itemID].prefab is Equipment;
            }

            public void Execute(int slotID, int itemID)
            {
                // �����ҷ��� ����� Ÿ��
                int equipType = (int)((Equipment)ItemInfoAssets.instance[itemID].prefab).type;

                // �̹� �����ϰ��ִ� �������� ID
                int itemEquippedID = _itemsEquippedDataModel.Items[equipType];

                // �����ϰ��ִ� �������� ID �� �κ��丮 ���� ���� (�����ϰ��ִ��� ������ ���)
                _inventoryDataModel.Change(slotID,
                                           itemEquippedID < 0 ? ItemData.empty : new ItemData(itemEquippedID, 1));

                // �����Ϸ��� �������� ID �� ��񽽷� ����
                _itemsEquippedDataModel.Change(equipType, itemID);
            }

            public bool TryExecute(int slotID, int itemID)
            {
                if (CanExecute(slotID, itemID))
                {
                    Execute(slotID, itemID);
                    return true;
                }
                return false;
            }
        }

        #endregion
    }
}