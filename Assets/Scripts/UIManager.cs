using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public Image img_ExperienceBar;
    public Text txt_CurrentLevel;
    public GameObject levelUpPanel;
    public List<Text> rewardSlotTexts;
    public List<Button> rewardSlotButton;

    public Text txt_CurrentHealth;
    public Text txt_MaxHealth;

    public GameObject GGPanel;
        


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        img_ExperienceBar.fillAmount = PlayerController.instance.currentExperience / PlayerController.instance.experienceNeededToLevelUp;
        //Debug.Log(PlayerController.instance.currentExperience / PlayerController.instance.experienceNeededToLevelUp);
        txt_CurrentLevel.text = PlayerController.instance.level.ToString();
        //img_ExperienceBar.transform.localScale.x += 1;

        txt_CurrentHealth.text = "HP: " + PlayerController.instance.currentHealth;
        txt_MaxHealth.text = "Max HP: "+PlayerController.instance.maxHealth;
    }
    
    //public void 
    
    
}
