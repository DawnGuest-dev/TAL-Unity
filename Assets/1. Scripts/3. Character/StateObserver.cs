using System;

public class StateObserver
{
    public event Action<IState> OnStateChanged;

    public void NotifyStateChanged(IState state)
    {
        OnStateChanged?.Invoke(state);
    }
}
