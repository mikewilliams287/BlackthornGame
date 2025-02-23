using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GemUIText : MonoBehaviour


{
    // The current gem count
    int gemCount;

    // Reference to the TextMeshPro component
    public TextMeshProUGUI gemsText;

    // Subscribe to the event when game object is enabled
    private void OnEnable()
    {
        Gem.OnGemCollected += IncrementGemCount;
    }

    //Unsubscribe when game object disabled
    private void OnDisable()
    {
        Gem.OnGemCollected -= IncrementGemCount;
    }

    private void Start()
    {
        // Get the TextMeshProIGUI component attached to this GameObject
        gemsText = GetComponent<TextMeshProUGUI>();

        // Initialize the gem count and update the UI
        gemCount = 0;
        UpdateGemText();
    }

    // Method to increment gem count
    public void IncrementGemCount()
    {
        // Increment gem count by 1
        Debug.Log("INCREMENT");

        gemCount++;

        // Update the TextMeshPro UI text to refelct new gem count
        UpdateGemText();
    }

    // Private "helper" method to update the UI text
    private void UpdateGemText()
    {
        // set the TextMeshPro text to display the gem count
        gemsText.text = gemCount.ToString();
    }


}
