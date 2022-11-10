using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementación de una máquina de estados con clases.

namespace OO {

    public class FSM2 : MonoBehaviour {

        StateBase _currentState;
        StateBase currentState {
            get {
                return _currentState;
            }

            set {

                if (value == _currentState) return;

                if (_currentState != null)
                    _currentState.Exit();
                _currentState = value;
                _currentState.Enter();

            }
        }
        public static StateBase patrolState;
        public static StateBase chaseState;

        void Start() {

            patrolState = new Patrol(this);
            chaseState = new Chase(this);

            currentState = patrolState;      
        }

        void Update () {
            currentState = currentState.Update();
        }    

    }


    public class StateBase {

        public FSM2 fsm;

        public StateBase(FSM2 fsm) {
            this.fsm = fsm;
        }

        public virtual void Enter() {}
        public virtual StateBase Update() { return this; }
        public virtual void Exit() {}

    }

    class Chase : StateBase {

        public Chase(FSM2 fsm) : base(fsm) {}

        public override void Enter() {
            Debug.Log("Chase enter");
        }

        public override StateBase Update() {
            
            Debug.Log("Chase update");

            if (Input.GetKeyDown(KeyCode.Space)) {
                return FSM2.patrolState;
            }

            return this;

        } 

        public override void Exit() {
            Debug.Log("Chase exit");
        } 

    }

    class Patrol : StateBase {

        public Patrol(FSM2 fsm) : base(fsm) {}

        public override void Enter() {
            Debug.Log("Patrol enter");
        }

        public override StateBase Update() {

            Debug.Log("Patrol update");


            if (Input.GetKeyDown(KeyCode.Space)) {
                return FSM2.chaseState;
            }

            return this;
            
        } 

        public override void Exit() {
            Debug.Log("Patrol exit");
        } 

    }

}