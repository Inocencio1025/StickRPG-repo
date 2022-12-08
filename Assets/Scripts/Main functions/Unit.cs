using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;
	public int maxHP;
	public int currentHP;
	public int damage;
	public int specialDamage;
	public int speed;
	public bool isPlayerUnit;

	//for turn order
	public int ticks;
	private const int tickMax = 100;
	public int speedMultiplier;
	public int turnsActive;

	//for turn decisions
	public int specAttRate;
	public int counterRate;

	//status trackers
	public bool counterState;
	public int poisonNum = 0;


	public Animator animator;
	public ParticleSystem effects;


	//constructors
	public Unit()
	{
		counterState = false;
		ticks = 0;
		speedMultiplier = 1;
		currentHP = maxHP;
		turnsActive = 0;
	}
	//setters
	public void setStrength(int str)
    {
		damage = str;
    }
	//getters
	public string getName()
    {
		return name;
    }
	public int GetTicks()
    {
		return ticks;
    }


	private void Awake()
	{
		effects = GetComponent<ParticleSystem>();
	}


	//methods for ticks in BattleQueue
	public void TickUnit()
    {
		ticks += speed;
    }

	public void SetBackTicks() //called when ticks > 100
	{
		ticks -= tickMax;
	}
	public void ResetTicks() //called at start of each battle
	{
		ticks = 0;
		ResetActiveTurns();
	}
	public void AddActiveTurn()
    {
		this.turnsActive++;
    }
	private void ResetActiveTurns()
    {
		this.turnsActive = 0;
    }





	//methods to battle

	//abstract attack method
	public abstract void TakeTurn();


	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
			return true;
		else
			return false;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public void PlayEffect()
	{
		effects.Play();
	}


	public void RemoveEnemy()
	{
		
		Destroy(gameObject);
	}


}
