using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

static public class Balance
{
    static private string[] powersOfTen = {"K","M","B","T","Q"};
    static private float balance=0, earnedByClicks=0, earnedPassively=0;
    static private float multiplier=1;
    static private float amountToMultiply=0;
    static private float adder=0;

    static public float getBalance(){
        return balance;
    }

    static public float getEarnedByClicks(){
        return earnedByClicks;
    }

    static public float getEarnedPassively(){
        return earnedPassively;
    }

    static public void clearStatistics(){
        earnedByClicks=0;
        earnedPassively=0;
    }

    static public float updateBalance(){
        float increaseBy=(1+adder)*(float)Math.Pow(multiplier, amountToMultiply);
        balance += increaseBy;
        earnedByClicks += increaseBy;
        UIManager.updateText((float)Math.Round(balance));
        return increaseBy;
    }

    static public void updateBalance(float withdraw){
        balance -= withdraw;
        //Debug.Log(withdraw+"; "+balance);
        UIManager.updateText((float)Math.Round(balance));
    }

    static public void increaseBalance(float income)
    {
        if(income>0)
        {
            balance+=income;
            earnedPassively+=income;
        }

        UIManager.updateText((float)Math.Round(balance));
    }

    static public float getAmountToMultiply(){
        return amountToMultiply;
    }

    static public void setAmountToMultiply(float value){
        amountToMultiply=value;
    }

    static public void setMultiplier(float value){
        multiplier=value;
    }

    static public void updateMultiplier(){
        amountToMultiply++;
    }

    static public float getAdder(){
        return adder;
    }

    static public void setAdder(float value){
        adder=value;
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
            return (float)Math.Round(number*Math.Pow(10,exponent-3),5-exponent)+powersOfTen[0];
        else if(exponent<9)
            return (float)Math.Round(number*Math.Pow(10,exponent-6),8-exponent)+powersOfTen[1];
        else if(exponent<12)
            return (float)Math.Round(number*Math.Pow(10,exponent-9),11-exponent)+powersOfTen[2];
        else if(exponent<15)
            return (float)Math.Round(number*Math.Pow(10,exponent-12),14-exponent)+powersOfTen[3];
        else if(exponent<18)
            return (float)Math.Round(number*Math.Pow(10,exponent-15),17-exponent)+powersOfTen[4];
        else
            return Math.Round(number,2).ToString();
    }
}
