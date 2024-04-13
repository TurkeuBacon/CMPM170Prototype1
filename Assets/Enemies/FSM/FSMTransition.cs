using System;

[Serializable]
public class FSMTransition 
{
    public FSMDecision Decision; // PlayerInRangeOfAttack -> True / False
    public string TrueState; // CurrentState -> AttackState
    public string FalseState; // CurrentState -> PatrolState

}
