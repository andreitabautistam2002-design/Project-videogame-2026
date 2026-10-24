using UnityEngine;

public class PipeRepair : MonoBehaviour
{
    [Header("Repair Sequence Settings")]
    private int currentStep = 1;
    private const int totalSteps = 3; // Step 1: Close valve, Step 2: Place patch, Step 3: Tighten nut
    private bool isRepaired = false;

    // Method triggered when the player interacts with a leaking pipe
    public void ExecuteRepairStep(int playerStepChoice)
    {
        // Do nothing if the leak is already fixed
        if (isRepaired) return;

        // Check if the player selected the correct step in the repair sequence
        if (playerStepChoice == currentStep)
        {
            Debug.Log("Repair step " + currentStep + " completed successfully.");
            currentStep++;

            // If all sequence steps are completed, mark pipe as fully repaired
            if (currentStep > totalSteps)
            {
                isRepaired = true;
                Debug.Log("Leak fully repaired! Water saved and area restored.");
                // TODO: Trigger visual environmental change (dry floor and blooming plants)
            }
        }
        else
        {
            // Reset sequence if player chooses an incorrect step order
            Debug.Log("Incorrect sequence order! Resetting repair steps.");
            currentStep = 1;
        }
    }
}
