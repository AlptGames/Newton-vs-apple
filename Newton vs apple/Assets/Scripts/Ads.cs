using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;


public class Ads : MonoBehaviour
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
            
        }
      } 

      public void SetReward()
      {

      //Hero.currentLives = 1;

      }

      public void ShowRewardAdv_UseCallback()
      {
        YG2.RewardedAdvShow(idAdv, () =>
        {

            SetReward();
        });
      }
   
    
}
