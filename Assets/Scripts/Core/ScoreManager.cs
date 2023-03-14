using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
   [SerializeField] private TextMeshProUGUI txt_Score;
   private int score;
   
   
   public void ScoreGained(int s)
   {
       score += s;
       UpdateScoreText();
   }
   
   private void UpdateScoreText()
   {
       txt_Score.text =  score.ToString("000");
   }
}
