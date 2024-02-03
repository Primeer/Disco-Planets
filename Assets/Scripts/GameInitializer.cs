using Model;
using VContainer.Unity;

public class GameInitializer : IStartable
{
    private readonly ThrowerModel throwerModel;


    public GameInitializer(ThrowerModel throwerModel)
    {
        this.throwerModel = throwerModel;
    }

    public void Start()
    {
        throwerModel.SpawnBall();
    }
}