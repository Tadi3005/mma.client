namespace Ai.Csharp.Stub.Infrastructures;

using Serilog;

//TODO: supprime-moi
public class EntityRepositoryImpl(ILogger logger)
{
    public void DoSomething()
        => logger.Warning("Coucou de l'infrastructure");
}
