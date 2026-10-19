using UnityEngine;

public class PipeRepair : MonoBehaviour
{
    [Header("Repair Sequence Steps")]
    private int currentStep = 1;
    private const int totalSteps = 3; // 1: Close valve, 2: Place patch, 3: Tighten nut
    private bool isRepaired = false;

    // Method called when player presses 'E' on the interactive leak point
    public void ExecuteRepairStep(int playerStepChoice)
    {
        if (isRepaired) return;

        // Verify if the player follows the correct sequential order
        if (playerStepChoice == currentStep)
        {
            Debug.Log("Step " + currentStep + " completed successfully.");
            currentStep++;

            if (currentStep > totalSteps)
            {
                isRepaired = true;
                Debug.Log("Leak fully repaired! Environment restoring.");
                // TODO: Trigger visual change (dry floor, flowers blooming)
            }
        }
        else
        {
            Debug.Log("Wrong step order! Restarting repair sequence.");
            currentStep = 1; // Reset sequence on failure
        }
    }
}