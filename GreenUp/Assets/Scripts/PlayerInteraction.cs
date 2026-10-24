using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private WasteBin nearbyWasteBin;
    private PipeRepair nearbyPipe;
    private int currentPipeStep = 1;

    private void Update()
    {
        // Deposit plastic waste when the player is near a waste bin
        if (nearbyWasteBin != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyWasteBin.TryDepositWaste("Plastic");
        }

        // Advance through the pipe repair sequence
        if (nearbyPipe != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyPipe.ExecuteRepairStep(currentPipeStep);

            currentPipeStep++;

            if (currentPipeStep > 3)
            {
                currentPipeStep = 1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Detect nearby waste bin
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null)
        {
            nearbyWasteBin = wasteBin;
        }

        // Detect nearby leaking pipe
        PipeRepair pipe = other.GetComponent<PipeRepair>();

        if (pipe != null)
        {
            nearbyPipe = pipe;
            currentPipeStep = 1;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Clear waste bin reference when leaving its interaction area
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null && wasteBin == nearbyWasteBin)
        {
            nearbyWasteBin = null;
        }

        // Clear pipe reference when leaving its interaction area
        PipeRepair pipe = other.GetComponent<PipeRepair>();

        if (pipe != null && pipe == nearbyPipe)
        {
            nearbyPipe = null;
            currentPipeStep = 1;
        }
    }
}