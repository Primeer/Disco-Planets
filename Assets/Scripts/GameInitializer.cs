using Model;
using VContainer.Unity;

public class GameInitializer : IStartable
{
    private readonly ThrowerModel throwerModel;
    private readonly BoosterModel boosterModel;

        
    public GameInitializer(ThrowerModel throwerModel, BoosterModel boosterModel)
    {
        this.throwerModel = throwerModel;
        this.boosterModel = boosterModel;
    }

    public void Start()
    {
        throwerModel.SpawnBall();
            
        boosterModel.RefreshBooster(0);
        boosterModel.RefreshBooster(1);
    }
}