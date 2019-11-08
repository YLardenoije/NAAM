using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiBehaviour : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    [SerializeField] private TMPro.TextMeshProUGUI HandedNessText;
    [SerializeField] private TMPro.TextMeshProUGUI LevelNameText;
    [SerializeField] private TMPro.TextMeshProUGUI WeaponNameText;
    [SerializeField] private TMPro.TextMeshProUGUI MovementNameText;
    [SerializeField] private Button SceneNameClickable;
    [SerializeField] private List<string> SceneNames;
    private int SelectedScene;

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(SceneNames[SelectedScene]);
    }

    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }

    public void OnOptionsButtonClicked()
    {
        if (GlobalData.SelectedHandedness == GlobalData.Handedness.Lefthanded)
        {
            HandedNessText.text = "Left-handed";
        }
        else
        {
            HandedNessText.text = "Right-handed";
        }
    }

    public void OnToolsButtonClicked()
    {
        if (GlobalData.SelectedMovementItem == GlobalData.MovementItems.Grapple)
        {
            MovementNameText.text = "Grapple";
        }
        else
        {
            MovementNameText.text = "Jetpack";
        }

        if( GlobalData.SelectedCombatItem == GlobalData.CombatItems.FireBall )
        {
            WeaponNameText.text = "Fireball";
        }
        else
        {
            WeaponNameText.text = "Scatter";
        }
    }

    public void OnHandednessButtonClicked()
    {
        if( GlobalData.SelectedHandedness == GlobalData.Handedness.Lefthanded )
        {
            GlobalData.SelectedHandedness = GlobalData.Handedness.Righthanded;
            HandedNessText.text = "Right-handed";
        }
        else
        {
            GlobalData.SelectedHandedness = GlobalData.Handedness.Lefthanded;
            HandedNessText.text = "Left-handed";
        }
    }

    public void OnMovementItemButtonClicked()
    {
        if (GlobalData.SelectedMovementItem == GlobalData.MovementItems.Grapple)
        {
            //GlobalData.SelectedMovementItem = GlobalData.MovementItems.JetPack;
            //MovementNameText.text = "Jetpack";
            Debug.Log("Disabled movement swapping for now, until we have a 2nd move item.");
        }
        else
        {
            GlobalData.SelectedMovementItem = GlobalData.MovementItems.Grapple;
            MovementNameText.text = "Grapple";
        }
    }

    public void OnWeaponButtonClicked()
    {
        if( GlobalData.SelectedCombatItem == GlobalData.CombatItems.FireBall )
        {
            GlobalData.SelectedCombatItem = GlobalData.CombatItems.Scatter;
            WeaponNameText.text = "Scatter";
        }
        else
        {
            GlobalData.SelectedCombatItem = GlobalData.CombatItems.FireBall;
            WeaponNameText.text = "Fireball";
        }
    }

    public void OnLevelSelectNextButtonClicked()
    {
        SelectedScene++;
        SelectedScene %= SceneNames.Count;
        UpdateSelectedScene();
    }

    public void OnLevelSelectPrevButtonClicked()
    {
        SelectedScene--;
        if( SelectedScene < 0 )
        {
            SelectedScene += SceneNames.Count;
        }
        UpdateSelectedScene();
    }

    public void UpdateSelectedScene()
    {
        LevelNameText.text = SceneNames[SelectedScene];
    }
}
