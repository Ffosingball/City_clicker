using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class Balance
{
    static private int balance=0;
    static private double multiplier=1;
    static private int adder=0;

    static public int getBalance(){
        return balance;
    }

    static public void updateBalance(){
        balance += (int)((1+adder)*multiplier);
    }

    static public double getMultiplier(){
        return multiplier;
    }

    static public void updateMultiplier(int increase){
        if(increase>0)
            multiplier += increase;
    }

    static public int getAdder(){
        return adder;
    }

    static public void updateAdder(int increase){
        if(increase>0)
            adder += increase;
    }
}
