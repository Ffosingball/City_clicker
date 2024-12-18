using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Balance
{
    static private int balance=0;
    static private float multiplier=1;
    static private int adder=0;

    static public int getBalance(){
        return balance;
    }

    static public void updateBalance(){
        balance += (int)((1+adder)*multiplier);
        UIManager.updateText(balance);
    }

    static public void updateBalance(int withdraw){
        balance -= withdraw;
        UIManager.updateText(balance);
    }

    static public double getMultiplier(){
        return multiplier;
    }

    static public void updateMultiplier(float increase){
        if(increase>0)
            multiplier += increase;
        Debug.Log(multiplier);
    }

    static public int getAdder(){
        return adder;
    }

    static public void updateAdder(float increase){
        if(increase>0)
            adder += (int)increase;
    }
}
