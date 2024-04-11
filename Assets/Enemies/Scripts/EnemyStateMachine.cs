using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EnemyStateMainDelegate(EnemyStateParams enemyStateParams);
public delegate bool EnemyStateTransitionDelegate(EnemyStateParams enemyStateParams);

public class EnemyStateMachine {

    private Dictionary<string, EnemyState> states;
    private EnemyState currentState;
    public EnemyStateMachine() {
        states = new Dictionary<string, EnemyState>();
    }
    
    public void addState(string stateName, EnemyState newState) {
        if(states.ContainsKey(stateName)) {
            Debug.LogError("STATE ALREADY EXISTS");
        } else {
            states.Add(stateName, newState);
        }
    }
    public void addTransition(string currentState, string nextState, EnemyStateTransitionDelegate transitionDelegate) {
        if(states.ContainsKey(currentState) && states.ContainsKey(nextState)) {
            states[currentState].addTransition(new EnemyTransition(transitionDelegate, states[nextState]));
        } else {
            Debug.LogError("MISSING STATE KEYS");
        }
    }
    public void setCurrentState(string state) {
        if(states.ContainsKey(state)) {
            currentState = states[state];
        } else {
            Debug.LogError("MISSING STATE KEY");
        }
    }
    public void execute(EnemyStateParams enemyStateParams) {
        EnemyState nextState = currentState.execute(enemyStateParams);
        currentState = nextState;
    }
    public EnemyStateMachine Clone() {
        EnemyStateMachine clone = new EnemyStateMachine();
        foreach(string stateKey in states.Keys) {
            clone.addState(stateKey, states[stateKey]);
        }
        clone.currentState = currentState;
        return clone;
    }
}

public class EnemyState {
    private EnemyStateMainDelegate mainLoop;
    private List<EnemyTransition> transitions;
    public EnemyState(EnemyStateMainDelegate mainLoop) {
        this.mainLoop = mainLoop;
        transitions = new List<EnemyTransition>();
    }
    public void addTransition(EnemyTransition transition) {
        transitions.Add(transition);
    }
    public EnemyState execute(EnemyStateParams enemyStateParams) {
        mainLoop(enemyStateParams);
        foreach(EnemyTransition transition in transitions) {
            if(transition.transitionDelegate(enemyStateParams)) {
                return transition.nextState;
            }
        }
        return this;
    }

}

public struct EnemyTransition {
    public EnemyStateTransitionDelegate transitionDelegate;
    public EnemyState nextState;
    public EnemyTransition(EnemyStateTransitionDelegate transitionDelegate, EnemyState nextState) {
        this.transitionDelegate = transitionDelegate;
        this.nextState = nextState;
    }

}

public struct EnemyStateParams {
    public EnemyBase self;
    public EnemyBase[] enemies;
    public PlayerController player;
    public EnemyStateParams(EnemyBase self, EnemyBase[] enemies, PlayerController player) {
        this.self = self;
        this.enemies = enemies;
        this.player = player;
    }
}
