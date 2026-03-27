public interface ICondition
{
    bool Check(GameState state);
}

public interface IEffect
{
    void Apply(GameState state);
}

public interface ICommand
{
    void Execute(GameState state, string[] args);
}

public interface IInteractable
{
    void Interact(GameState state);
}

public interface IGameEvent
{
    void Trigger(GameState state);
}

public abstract class ConditionBase : ICondition
{
    public abstract bool Check(GameState state);
}

public abstract class EffectBase : IEffect
{
    public abstract void Apply(GameState state);
}

public abstract class CommandBase : ICommand
{
    public abstract string Name { get; }
    public abstract string Description { get; }
    public abstract void Execute(GameState state, string[] args);
}

public abstract class GameEventBase : IGameEvent
{
    protected ICondition _condition;
    protected List<IEffect> _effects;
    protected bool _isOneTime;
    protected bool _hasTriggered;

    public GameEventBase(ICondition condition, List<IEffect> effects, bool isOneTime = false)
    {
        _condition = condition;
        _effects = effects ?? new List<IEffect>();
        _isOneTime = isOneTime;
        _hasTriggered = false;
    }

    public virtual void Trigger(GameState state)
    {
        if (_isOneTime && _hasTriggered)
            return;

        if (_condition != null && !_condition.Check(state))
            return;

        foreach (var effect in _effects)
        {
            effect.Apply(state);
        }

        if (_isOneTime)
            _hasTriggered = true;
    }
}

public class GameState
{
    public int Health { get; set; } = 100;
    public List<string> Inventory { get; set; } = new List<string>();
    public Dictionary<string, bool> Flags { get; set; } = new Dictionary<string, bool>();
    public Location CurrentLocation { get; set; }
    public List<string> EventLog { get; set; } = new List<string>();
    public List<Quest> Quests { get; set; } = new List<Quest>();
    public bool IsGameOver { get; set; } = false;
    public int TurnCount { get; set; } = 0;

    public void CheckQuests()
    {
        foreach (var quest in Quests)
        {
            if (!quest.IsCompleted && quest.CompletionCondition != null)
            {
                if (quest.CompletionCondition.Check(this))
                {
                    quest.IsCompleted = true;
                    EventLog.Add($"Quest completed: {quest.Title}");
                }
            }
        }
    }
}

public class Location
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<IInteractable> Objects { get; set; } = new List<IInteractable>();
    public List<IGameEvent> Events { get; set; } = new List<IGameEvent>();
    public Dictionary<string, Location> Exits { get; set; } = new Dictionary<string, Location>();

    public void AddEvent(IGameEvent gameEvent)
    {
        Events.Add(gameEvent);
    }

    public void AddObject(IInteractable obj)
    {
        Objects.Add(obj);
    }

    public void AddExit(string direction, Location location)
    {
        Exits[direction] = location;
    }

    public void TriggerEvents(GameState state)
    {
        foreach (var gameEvent in Events)
        {
            gameEvent.Trigger(state);
        }
    }
}

public class Quest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public ICondition CompletionCondition { get; set; }
    public bool IsCompleted { get; set; } = false;
}

public class World
{
    public Dictionary<string, Location> Locations { get; set; } = new Dictionary<string, Location>();

    public void AddLocation(string name, Location location)
    {
        Locations[name] = location;
    }

    public Location GetLocation(string name)
    {
        Locations.TryGetValue(name, out var location);
        return location;
    }
}