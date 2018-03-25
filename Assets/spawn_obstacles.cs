using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn_obstacles : MonoBehaviour {

	public GameObject obs1, obs2, obs3, obs4, obs5, obs6;
	public List <GameObject> obs1pool, obs2pool, obs3pool, obs4pool, obs5pool, obs6pool;
	GameObject[] obs = new GameObject[6]; 
	List<GameObject>[] obspool = new List<GameObject>[6]; 
	public int poolSize; public float interval, delay;
	public Transform p1enemy, p2enemy;

	void Awake () {
		//SharedInstance = this;

		//array initialization
		obs [0] = obs1; obspool [0] = obs1pool;
		obs [1] = obs2; obspool [1] = obs2pool;
		obs [2] = obs3; obspool [2] = obs3pool;
		obs [3] = obs4; obspool [3] = obs4pool;
		obs [4] = obs5; obspool [4] = obs5pool;
		obs [5] = obs6; obspool [5] = obs6pool;

		//array convention guidelines:
		//object pool 1 = obspool[0]
		//object 1 = obs[0]
		//object in object pool 1 = obspool[0][0] (1st number denotes which pool, 2nd number denotes which member of pool)
	}

	void Start () {
		//instantiate object pool of 'poolSize' per item
		for(int n=0; n<6; n++){
			obspool[n] = new List<GameObject>();
			for (int i=0;i<poolSize;i++){
				GameObject obj = Instantiate(obs[n]);
				obj.SetActive(false);
				obs1pool.Add(obj);
				Debug.Log ("Successfully added object " + n + " to pool in register " + i + ".");
			}
			Debug.Log ("Pool number " + n + " has finished pooling");
		}
		Debug.Log ("All object pools has finished pooling");

		//Game dimulai dalam 'delay' detik
		InvokeRepeating ("SpawnObstacles", delay, interval);
		Debug.Log ("Invoking obstacle spawner function in " + delay + "s every " + interval + "s");
	}

	//loveyou sayang bebebzzzqqu arrghhhnnnn~~~
	void SpawnObstacles(int x){
		if (x == null) {
			x = 3;
			Debug.Log ("SpawnObstacles called normally");
		}

		//choose where to spawn (p1 vs p2 vs both)
		if (Random.value == 1 || x == 0) {
			
			if (x == 0) {
				Debug.Log ("Spawning for x=0.");
			}

			//SPAWN FOR BOTH
			GameObject obj1 = obstacle (Random.Range (0, 2)); //ambil obstacle dari pool
			GameObject obj2 = obstacle (Random.Range (0, 2)); //ambil obstacle dari pool
			obj1.transform.position = p1enemy.position; obj1.SetActive (true); //keluarkan obstacle untuk player 1
			obj2.transform.position = p2enemy.position; obj2.SetActive (true); //keluarkan obstacle untuk player 2
		} 
		else {
			if (Random.value == 1 || x == 1) { //Spawn for P1
				GameObject obj;
				if (x == 1) {
					obj = obstacle (Random.Range (0, 5));
					Debug.Log ("Spawning for x=1.");
				} 
				else {
					obj = obstacle (Random.Range (0, 8));
				}
				obj.transform.position = p1enemy.position;
				obj.SetActive (true);
			} 
			else {
				//spawn P2
				GameObject obj;
				if (x == 2) {
					obj = obstacle (Random.Range (0, 5));
					Debug.Log ("Spawning for x=2.");
				} 
				else {
					obj = obstacle (Random.Range (0, 8));
				}
				obj.transform.position = p2enemy.position;
				obj.SetActive (true);
			}
		}
	}

	GameObject obstacle(int n){
		if (n > 5) {
			Debug.Log ("Spawned NOTHING.");
			SpawnObstacles (Random.Range (0, 2));
			return null;
		} else {
			for (int i = 0; i < obspool [n].Count; i++) {
				if (!obspool [n] [i].activeInHierarchy) {
					Debug.Log ("Activated obstacle type " + n + " in register " + i + " of it's array");
					return obspool [n] [i];
				}
			}
			Debug.Log ("All objects in object pool" + n + " is already active.");
			return null;
		}
	}
}
