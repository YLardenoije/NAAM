using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent (typeof (Button))]
public class ToggleEquipButtonHandler : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    private Text[] Texts;
    [SerializeField] private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = FindObjectOfType<Button>();
        Texts = button.GetComponentsInChildren<Text>();
        GlobalData.SelectedItemType = GlobalData.ItemTypes.MovementItem;
        Texts[(int)GlobalData.ItemTypes.CombatItem].enabled = true;
        Texts[(int)GlobalData.ItemTypes.MovementItem].enabled = false;
    }

    public void OnButtonPress()
    {
        button.image.color = Color.red; //DEBUG, turns the button red on press
        //toggle the enable status of both text fields
        Texts[(int)GlobalData.ItemTypes.CombatItem].enabled = !Texts[(int)GlobalData.ItemTypes.CombatItem].enabled;
        Texts[(int)GlobalData.ItemTypes.MovementItem].enabled = !Texts[(int)GlobalData.ItemTypes.MovementItem].enabled;

        //switch the equipped item type
        if ( GlobalData.SelectedItemType == GlobalData.ItemTypes.MovementItem )
        {
            GlobalData.SelectedItemType = GlobalData.ItemTypes.CombatItem;
        }
        else
        {
            GlobalData.SelectedItemType = GlobalData.ItemTypes.MovementItem;
        }

        if( GlobalData.SelectedItemType == GlobalData.ItemTypes.CombatItem )
        {
            button.image.color = Color.blue; //DEBUG
        }
    }
    

}
