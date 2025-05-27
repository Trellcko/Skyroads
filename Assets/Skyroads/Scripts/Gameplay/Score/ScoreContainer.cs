namespace Trell.Skyroads.Gameplay.Score
{
    public class ScoreContainer : IScoreContainer
    {
        public IScore Score { get; private set; } = new Score();
        public ICurrency MeteoritesPassed { get; private set; } = new Currency();
    }
}