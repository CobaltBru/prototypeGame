using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SC_UIBuilder : MonoBehaviour
{
    private void OnEnable() 
    {
    VisualElement root = GetComponent<UIDocument>().rootVisualElement;

    Button button1 = root.Q<Button>("button1");

    button1.clicked += () => Debug.Log("button 1 test complete");
    }
}
