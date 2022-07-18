using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;
	public int specialDamage;
	public float speed;
	public float speedMultiplier;
	public float ticks;  //works with speed for turn order
	public int turnsActive;
	public int specAttRate;
	public int counterRate;
	public bool counterState;


	public int maxHP;
	public int currentHP;

	public int poisonNum = 0;

	
	public Animator animator;

	public ParticleSystem effects;

	private void Awake()
	{
		counterState = false;
		ticks = 0f;
		turnsActive = 0;
		speedMultiplier = 1;
		effects = GetComponent<ParticleSystem>();
		currentHP = maxHP;
	}

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
		turnsActive = 0;
		Destroy(gameObject);
	}


}
