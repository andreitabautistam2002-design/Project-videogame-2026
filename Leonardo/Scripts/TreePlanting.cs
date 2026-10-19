using UnityEngine;

public class TreePlanting : MonoBehaviour
{
    [Header("Planting Settings")]
    [SerializeField] private string requiredSoilType; // e.g., "Humid Soil", "Dry Soil", "Urban Soil"
    [SerializeField] private bool isPlanted = false;
    
    [Header("Audio Feedback")]
    [SerializeField] private AudioSource plantingAudio;
    [SerializeField] private AudioClip successPlantSound;
    [SerializeField] private AudioClip errorPlantSound;

    // Method called when the player presses 'E' on a planting zone
    public void TryPlantTree(string playerSelectedSpecies, string zoneSoilType)
    {
        if (isPlanted)
        {
            Debug.Log("A tree is already planted in this zone.");
            return;
        }

        // Check if the selected tree species matches the soil requirements of the zone
        if (zoneSoilType == requiredSoilType)
        {
            isPlanted = true;
            Debug.Log("Correct species planted! Environmental progress increasing.");
            PlaySound(successPlantSound);
            
            // TODO: Trigger visual change (spawn tree sprite/model and make it grow)
        }
        else
        {
            Debug.Log("Incorrect species for this soil type! The plant cannot grow here.");
            PlaySound(errorPlantSound);
            
            // TODO: Trigger UI warning message, allow player to select another species
        }
    }

    // Helper method to play audio feedback (WCAG / DUA multimodal compliance)
    private void PlaySound(AudioClip clip)
    {
        if (plantingAudio != null && clip != null)
        {
            plantingAudio.PlayOneShot(clip);
        }
    }
}