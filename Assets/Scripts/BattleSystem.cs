using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


using TMPro;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	public GameManager gameManager;

	public BattleState state;
	public bool jackInParty;
	public bool kamiInParty;
	public bool riverInParty;
	public bool ltInParty;

	public List<GameObject> playerInParty = new List<GameObject>(4);
	public List<GameObject> enemiesInBattle;  ///enemies to be loaded in

	List<GameObject> playerGO = new List<GameObject>(4);
	List<GameObject> enemyGO = new List<GameObject>(4);

	// holds prefabs to spawn in	
	public List<GameObject> playerPrefabs;
	public List<GameObject> enemyPrefab;

	//transform is used for coodinates of battlestations
	public List<Transform> playerBattleStation;
	public List<Transform> enemyBattleStation;

	//holds unit in battle
	public List<Unit> playerUnit;
	public List<Unit> enemyUnit;
	

	//list use to hold turn order
	public List<int> BattleTurns;

	// for UI
	public GameObject BattleCanvas;
	public List<Button> attackBtn;
	public List<Button> itemBtn;
	public List<Image> TurnImage;

	public List<BattleHud> playerHUD;
	public List<EnemyBattleHud> enemyHUD;

	

	//for targeting
	Unit currentTarget;
	Animator targetAnimator;
	Animator selected;
	public List<Animator> playerBattleStationAnimator;
	int targetNum;




	// others

	//public AudioSource SwordAttackEffect;
	public bool isDead;




	


	// Start is called before the first frame update
	void Awake()
	{
		LoadPlayers();

		//StartCoroutine(SetupBattle()); //Coroutine for delay
		

	}


	public IEnumerator SetupBattle()   
	{
	
		state = BattleState.START;

		foreach (GameObject obj in playerGO)
			obj.SetActive(true);

		LoadEnemies();

		BattleCanvas.SetActive(true);

		//disable all buttons
		foreach (Button abtn in attackBtn)
			abtn.interactable = false;
		foreach (Button ibtn in itemBtn)
			ibtn.interactable = false;

		GenerateList();
		

		Debug.Log("The Battle Has Begun");
		yield return new WaitForSeconds(2f);

		currentTarget = enemyUnit[0];
		targetAnimator = enemyUnit[0].animator;
		targetNum = 0;
		selected = enemyBattleStation[0].GetComponent<Animator>();
		selected.SetBool("selected", true);

		
		Battle();

	}


	void Battle()
	{
		SetTurnImages();

		do
		{
			NextTurn();   //adds activeTurns to units
			UpdateList();  //removes first in list battleturns
			
		} while (BattleTurns[0] < 4);  // loop until an enemy turn, allows multiple characters to go at once

		EndPhase();

	}

	void NextTurn()
    {
        switch (BattleTurns[0])
        {
			case 0:
				attackBtn[0].interactable = true;
				itemBtn[0].interactable = true;
				playerUnit[0].turnsActive++;
				
				break;

			case 1:
				attackBtn[1].interactable = true;
				itemBtn[1].interactable = true;
				playerUnit[1].turnsActive++;


				break;
			case 2:
				attackBtn[2].interactable = true;
				itemBtn[2].interactable = true;
				playerUnit[2].turnsActive++;


				break;
			case 3:
				attackBtn[3].interactable = true;
				itemBtn[3].interactable = true;
				playerUnit[3].turnsActive++;


				break;



			case 4:
				enemyUnit[0].turnsActive++;
                break;
			case 5:
				enemyUnit[1].turnsActive++;
				break;
			case 6:
				enemyUnit[2].turnsActive++;
				break;
			case 7:
				enemyUnit[3].turnsActive++;
				
				break;
		}

		



	}

	void EndPhase()
    {
		if (PlayerTurnDone())
		{
			SetTurnImages();
			selected.SetBool("selected", false); 
			
			StartCoroutine(EnemyTurn());
		}

     
	}

	void UpdateList()
    {
		BattleTurns.RemoveAt(0);

		GenerateList();
		

	}
	void GenerateList()
    {
		while (BattleTurns.Count < 10)
			TickUnits();
		/*foreach (int num in BattleTurns) 
			Debug.Log(num);
		Debug.Log("---------------"); */
    }


	void TickUnits()
	{

		int u = 0; //to add to queue


		foreach (Unit unit in playerUnit)
		{
			if (unit.currentHP >= 0)
			{
				unit.ticks += unit.speed * unit.speedMultiplier;
				if (unit.ticks >= 100)
				{

					unit.ticks -= 100f;
					BattleTurns.Add(u);




				}
				
			}
			u++;
		}

		u = 4;

		foreach (Unit unit in enemyUnit)
		{
			if (unit.currentHP > 0)
			{
				unit.ticks += unit.speed * unit.speedMultiplier;
				if (unit.ticks >= 100)
				{

					unit.ticks -= 100f;
					BattleTurns.Add(u);

				}
			}
			u++;

		}


	}


    IEnumerator EnemyTurn()
    {
		int number = 0; // for debug
		int attnum; //decides who to attack
		int specialNum; // number to determine if to use special move

		

		foreach (Unit unit in enemyUnit) // turns off counter state
		{
			if (unit != null)
			{
				unit.animator.SetBool("counterState", false);
				unit.counterState = false;
			}
		}

		while (BattleTurns[0] > 3)
		{
			NextTurn();
			UpdateList();

		}

		foreach (Unit unit in enemyUnit)
			while (unit.turnsActive > 0)
			{
				unit.turnsActive--;
				number++;
				//picks target thats not dead or not there
				attnum = Random.Range(0, playerInParty.Count);
				do
				{
					
					specialNum = Random.Range(1, 101);
				}
				while (playerUnit[attnum].currentHP <= 0 || playerUnit[attnum] == null);



				if (specialNum < unit.specAttRate)
				{
					unit.animator.SetTrigger("special");
					yield return new WaitForSeconds(2f);


					

				} else if (specialNum < unit.counterRate)
                {
					unit.animator.SetBool("counterState", true);
					unit.counterState = true;


                } else   //Normal attacks
                {
					if (unit.damage > 0)
					{

						playerBattleStationAnimator[attnum].SetTrigger("attacked");
						unit.animator.SetTrigger("attack");
						yield return new WaitForSeconds(.5f); //For enemy attack animation to line up with health update

						isDead = playerUnit[attnum].TakeDamage(unit.damage);
						playerUnit[attnum].animator.SetTrigger("damaged");
						playerHUD[attnum].animator.SetTrigger("damagedUI");
					}

					


				}


				yield return new WaitForSeconds(1f);
				

				//update health to hud
				playerHUD[attnum].SetHP(playerUnit[attnum].currentHP);

				

				Debug.Log("Enemy attack number " + number);

				
				CheckStatus(unit);
				
			}
		
		selected.SetBool ("selected",true);
		number = 0;
		Battle();
		
	}
	
	//checks all players with turn active has taken their turn, ending plyer turn
	bool PlayerTurnDone()
	{
		int count = 0;

		foreach(Unit unit in playerUnit)
        {
			if (unit.turnsActive <= 0)
				count++;
        }

		if (playerUnit.Count == count)
		{
			Debug.Log("player turns are done");
			return true;
		}

		else 
		{
			Debug.Log("player turns are NOT done");

			return false; 
		}

	}


	
	IEnumerator PlayerAttack(Unit attackingUnit, Unit target)
	{
		attackingUnit.turnsActive--;
		//SwordAttackEffect.Play();
		yield return new WaitForSeconds(0.5f);


		if (target.counterState == false)
		{
			isDead = target.TakeDamage(attackingUnit.damage);
			// add health update and dialouge to hud here
			enemyHUD[0].SetHP(enemyUnit[0].currentHP);
			//enemyHUD[1].SetHP(enemyUnit[1].currentHP);


			

			if (isDead)
			{
				//Debug.Log("HERERERERERE!!!!!!!!!!!!!!");


				bool notDone = true;

				while (notDone)
				{
					notDone = BattleTurns.Remove(targetNum + 4);
					//Debug.Log("removed " + (targetNum + 4));

				}

				


				GenerateList();
				target.turnsActive = 0;
				targetAnimator.SetTrigger("defeated");


				selected.SetBool("selected", false);

				if (AllEnemiesDead() == false)
				{
					yield return new WaitForSeconds(1f);
					SwitchTargets();
					EndPhase();
				}

				else
				{
					StartCoroutine(EndBattle());
				}


			}
			else
			{
				targetAnimator.SetTrigger("damaged");
				yield return new WaitForSeconds(1f);

				CheckStatus(attackingUnit);

				
				EndPhase();

				//enemy turn
			}
		}
		else ///for counters
        {

			isDead = attackingUnit.TakeDamage(target.specialDamage);
			targetAnimator.SetTrigger("counter");

			int position = playerUnit.IndexOf(attackingUnit);// for hud
			yield return new WaitForSeconds(.5f);

			if (target.specialDamage > 0)
			{
				attackingUnit.animator.SetTrigger("damaged");

				playerHUD[position].animator.SetTrigger("damagedUI");
				playerBattleStationAnimator[position].SetTrigger("attacked");
			}

			
			

			
			EndPhase();

		}

		
	}


	bool AllEnemiesDead()
    {
		int count = 0;
		foreach (Unit unit in enemyUnit) // counts units dead
		{
			if (unit == null || unit.currentHP <= 0)
			{
				count++;
			}
		}

		if (count == enemyUnit.Count || enemyUnit.Count == 0)
		{   // compare to original list
			Debug.Log("all enemies dead");
			state = BattleState.WON;
			return true;
		}
		else
		{
			Debug.Log("all enemies not dead");
			return false;
		}
    }

	IEnumerator PlayerHeal(Unit healingUnit)
	{

		yield return new WaitForSeconds(1f); // executes delay

		healingUnit.Heal(5);

	
		healingUnit.turnsActive--;

		if (healingUnit.unitName == "Kami")
		{
			foreach(Unit Unit in playerUnit)
				if (Unit.name != "Kami")
					Unit.Heal(5);

			Debug.Log("The Healing Smoke Spreads");

		}

		playerHUD[0].SetHP(playerUnit[0].currentHP);
		playerHUD[1].SetHP(playerUnit[1].currentHP);
		playerHUD[2].SetHP(playerUnit[2].currentHP);
		playerHUD[3].SetHP(playerUnit[3].currentHP);

		Debug.Log("You feel better");

		yield return new WaitForSeconds(1f); // executes delay

		CheckStatus(healingUnit);
		state = BattleState.ENEMYTURN;

		EndPhase();

		
	}
    void StorePlayerInfo()
    {
		PlayerStorage.HealthStorage.Clear();

		foreach (Unit unit in playerUnit)
		{

			unit.turnsActive = 0;
			PlayerStorage.HealthStorage.Add(unit.currentHP);
			Debug.Log("stored unit");
		}
	}
	

	IEnumerator EndBattle()
	{
		
		

		if (state == BattleState.WON)
		{
			StorePlayerInfo();
			
			//update dialounge that you won
			Debug.Log("You WON");
			yield return new WaitForSeconds(2f); 

			//clean up
			foreach (GameObject obj in playerGO)
				obj.SetActive(false);
			BattleCanvas.SetActive(false);
			BattleTurns.Clear();
			enemyGO.Clear();
			enemyUnit.Clear();


			gameManager.EndBattle();

			


		}
		else if (state == BattleState.LOST)
		{
			//update dialounge that you lost
			Debug.Log("You LOST");
			yield return new WaitForSeconds(2f); // executes delay

		}

	}




	void LoadPlayers()  //Loads in party members 
	{
		
		if (jackInParty == true)
		{
			playerInParty.Add(playerPrefabs[0]);
		}

		if (kamiInParty == true)
		{
			playerInParty.Add(playerPrefabs[1]);
			
		}

		if (riverInParty == true)
		{
			playerInParty.Add(playerPrefabs[2]);
		}

		if (ltInParty == true)
		{
			playerInParty.Add(playerPrefabs[3]);
		}

		int i = 0;
		foreach (GameObject obj in playerInParty)
		{
			
			playerGO.Add(Instantiate(obj, playerBattleStation[i]));
			playerUnit.Add(playerGO[i].GetComponent<Unit>());
			playerHUD[i].SetHUD(playerUnit[i]);
			playerHUD[i].SetHP(playerUnit[i].currentHP);
			
			i++;
		}
		int queue = 0;
		if (PlayerStorage.HealthStorage.Count > 0)
			foreach (Unit unit in playerUnit)
            {
				Debug.Log("Retrieving hp");
				unit.currentHP = PlayerStorage.HealthStorage[queue];
				queue++;
            }


		foreach (GameObject obj in playerGO)
			obj.SetActive(false);



	}
	void GenerateEnemies()
    {
		int num = 0;
		int rand;
		while (num < 4)
		{
			rand = Random.Range(0, 101);
			if (rand <= 20)
				enemiesInBattle.Add(enemyPrefab[0]);

			else if (rand > 20 && rand <= 30)
				enemiesInBattle.Add(enemyPrefab[1]);

			else if (rand > 30 && rand <= 40)
				enemiesInBattle.Add(enemyPrefab[2]);

			else if (rand > 40 && rand <= 50)
				enemiesInBattle.Add(enemyPrefab[3]);



			//Debug.Log(rand);
			num++;

		}


		if (enemiesInBattle.Count == 0)
		{
			rand = Random.Range(0, enemyPrefab.Count);
			enemiesInBattle.Add(enemyPrefab[rand]);
		}
	}
	
	void LoadEnemies()
    {

		if (enemiesInBattle.Count < 1)
			GenerateEnemies();


		int i = 0;
		foreach (GameObject obj in enemiesInBattle)
		{

			enemyGO.Add(Instantiate(obj, enemyBattleStation[i]));
			enemyUnit.Add(enemyGO[i].GetComponent<Unit>());
			i++;
		}
		// could use method for all hud
		enemyHUD[0].SetHUD(enemyUnit[0]);
		//enemyHUD[1].SetHUD(enemyUnit[1]);
		enemiesInBattle.Clear();

	}
	void SetTurnImages()
	{
		for (int i = 0; i < TurnImage.Count; i++)
		{
			
			switch (BattleTurns[i])
			{
				case 0:
					TurnImage[i].GetComponent<Image>().sprite = playerUnit[0].GetComponent<Image>().sprite;
					break;
				case 1:
					TurnImage[i].GetComponent<Image>().sprite = playerUnit[1].GetComponent<Image>().sprite;
					break;
				case 2:
					TurnImage[i].GetComponent<Image>().sprite = playerUnit[2].GetComponent<Image>().sprite;
					break;
				case 3:
					TurnImage[i].GetComponent<Image>().sprite = playerUnit[3].GetComponent<Image>().sprite;
					break;
				case 4:
					TurnImage[i].GetComponent<Image>().sprite = enemyUnit[0].GetComponent<Image>().sprite;
					break;
				case 5:
					TurnImage[i].GetComponent<Image>().sprite = enemyUnit[1].GetComponent<Image>().sprite;
					break;
				case 6:
					TurnImage[i].GetComponent<Image>().sprite = enemyUnit[2].GetComponent<Image>().sprite;
					break;
				case 7:
					TurnImage[i].GetComponent<Image>().sprite = enemyUnit[3].GetComponent<Image>().sprite;
					break;
			}
		}
	}
	void CheckStatus(Unit unitChk)
	{
		if (unitChk.poisonNum > 0)
		{
			unitChk.currentHP -= unitChk.maxHP / 10;
			if (unitChk.currentHP <= 0)
				unitChk.currentHP = 1;
			Debug.Log("poison has taken effect");


			unitChk.animator.SetTrigger("damaged");

			unitChk.poisonNum--;

			CheckToTurnOffUIPoisonAnimation();

		}

		if (unitChk.turnsActive < 0)
			unitChk.turnsActive = 0;
	}

	void CheckToTurnOffUIPoisonAnimation()
    {
		int i = 0;
		foreach (Unit unit in playerUnit)
        {
			if (unit.poisonNum >= 0)
				playerHUD[i++].animator.SetBool("poisoned", false);
        }
    }

	

	public void SwitchTargets()
	{
		if (state != BattleState.WON || state != BattleState.LOST)
		{
			do
			{
				targetNum++;
				if (targetNum >= enemyUnit.Count )
					targetNum = 0;


				currentTarget = enemyUnit[targetNum];
				targetAnimator = enemyUnit[targetNum].animator;

				selected.SetBool("selected", false);
				selected = enemyBattleStation[targetNum].GetComponent<Animator>();
				selected.SetBool("selected", true);



			}
			while (enemyUnit[targetNum].currentHP <= 0 || enemyUnit[targetNum] == null);
			Debug.Log("target switched to" + targetNum);
		}
	}





	//ALL PLAYER BUTTONS
	public void OnAttackButton()
    {
		if (playerUnit[0].turnsActive <= 1)
		{
			attackBtn[0].interactable = false;
			itemBtn[0].interactable = false;
		}

		playerUnit[0].animator.SetTrigger("attack");
		StartCoroutine(PlayerAttack(playerUnit[0], currentTarget));
    }
	public void OnItemButton()
	{
		if (playerUnit[0].turnsActive <= 1)
		{
			attackBtn[0].interactable = false;
			itemBtn[0].interactable = false;
		}

		playerUnit[0].animator.SetTrigger("heal");
		StartCoroutine(PlayerHeal(playerUnit[0]));
	}

	public void OnAttackButton2()
	{
		if (playerUnit[1].turnsActive <= 1)
		{
			attackBtn[1].interactable = false;
			itemBtn[1].interactable = false;
		}

		playerUnit[1].animator.SetTrigger("attack");
		StartCoroutine(PlayerAttack(playerUnit[1], currentTarget));
	}

	public void OnItemButton2()
	{
		if (playerUnit[1].turnsActive <= 1)
		{
			attackBtn[1].interactable = false;
			itemBtn[1].interactable = false;
		}

		playerUnit[1].animator.SetTrigger("heal");
		StartCoroutine(PlayerHeal(playerUnit[1]));
	}

	public void OnAttackButton3()
	{
		if (playerUnit[2].turnsActive <= 1)
		{
			attackBtn[2].interactable = false;
			itemBtn[2].interactable = false;
		}

		playerUnit[2].animator.SetTrigger("attack");
		StartCoroutine(PlayerAttack(playerUnit[2], currentTarget));
	}
	public void OnItemButton3()
	{
		if (playerUnit[2].turnsActive <= 1)
		{
			attackBtn[2].interactable = false;
			itemBtn[2].interactable = false;
		}

		playerUnit[2].animator.SetTrigger("heal");
		StartCoroutine(PlayerHeal(playerUnit[2]));
	}
	public void OnAttackButton4()
	{
		if (playerUnit[3].turnsActive <= 1)
		{
			attackBtn[3].interactable = false;
			itemBtn[3].interactable = false;
		}

		playerUnit[3].animator.SetTrigger("attack");
		StartCoroutine(PlayerAttack(playerUnit[3], currentTarget));
	}
	public void OnItemButton4()
	{
		if (playerUnit[3].turnsActive <= 1)
		{
			attackBtn[3].interactable = false;
			itemBtn[3].interactable = false;
		}

		playerUnit[3].animator.SetTrigger("heal");
		StartCoroutine(PlayerHeal(playerUnit[3]));
	}

}


