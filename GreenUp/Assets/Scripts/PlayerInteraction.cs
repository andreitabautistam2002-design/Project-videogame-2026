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

    private readonly Color32 plasticColor = new Color32(70, 70, 70, 255);
    private readonly Color32 organicColor = new Color32(46, 125, 50, 255);
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
        bool interactionPressed =
            Input.GetKeyDown(KeyCode.E)
            || Input.GetKeyDown(KeyCode.Return);

        // Waste classification
        if (nearbyWasteBin != null && interactionPressed)
        {
            if (nearbyWasteBin.gameObject.name.Contains("Plastic"))
            {
                nearbyWasteBin.TryDepositWaste("Plastic");

                if (!HasGameEnded())
                {
                    ShowFeedback(
                        "¡Residuo aprovechable clasificado correctamente!",
                        plasticColor
                    );
                }
            }
            else if (nearbyWasteBin.gameObject.name.Contains("Organic"))
            {
                nearbyWasteBin.TryDepositWaste("Organic");

                if (!HasGameEnded())
                {
                    ShowFeedback(
                        "¡Residuo orgánico clasificado correctamente!",
                        organicColor
                    );
                }
            }
            else
            {
                ShowFeedback(
                    "¡Contenedor no reconocido!",
                    errorColor
                );
            }
        }

        // Pipe repair
        if (nearbyPipe != null && interactionPressed)
        {
            if (!nearbyPipe.IsRepaired)
            {
                bool stepCompleted =
                    nearbyPipe.ExecuteRepairStep(currentPipeStep);

                if (HasGameEnded())
                {
                    return;
                }

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
                ShowFeedback(
                    "¡La fuga ya está reparada!",
                    pipeColor
                );
            }
        }

        // Tree planting
        if (nearbyTree != null && interactionPressed)
        {
            nearbyTree.TryPlantTree(
                "Urban Tree",
                "Urban Soil"
            );

            if (!HasGameEnded())
            {
                ShowFeedback(
                    "¡Árbol plantado correctamente!",
                    treeColor
                );
            }
        }
    }

    private bool HasGameEnded()
    {
        return GameManager.Instance != null
            && GameManager.Instance.sustainabilityScore >= 100f;
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