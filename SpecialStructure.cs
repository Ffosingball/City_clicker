using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialStructure : Structure
{
    private float period, passiveIncome;

    public SpecialStructure(GameObject structure, int x, int y, string type, float period, float passiveIncome) : base(structure, x, y, type)
    {
        this.period = period;
        this.passiveIncome = passiveIncome;
    }
}
