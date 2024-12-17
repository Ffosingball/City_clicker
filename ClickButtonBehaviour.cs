using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonBehaviour : MonoBehaviour
{
    public Text coinsNum;


    // Start is called before the first frame update
    void Start()
    {
        coinsNum.text = "0";
    }

    // Update is called once per frame
    /*void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            increaseBalance();
        }
    }*/


    public void increaseBalance(){
        Balance.updateBalance();
        coinsNum.text=""+Balance.getBalance();
    }
}
