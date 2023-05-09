using UnityEngine;
using UnityEngine.UI;

public class UnicodeCheck : MonoBehaviour
{
    // Reference to the TextBox component
    public InputField myTextBox;

    private void Start()
    {
        // Add the OnValueChanged event listener to the TextBox
        myTextBox.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newValue)
    {
        // Check if the new value contains any Unicode characters
        if (ContainsUnicode(newValue))
        {
            // Remove any Unicode characters from the new value
            newValue = RemoveUnicode(newValue);

            // Update the TextBox with the new value
            myTextBox.text = newValue;
        }
    }

    private bool ContainsUnicode(string text)
    {
        // Check if the text contains any Unicode characters
        foreach (char c in text)
        {
            if (c > 127)
            {
                return true;
            }
        }

        return false;
    }

    private string RemoveUnicode(string text)
    {
        // Remove any Unicode characters and spaces from the text
        string result = "";

        foreach (char c in text)
        {
            if (c <= 127)
            {
                result += c;
            }
        }

        return result;
    }
}
