
using UnityEngine;

public class PipeRepair : MonoBehaviour
{
    [Header("Repair Sequence Settings")]
    private int currentStep = 1;
    private const int totalSteps = 3;
    private bool isRepaired = false;

    public bool IsRepaired => isRepaired;

    // Execute the next repair step
    public bool ExecuteRepairStep(int playerStepChoice)
    {
        if (isRepaired)
            return false;

        if (playerStepChoice == currentStep)
        {
            Debug.Log("Repair step " + currentStep + " completed successfully.");

            currentStep++;

            if (currentStep > totalSteps)
            {
                isRepaired = true;
                Debug.Log("Leak fully repaired! Water saved and area restored.");
            }

            return true;
        }
        else
        {
            Debug.Log("Incorrect sequence order! Resetting repair steps.");
            currentStep = 1;

            return false;
        }
    }
}
