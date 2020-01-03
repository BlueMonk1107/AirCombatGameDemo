using System.Collections;

public class CoroutineItem
{
    public enum CoroutineState
    {
        WAITTING,
        RUNNING,
        PASUED,
        STOP
    }

    public CoroutineState State { get; set; }

    public IEnumerator Body(IEnumerator routine)
    {
        while (State == CoroutineState.WAITTING) yield return null;

        while (State == CoroutineState.RUNNING)
            if (State == CoroutineState.PASUED)
            {
                yield return null;
            }
            else
            {
                if (routine != null && routine.MoveNext())
                    yield return routine.Current;
                else
                    State = CoroutineState.STOP;
            }
    }
}