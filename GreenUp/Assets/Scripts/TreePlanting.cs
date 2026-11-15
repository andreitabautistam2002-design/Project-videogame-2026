
using System.Collections;
using UnityEngine;

public class TreePlanting : MonoBehaviour
{
    [Header("Planting Settings")]
    [SerializeField] private string requiredSoilType;
    [SerializeField] private bool isPlanted = false;

    [Header("Growth Animation")]
    [SerializeField] private float growthScale = 1.25f;
    [SerializeField] private float growthDuration = 0.8f;

    private Vector3 originalScale;

    [Header("Audio Feedback")]
    [SerializeField] private AudioSource plantingAudio;
    [SerializeField] private AudioClip successPlantSound;
    [SerializeField] private AudioClip growthSound;
    [SerializeField] private AudioClip errorPlantSound;

    private void Awake()
    {
        originalScale = transform.localScale;

        if (plantingAudio == null)
        {
            plantingAudio = gameObject.AddComponent<AudioSource>();
            plantingAudio.playOnAwake = false;
            plantingAudio.loop = false;
            plantingAudio.spatialBlend = 0f;
        }
    }

    public void TryPlantTree(string playerSelectedSpecies, string zoneSoilType)
    {
        if (isPlanted)
        {
            Debug.Log("A tree has already been planted in this area.");
            return;
        }

        if (zoneSoilType == requiredSoilType)
        {
            isPlanted = true;

            Debug.Log("Correct plant species selected! Tree planted successfully.");

            PlaySound(successPlantSound);
            StartCoroutine(GrowPlant());
        }
        else
        {
            Debug.Log("Incompatible plant species for this soil type! Action failed.");

            PlaySound(errorPlantSound);
        }
    }

    private IEnumerator GrowPlant()
    {
        Vector3 targetScale = originalScale * growthScale;
        float elapsedTime = 0f;
        
        PlaySound(growthSound);

        while (elapsedTime < growthDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = Mathf.Clamp01(elapsedTime / growthDuration);

            transform.localScale = Vector3.Lerp(
                originalScale,
                targetScale,
                progress
            );

            yield return null;
        }

        transform.localScale = targetScale;
    }

    private void PlaySound(AudioClip clip)
    {
        if (plantingAudio != null && clip != null)
        {
            plantingAudio.PlayOneShot(clip);
        }
    }
}
