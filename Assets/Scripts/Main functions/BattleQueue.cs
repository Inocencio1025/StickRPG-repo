using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleQueue : MonoBehaviour
{
    private Queue<Unit> battleQueue;
    private List<Unit> allUnits;
    private const int maxQueue = 15;
    private const int tickMax = 100; //if change tickmax, also change in units


    //adds to battle queue til max
    public void FillUpQueue()
    {
        while (battleQueue.Count < maxQueue)
            TickAllUnits();
    }

    public void setUnitsForQueue(List<Unit> playerUnits, List<Unit> enemyUnits)
    {
        GetAllUnits(playerUnits, enemyUnits);
    }

    public Unit NextUnit()
    {
        Unit unit = battleQueue.Dequeue();
        return unit;
    }

    public void TickAllUnits()
    {
        //list for if two units overtick at same time
        List<Unit> tempListForSorting = new List<Unit>();


        //ticks units and adds to tempList for sorting
        //before adding to battleQueue
        foreach (Unit unit in allUnits)
        {
            unit.TickUnit();

            if (unit.ticks > tickMax)
            {
                tempListForSorting.Add(unit);
                unit.SetBackTicks();        
            }
        }


        //sorts templist and then adds to queue
        if (tempListForSorting.Count > 0) {
            tempListForSorting.Sort((a, b) => b.GetTicks().CompareTo(a.GetTicks()));
            foreach (Unit unit in tempListForSorting)
            {
                battleQueue.Enqueue(unit);

                //for testing
                Debug.Log(unit.getName() + " has been add to the Battle Queue\n" +
                    "\t with  " + unit.GetTicks() + " over ticks!");
            }
        }
    }

    //combines players and enemies into one list
    private void GetAllUnits(List<Unit> playerUnits, List<Unit> enemyUnits)
    {
        foreach (Unit unit in playerUnits)
            allUnits.Add(unit);
        foreach (Unit unit in enemyUnits)
            allUnits.Add(unit);

        //reset ticks
        foreach (Unit unit in allUnits)
            unit.ResetTicks();
    }

    public Unit PeekNext()
    {
        return battleQueue.Peek();
    }

    public void ClearQueue()
    {
        battleQueue.Clear();
        allUnits.Clear();
    }
}
