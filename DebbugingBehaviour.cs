using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebbugingBehaviour : MonoBehaviour
{
    public void getTenThousand(){ Balance.increaseBalance(10000f); }

    public void getOneMillion(){ Balance.increaseBalance(1000000f); }
}
