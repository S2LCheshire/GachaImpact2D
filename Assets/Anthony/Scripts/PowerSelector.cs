using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSelector : MonoBehaviour
{
    const int MAX_BREAK_POWER = 99;
    const int MIN_BREAK_POWER = 0;
    int breakPower = 0;

    public void ChangeBreakPower(int change)
    {
        breakPower = (breakPower + change + MAX_BREAK_POWER + 1) %(MAX_BREAK_POWER+1);
    }

    public void AddBreakPower()
    {
        ChangeBreakPower(1);
    }
    public void ReduceBreakPower()
    {
        ChangeBreakPower(-1);
    }
    public int GetBreakPower()
    {
        return breakPower;
    }

}
