using AsteroidsGame.Events;

using Zenject;

namespace AsteroidsGame.Services
{
	public class ScoreManager : IScore, IInitializable
	{
		public int GetCurrentScore { get; private set; } = 0;

		private readonly SignalBus _signalBus;

		public ScoreManager(SignalBus signalBus)
		{
			_signalBus = signalBus;
		}

		public void Initialize()
		{
			_signalBus.Subscribe((AsteroidDiedSignal data) => AddScore(data.ScoreReward));
			_signalBus.Subscribe((SaucerDiedSignal data) => AddScore(data.ScoreReward));
		}

		private void AddScore(int scoreToAdd)
		{
			GetCurrentScore += scoreToAdd;
		}
	}
}