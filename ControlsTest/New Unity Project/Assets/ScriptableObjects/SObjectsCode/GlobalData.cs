using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GlobalData", menuName = "ScriptableObjects/GlobalData", order = 1)]
public class GlobalData : ScriptableObject
{
    public enum ItemTypes { MovementItem = 0, CombatItem };
    public ItemTypes SelectedItemType;

    void Start()
    {
        SelectedItemType = ItemTypes.MovementItem;
    }
}
