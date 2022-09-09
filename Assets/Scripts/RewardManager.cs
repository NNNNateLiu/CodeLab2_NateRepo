using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour
{
    /* Reward Types 4
     1, restore health 50%
     2, gain/upgrade item1 - AOE damage
     3, gain/upgrade item2 - increase max HP
     4, upgrade normal attack - attack rate/number
     */
    public List<int> rewardIndexPool = new List<int>();
    public List<int> chosenRewardPool = new List<int>();

    public static RewardManager instance;

    private void Awake()
    {
        instance = this;
    }


    private void RandomRewardsOutOfTwo()
    {
        List<int> chosenRewards = new List<int>{0,1,2,3};
        
        for (int i = 0; i <= 1; i++)
        {
            chosenRewards.Remove(chosenRewards[Random.Range(0, chosenRewards.Count)]);
        }

        chosenRewardPool = chosenRewards;
    }

    public void GenerateRewardOnUI()
    {
        int currentIndex = 0;
        foreach (var ChosenReward in chosenRewardPool)
        {
            Debug.Log("ChooseReward:"+ChosenReward);
            switch (ChosenReward)  
            {                                                  
                case 0:
                    UIManager.instance.rewardSlotTexts[currentIndex].text = "Restore Health";
                    break;                                     
                case 1:
                    UIManager.instance.rewardSlotTexts[currentIndex].text = "Gain/Upgrade AOE Damage Ability";    
                    break;                                     
                case 2:
                    UIManager.instance.rewardSlotTexts[currentIndex].text = "Increase 20% Max Health";    
                    break;                                     
                case 3:
                    UIManager.instance.rewardSlotTexts[currentIndex].text = "Upgrade Basic Attack";    
                    break;                                     
            }
            currentIndex++;
        }
    }
    
    public void ChooseReward(int chosenRewardsPoolIndex)
    {
        RandomRewardsOutOfTwo();
        switch (chosenRewardPool[chosenRewardsPoolIndex])
        {
            case 0:
                break;
            case 1:
                PlayerController.instance.killZone.SetActive(true);
                break;
            case 2:
                break;
            case 3:
                break;
        }
        UIManager.instance.levelUpPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void LevelUp()
    {
        UIManager.instance.levelUpPanel.SetActive(true);  
        RandomRewardsOutOfTwo();                          
        GenerateRewardOnUI();                             
    }
}
