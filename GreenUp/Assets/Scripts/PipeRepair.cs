
using UnityEngine;

public class PipeRepair : MonoBehaviour
{
    [Header("Repair Sequence Settings")]
    private int currentStep = 1;
    private const int totalSteps = 3;
    private bool isRepaired = false;

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer pipeRenderer;
    [SerializeField] private Sprite repairedPipeSprite;

    [Header("Audio Feedback")]
    [SerializeField] private AudioSource repairAudio;
    [SerializeField] private AudioClip repairStepSound;
    [SerializeField] private AudioClip repairCompleteSound;

    public bool IsRepaired => isRepaired;

    private void Awake()
    {
        if (repairAudio == null)
        {
            repairAudio = GetComponent<AudioSource>();

            if (repairAudio == null)
            {
                repairAudio = gameObject.AddComponent<AudioSource>();
            }
        }

        repairAudio.playOnAwake = false;
        repairAudio.loop = false;
        repairAudio.spatialBlend = 0f;
    }

    public bool ExecuteRepairStep(int playerStepChoice)
    {
        if (isRepaired)
            return false;

        if (playerStepChoice == currentStep)
        {
            Debug.Log("Repair step " + currentStep + " completed successfully.");

            if (currentStep == totalSteps)
            {
                PlayRepairSound(repairCompleteSound);
            }
            else
            {
                PlayRepairSound(repairStepSound);
            }

            currentStep++;

            if (currentStep > totalSteps)
            {
                isRepaired = true;

                if (pipeRenderer != null && repairedPipeSprite != null)
                {
                    pipeRenderer.sprite = repairedPipeSprite;
                }

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

    private void PlayRepairSound(AudioClip clip)
    {
        if (repairAudio != null && clip != null)
        {
            repairAudio.PlayOneShot(clip);
        }
    }
}
