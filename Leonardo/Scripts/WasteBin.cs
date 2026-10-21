using UnityEngine;

public class WasteBin : MonoBehaviour
{
    [Header("Bin Settings")]
    [SerializeField] private string acceptedWasteType; // e.g., "Plastic", "Organic", "Glass"
    [SerializeField] private AudioSource feedbackAudio;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip errorSound;

    // Method called when the player attempts to deposit collected waste
    public void TryDepositWaste(string playerWasteType)
    {
        // Compare the waste category carried by the player against the accepted type
        if (playerWasteType == acceptedWasteType)
        {
            // Log success feedback and trigger positive audio response
            Debug.Log("Correct waste classification! Increasing environmental progress.");
            PlaySound(successSound);
            
            // TODO: Increase global sustainability indicator and update UI text
        }
        else
        {
            // Log error feedback and trigger rejection audio response
            Debug.Log("Incorrect classification! Waste rejected by the bin.");
            PlaySound(errorSound);
            
            // TODO: Display accessible UI error message and return item to player
        }
    }

    // Helper method to trigger audio clips complying with WCAG / DUA guidelines
    private void PlaySound(AudioClip clip)
    {
        // Verify audio source and clip exist before playing
        if (feedbackAudio != null && clip != null)
        {
            feedbackAudio.PlayOneShot(clip);
        }
    }
}
