using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebbugingBehaviour : MonoBehaviour
{
    public void getTenThousand(){ Balance.updateBalance(-10000f); }

    public void getOneMillion(){ Balance.updateBalance(-1000000f); }
}
