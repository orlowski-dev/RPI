using System;
using Godot;

// Generyczny singleton bo static nie jest poliformiczny i Instance
// by nadpisywały się nawzajem w klasach dziedziczących

/// <summary>
/// Bazowa klasa singleton dla systemów globalnych w grze.
/// </summary>
/// <remarks>
/// Generyczna klasa abstrakcyjna implementująca
/// wzorzec Singleton dla klas dziedziczących po Node.
/// </remarks>
public abstract partial class BaseSingleton<T> : Node
    where T : Node
{
    public static T Instance { get; private set; }

    public override void _EnterTree()
    {
        if (Instance != null)
        {
            GD.PushError($"{typeof(T).Name} już istnieje!");
            QueueFree();
            return;
        }

        Instance = this as T;
    }

    public override void _ExitTree()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
