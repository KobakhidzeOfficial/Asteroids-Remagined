using System;

using AsteroidsGame.Enemies.Asteroid;
using AsteroidsGame.Enemies.Saucer;
using AsteroidsGame.GameLogic;
using AsteroidsGame.PlayerShip;
using AsteroidsGame.Services;

namespace AsteroidsGame.Data
{
	[Serializable]
	public class GameSettings
	{
		public GameManager.Settings GameManagerSettings;
		public WaveManager.Settings WaveManagerSettings;
		public ScreenEdgesGenerator.Settings ScreenBondarySettings;
		public EnemySpawner.Settings EnemySpawnerSettings;
	}

	[Serializable]
	public class PlayerSettings
	{
		public PlayerHealth.Settings PlayerHealthSettings;
		public PlayerMovement.Settings PlayerMovementSettings;
		public PlayerShoot.Settings PlayerShootSettings;
		public PlayerVisibility.Settings PlayerVisibilitySettings;
		public PlayerWarpDrive.Settings PlayerWarpDriveSettings;
	}

	[Serializable]
	public class EnemySettings
	{
		public AsteroidData LargeAsteroidSettings;
		public AsteroidData MediumAsteroidSettings;
		public AsteroidData SmallAsteroidSettings;

		public SaucerData BigSaucerSettings;
		public SaucerData SmallSaucerSettings;
	}

	[Serializable]
	public class AsteroidData
	{
		public AsteroidHealth.Settings HealthSettings;
		public AsteroidMovement.Settings MovementSettings;
	}

	[Serializable]
	public class SaucerData
	{
		public SaucerHealth.Settings HealthSettings;
		public SaucerMovement.Settings MovementSettings;
		public SaucerShootBase.Settings ShootSettings;
	}
}