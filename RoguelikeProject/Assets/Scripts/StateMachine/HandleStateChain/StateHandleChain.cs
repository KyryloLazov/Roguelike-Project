using System.Collections.Generic;
using System.Linq;
using Zenject;

public class StateHandleChain
{
    private List<IStateHandle> _handles;

    [Inject]
    public StateHandleChain(IEnumerable<IStateHandle> handles)
    {
        _handles = handles.ToList();
    }

    public void HandleState() =>
        _handles.FirstOrDefault(handle => handle.CanHandle())?.Handle();

    public void HandleState<T>() where T : IStateHandle =>
        _handles.FirstOrDefault(handle => handle.GetType() == typeof(T) && handle.CanHandle())?.Handle();

    public bool CanHandleState<T>() where T : IStateHandle
    {
        var handle = _handles.FirstOrDefault(handle => handle.GetType() == typeof(T) && handle.CanHandle());
        return handle != null;
    }
}