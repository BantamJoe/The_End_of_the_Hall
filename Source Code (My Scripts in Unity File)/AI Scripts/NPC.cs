using UnityEngine;
using System.Collections;

public class NPC {

	public string name { get; set; }
	public AggressionState aggressionState { get; set; }
	public Vector3[,] boundary { get; set; }

}
