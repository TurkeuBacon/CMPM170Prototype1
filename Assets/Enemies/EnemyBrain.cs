using System;
using UnityEngine;

[Serializable]
public class EnemyBrain: MonoBehaviour
{
    [SerializeField] private string initState;
    [SerializeField] private FSMState[] states;
    public FSMState CurrentState { get; set; }

    private void Start()
    {
        ChangeState(initState);
    }
    public void Update()
    {
        if (CurrentState == null) return;
        CurrentState.UpdateState(this);
    }

    public void ChangeState(string newStateID)
    {
        FSMState newState = GetState(newStateID);
        if (newState == null) return;

        CurrentState = newState;
    }

    private FSMState GetState(string newStateID)
    {
        for (int i = 0; i < states.Length; i++)
        {
            if (states[i].ID == newStateID)
            {
                return states[i];
            }
        }

        return null;
    }
}
