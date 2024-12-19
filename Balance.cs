using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class Balance
{
    static private string[] powersOfTen = {"K","M","B","T","Q"};
    static private float balance=0;
    static private float multiplier=1;
    static private float adder=0;

    static public float getBalance(){
        return balance;
    }

    static public float updateBalance(){
        float increaseBy=(1+adder)*multiplier;
        balance += increaseBy;
        UIManager.updateText((float)Math.Round(balance));
        return increaseBy;
    }

    static public void updateBalance(int withdraw){
        balance -= withdraw;
        UIManager.updateText((float)Math.Round(balance));
    }

    static public float getMultiplier(){
        return multiplier;
    }

    static public void updateMultiplier(float increase){
        if(increase>0)
            multiplier += increase;
        //Debug.Log(multiplier);
    }

    static public float getAdder(){
        return adder;
    }

    static public void updateAdder(float increase){
        if(increase>0)
            adder += increase;
    }


    static public string outputCostCorrectly(float number){
        int exponent=0;
        while(number>=10){
            number/=10;
            exponent++;
        }

        if(exponent<3)
            return Math.Round(number*(float)Math.Pow(10,exponent)).ToString();
        else if(exponent<6)
            return (float)Math.Round(number,2)+powersOfTen[0];
        else if(exponent<9)
            return (float)Math.Round(number,2)+powersOfTen[1];
        else if(exponent<12)
            return (float)Math.Round(number,2)+powersOfTen[2];
        else if(exponent<15)
            return (float)Math.Round(number,2)+powersOfTen[3];
        else if(exponent<18)
            return (float)Math.Round(number,2)+powersOfTen[4];
        else
            return Math.Round(number,2).ToString();
    }
}
