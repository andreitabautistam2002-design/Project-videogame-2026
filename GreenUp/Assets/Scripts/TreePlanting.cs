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

    // Method called when the player interacts with a designated planting zone
    public void TryPlantTree(string playerSelectedSpecies, string zoneSoilType)
    {
        // Prevent planting multiple trees in the same spot
        if (isPlanted)
        {
            Debug.Log("A tree has already been planted in this area.");
            return;
        }

        // Validate if the selected plant species matches the soil requirements
        if (zoneSoilType == requiredSoilType)
        {
            isPlanted = true;
            Debug.Log("Correct plant species selected! Tree planted successfully.");
            PlaySound(successPlantSound);
            
            // TODO: Instantiate tree visual asset and update sustainability progress
        }
        else
        {
            Debug.Log("Incompatible plant species for this soil type! Action failed.");
            PlaySound(errorPlantSound);
            
            // TODO: Show accessible UI hint regarding soil conditions
        }
    }

    // Helper method to play audio feedback following accessibility requirements
    private void PlaySound(AudioClip clip)
    {
        if (plantingAudio != null && clip != null)
        {
            plantingAudio.PlayOneShot(clip);
        }
    }
}
