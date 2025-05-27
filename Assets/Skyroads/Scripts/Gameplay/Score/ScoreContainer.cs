namespace Trell.Skyroads.Gameplay.Score
{
    public class ScoreContainer : IScoreContainer
    {
        public IScore Score { get; private set; } = new Score();
        public ICurrency PassedMeteorites { get; private set; } = new Currency();
    }
}