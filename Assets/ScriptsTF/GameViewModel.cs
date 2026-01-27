using System;

[Serializable]
public class GameViewModel
{
    public bool HasDead { get; private set; }
    public bool IsPaused { get; private set; }

    public event Action<bool> OnPauseStateChanged;

    public GameViewModel()
    {
        HasDead = false;
        IsPaused = false;
    }

    public void SetDeadState(bool hasDied)
    {
        HasDead = hasDied;
    }

    public void SetPauseState(bool isPaused)
    {
        IsPaused = isPaused;
        OnPauseStateChanged?.Invoke(isPaused);
    }
}