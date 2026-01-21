using Player.Domain.PlayerStateMachine;

public interface IStateHandle
{
    InitializationPlayerStateMachine StateMachine { get; }
    bool CanHandle();
    void Handle();
}