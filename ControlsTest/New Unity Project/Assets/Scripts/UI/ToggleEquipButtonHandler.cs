using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof (Button))]
public class ToggleEquipButtonHandler : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    private Text[] Texts;
    // Start is called before the first frame update
    void Start()
    {
        Texts = GetComponentsInChildren<Text>();
        GlobalData.SelectedItemType = GlobalData.ItemTypes.MovementItem;
        Texts[(int)GlobalData.ItemTypes.CombatItem].enabled = true;
        Texts[(int)GlobalData.ItemTypes.MovementItem].enabled = false;
    }

    public void OnButtonPress()
    {
        //toggle the enable status of both text fields
        Texts[(int)GlobalData.ItemTypes.CombatItem].enabled ^= true;
        Texts[(int)GlobalData.ItemTypes.MovementItem].enabled ^= true;

        //switch the equipped item type
        if( GlobalData.SelectedItemType == GlobalData.ItemTypes.MovementItem )
        {
            GlobalData.SelectedItemType = GlobalData.ItemTypes.CombatItem;
        }
        else
        {
            GlobalData.SelectedItemType = GlobalData.ItemTypes.MovementItem;
        }
        
    }
    

}
