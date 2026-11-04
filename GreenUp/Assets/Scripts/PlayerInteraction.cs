
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    private WasteBin nearbyWasteBin;
    private PipeRepair nearbyPipe;
    private TreePlanting nearbyTree;

    private int currentPipeStep = 1;

    [Header("UI Feedback")]
    [SerializeField] private TMP_Text feedbackText;

    // Feedback colors
    private readonly Color32 plasticColor = new Color32(139, 90, 0, 255);
    private readonly Color32 paperColor = new Color32(21, 87, 153, 255);
    private readonly Color32 treeColor = new Color32(46, 125, 50, 255);
    private readonly Color32 pipeColor = new Color32(0, 105, 110, 255);
    private readonly Color32 errorColor = new Color32(183, 28, 28, 255); 

    private void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }
    }

    private void Update()
    {
        // Waste classification
        if (nearbyWasteBin != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyWasteBin.TryDepositWaste("Plastic");

            if (nearbyWasteBin.gameObject.name.Contains("Plastic"))
            {
                ShowFeedback("¡Clasificación correcta!", plasticColor);
            }
            else
            {
                ShowFeedback("¡Clasificación incorrecta!", errorColor);
            }
        }


        // Pipe repair
        if (nearbyPipe != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!nearbyPipe.IsRepaired)
            {
                bool stepCompleted = nearbyPipe.ExecuteRepairStep(currentPipeStep);

                if (!stepCompleted)
                {
                    ShowFeedback(
                        "¡Orden incorrecto! Intenta nuevamente.",
                        errorColor
                    );

                    currentPipeStep = 1;
                }
                else
                {
                    if (currentPipeStep == 3)
                    {
                        ShowFeedback(
                            "¡Fuga reparada por completo!",
                            pipeColor
                        );

                        currentPipeStep = 1;
                    }
                    else
                    {
                        ShowFeedback(
                            "¡Paso " + currentPipeStep + " completado!",
                            pipeColor
                        );

                        currentPipeStep++;
                    }
                }
            }
            else
            {
                ShowFeedback("¡La fuga ya está reparada!", pipeColor);
            }
        }


        // Tree planting
        if (nearbyTree != null && Input.GetKeyDown(KeyCode.E))
        {
            nearbyTree.TryPlantTree("Urban Tree", "Urban Soil");

            ShowFeedback("¡Árbol plantado correctamente!", treeColor);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null)
        {
            nearbyWasteBin = wasteBin;
        }

        PipeRepair pipe = other.GetComponent<PipeRepair>();

        if (pipe != null)
        {
            nearbyPipe = pipe;
            currentPipeStep = 1;
        }

        TreePlanting tree = other.GetComponent<TreePlanting>();

        if (tree != null)
        {
            nearbyTree = tree;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        WasteBin wasteBin = other.GetComponent<WasteBin>();

        if (wasteBin != null && wasteBin == nearbyWasteBin)
        {
            nearbyWasteBin = null;
        }

        PipeRepair pipe = other.GetComponent<PipeRepair>();

        if (pipe != null && pipe == nearbyPipe)
        {
            nearbyPipe = null;
            currentPipeStep = 1;
        }

        TreePlanting tree = other.GetComponent<TreePlanting>();

        if (tree != null && tree == nearbyTree)
        {
            nearbyTree = null;
        }
    }

    private void ShowFeedback(string message, Color color)
    {
        if (feedbackText != null)
        {
            feedbackText.text = message;
            feedbackText.color = color;
        }
    }
}
