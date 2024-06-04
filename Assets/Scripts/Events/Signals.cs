namespace AsteroidsGame.Events
{
	public class AsteroidCreatedSignal
	{

	}

	public class AsteroidDiedSignal
	{
		public AsteroidDiedSignal(int scoreReward)
		{
			ScoreReward = scoreReward;
		}

		public int ScoreReward
		{
			get;
			private set;
		}
	}

	public class SaucerCreatedSignal
	{

	}

	public class SaucerDiedSignal
	{
		public SaucerDiedSignal(int scoreReward)
		{
			ScoreReward = scoreReward;
		}

		public int ScoreReward
		{
			get;
			private set;
		}
	}

	public class PlayerDiedSignal
	{

	}

	public class GameOverSignal
	{

	}
}