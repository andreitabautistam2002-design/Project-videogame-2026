using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private WasteBin nearbyWasteBin;

    private void Update()
    {
        if (nearbyWasteBin != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyWasteBin.TryDepositWaste("Plastic");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null)
        {
            nearbyWasteBin = wasteBin;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null && wasteBin == nearbyWasteBin)
        {
            nearbyWasteBin = null;
        }
    }
}
