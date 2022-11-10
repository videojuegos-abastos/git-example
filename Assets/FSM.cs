using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum State {
	Patrol,
	Chase
}

enum Phase {
	Start = 0,
	Update = 1,
	End = 2
}

public class FSM : MonoBehaviour
{

	[SerializeField]
	State state;

	[SerializeField]
	Phase phase;


	Action[][] actions;

	void Start () {
		

		Action[] patrol = new Action[] {PatrolStart, PatrolUpdate, PatrolEnd};
		Action[] chase = new Action[] {ChaseStart, ChaseUpdate, ChaseEnd};

		actions = new Action[][] { patrol, chase };
		
	}

	void Update() {

		actions[(int) state][(int) phase]();

		// switch (state) {

		// 	case State.Patrol:
		//		
		// 		break;

		// 	case State.Chase:
		// 		break;

		// }

	}


	void NextPhase() {
		int phaseInt = (int) phase + 1;

		if (phaseInt == 3)
			phaseInt = 0;

		phase = (Phase) phaseInt;
	}

	// PATROL
	void PatrolStart() {

		Debug.Log($"Localizamos puntos de patrulla");
		NextPhase();
	}
	void PatrolUpdate() {

		Debug.Log($"Patrullamos, comprobamos condición de salida");

		if (Input.GetKeyDown(KeyCode.Space)) {
			NextPhase();
		}


	}
	void PatrolEnd() {

		Debug.Log("Finalizamos proceso y cambiamos de estado");

		state = State.Chase;
		phase = Phase.Start;

	}


	// CHASE
	void ChaseStart() {}
	void ChaseUpdate() {}
	void ChaseEnd() {}

}


