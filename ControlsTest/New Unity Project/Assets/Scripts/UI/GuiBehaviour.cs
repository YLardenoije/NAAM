using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GuiBehaviour : MonoBehaviour
{
    [SerializeField] private GlobalData GlobalData;
    [SerializeField] private TMPro.TextMeshProUGUI HandedNessText;

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1); //load the first scene after this one ( in build order )
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
}
