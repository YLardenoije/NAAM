using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeDisplay : MonoBehaviour
{
    
    private void OnEnable()
    {
        GetComponent<Text>().text = "You took: " + Time.timeSinceLevelLoad.ToString() + " seconds";
    }
}
