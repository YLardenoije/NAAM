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
            HandedNessText.text = "Grapple";
        }
        else
        {
            HandedNessText.text = "Jetpack";
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
            GlobalData.SelectedMovementItem = GlobalData.MovementItems.JetPack;
            HandedNessText.text = "Jetpack";
        }
        else
        {
            GlobalData.SelectedMovementItem = GlobalData.MovementItems.Grapple;
            HandedNessText.text = "Grapple";
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
