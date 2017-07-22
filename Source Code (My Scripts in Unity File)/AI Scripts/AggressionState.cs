using UnityEngine;
using System.Collections;

public class AggressionState {

	/*
	 *	State key
	 * 	0 = passive
	 * 	1 = follow
	 *  2 = attack
	 */

	public int state { get; set; }

	public void increaseAggression(){
		state++;
		if (state > 2) {
			state = 2;
		}
	}

	public void decreaseAggression(){
		state--;
		if (state < 0) {
			state = 0;
		}
	}

	public void initialize(){
		state = Random.Range (0, 2);
	}

	public bool isLook(){
		return state == 0;
	}

	public bool isFollow(){
		return state == 1;
	}

	public bool isAttack(){
		return state == 2;
	}

}
