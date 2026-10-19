using UnityEngine;

public class WasteBin : MonoBehaviour
{
    [Header("Bin Settings")]
    [SerializeField] private string acceptedWasteType; // e.g., "Plastic", "Organic", "Glass"
    [SerializeField] private AudioSource feedbackAudio;
    [SerializeField] private AudioClip successSound;
    [SerializeField] private AudioClip errorSound;

    // Detect when the player interacts with the correct or incorrect bin
    public void TryDepositWaste(string playerWasteType)
    {
        // Check if the waste matches the bin's category
        if (playerWasteType == acceptedWasteType)
        {
            Debug.log("Correct classification! Increasing environmental progress.");
            PlaySound(successSound);
            // TODO: Trigger UI success message and increase sustainability score
        }
        else
        {
            Debug.log("Incorrect classification! Waste rejected by the bin.");
            PlaySound(errorSound);
            // TODO: Trigger UI error message, return waste to player
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (feedbackAudio != null && clip != null)
        {
            feedbackAudio.PlayOneShot(clip);
        }
    }
}