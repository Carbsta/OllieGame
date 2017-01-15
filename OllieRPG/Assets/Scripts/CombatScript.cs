using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatScript : MonoBehaviour {

	[SerializeField]
	private string[] emotions;
	[SerializeField]
	private Text button1;
	[SerializeField]
	private Text button2;
	[SerializeField]
	private Text button3;
	[SerializeField]
	private Text button4;
	[SerializeField]
	private Text button5;
	[SerializeField]
	private Text button6;

	public void StartCombat(){
		shuffle (emotions);
		button1.text = emotions [0];
		button2.text = emotions [1];
		button3.text = emotions [2];
		button4.text = emotions [3];
		button5.text = emotions [4];
		button6.text = emotions [5];
	}

	//fisher-yates shuffle algorithm - shuffles the array into a random order.
	public void shuffle<T>(T[] array){
		int n = array.Length;
		for (int i = 0; i < n; i++) 
		{
			//Random.value returns a random float between 0.0 (inclusive) and 0.1 (inclusive)
			//k ← random integer such that i ≤ k < n, as specified by Knuth
			//int k = i + (int)(Random.value * (n - i)); ensures that i ≤ k < n is true. (int) casts it to an integer so it can be an index.
			int k = i + (int)(Random.value * (n - i));
			//T t - stores the value of array[k]. Is generic type T so can work no matter what type of array is passed, here I could use strings.
			T t = array[k];
			//the items at array[k] and array[i] are swapped - shuffling the array.
			array[k] = array[i];
			array[i] = t;
		}
		
		
	}

	// Update is called once per frame
	void Update () {
	
	}
}
