using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebbugingBehaviour : MonoBehaviour
{
    public Text earnedByClicks, earnedPassively;

    public void getTenThousand(){ Balance.increaseBalance(10000f); }

    public void getOneMillion(){ Balance.increaseBalance(1000000f); }

    public void clearStats(){ Balance.clearStatistics(); }

    private void Update()
    {
        earnedByClicks.text = Balance.outputCostCorrectly(Balance.getEarnedByClicks());
        earnedPassively.text = Balance.outputCostCorrectly(Balance.getEarnedPassively());
    }
}
