using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private WasteBin nearbyWasteBin;
    private PipeRepair nearbyPipe;
    private TreePlanting nearbyTree;

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

        // Plant a tree when the player is near a planting zone
        if (nearbyTree != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyTree.TryPlantTree("Urban Tree", "Urban Soil");
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

        // Detect nearby planting zone
        TreePlanting tree = other.GetComponent<TreePlanting>();

        if (tree != null)
        {
            nearbyTree = tree;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Clear waste bin reference
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null && wasteBin == nearbyWasteBin)
        {
            nearbyWasteBin = null;
        }

        // Clear pipe reference
        PipeRepair pipe = other.GetComponent<PipeRepair>();

        if (pipe != null && pipe == nearbyPipe)
        {
            nearbyPipe = null;
            currentPipeStep = 1;
        }

        // Clear tree reference
        TreePlanting tree = other.GetComponent<TreePlanting>();

        if (tree != null && tree == nearbyTree)
        {
            nearbyTree = null;
        }
    }
}