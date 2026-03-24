/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;
using UnityEngine.UI;
using TMPro;

public class Ads: MonoBehaviour
{
 public string idAdv;

    public Hero script;
    
    void Start()
    {
      script = GetComponent<Hero>();
    }

    void Update()
    {
       //script.moneyText.text = script.FormatNumber(script.money) + " $";
    }

      private void OnEnable()
      {
        YG2.onRewardAdv += Rewarded;
      } 

      private void OnDisable()
      {
        YG2.onRewardAdv -= Rewarded;
      }

      private void Rewarded(string id)
      {
        if(id == idAdv)
        {
            SetReward();
             script.moneyText.text = script.FormatNumber(script.money) + " $";
        }
      } 

      public void SetReward()
      {
        int money = PlayerPrefs.GetInt("money");
        PlayerPrefs.SetInt("money", script.money * 2);
         //PlayerPrefs.SetInt("money", script.totalMoney * 2);
     // script.moneyText.text = script.FormatNumber(script.money) + " $";
     
      }

      public void ShowRewardAdv_UseCallback()
      {
        YG2.RewardedAdvShow(idAdv, () =>
        {

            SetReward();
        });
      }
   
    
}
*/