//GreenUp UI Controller
//Updates the energy indicators ans sustainability bar.

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GreenUpUI : MonoBehaviour
{
  [Header("Energy UI")]
  [SerializeField] private Image[] energyBolts =new Image[3];

  [SerializeField] private Color activeBoltColor = Color.white;
  [SerializeField] private Color inactiveBoltColor = new Color(0.45f, 0.45f, 0.45f,1f);

  [Header("Sustainability UI")]
  [SerializeField] private Image sustainabilityFill;
  [SerializeField] private TMP_Text sustainabilityText;

  private int lastEnergy=-1;
  private float lastSustainability=-1f;

  private void Update()
    {
    GameManager gameManager = GameManager.Instance;
    
    if (gameManager ==null)
      {
      return;}

      //Read and limit the current game values. 
      int currentEnergy = Mathf.Clamp(gameManager.playerEnergy,0,3);
      float currentSustainability = Mathf.Clamp(
        gameManager.sustainabilityScore,0f,100f
      );

      //Update energy UI only when the value changes.
      if (currentEnergy != lastEnergy)
        {
        UpdateEnergyUI(currentEnergy);
        lastEnergy = currentEnergy;
        }

        //Update sustainability UI only when the value changes. 
        if(!Mathf.Approximately(currentSustainability, lastSustainability))
        {
          UpdateSustainabilityUI(currentSustainability);
          lastSustainability=currentSustainability;
        }

      }

      private void UpdateEnergyUI(int energy)
      {
        if (energyBolts == null || energyBolts.Length !=3)
        {
          Debug.LogWarning("GreenUPUI: Assing exactly three energy indicators.");
          return;
        }
        
        for(int i=0; i < energyBolts.Length; i++)
        {
          if(energyBolts[i] !=null)
          {
            energyBolts[i].color = i < energy
              ? activeBoltColor
              : inactiveBoltColor;
          }
        }
      }

  //Update the sustainability bar and percentage text. 
  private void UpdateSustainabilityUI(float sustainability)
    {
      if (sustainabilityFill !=null)
      {
        sustainabilityFill.fillAmount = sustainability / 100f; 
      }

      if (sustainabilityText !=null)
      {
        sustainabilityText.text = Mathf.RoundToInt(sustainability) + "%";
      }
      
    }
  
}
