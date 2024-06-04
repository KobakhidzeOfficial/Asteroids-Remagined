using AsteroidsGame.Data;
using AsteroidsGame.Data.Types;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Installers
{
	[CreateAssetMenu(fileName = "GameSettings", menuName = "AsteroidsGame/GameSettings")]
	public class GameSettingsInstaller : ScriptableObjectInstaller<GameSettingsInstaller>
	{
		public PlayerSettings Player;
		public EnemySettings Enemy;
		public GameSettings GameSetup;

		public override void InstallBindings()
		{
			Container.BindInstance(GameSetup.GameManagerSettings).IfNotBound();
			Container.BindInstance(GameSetup.WaveManagerSettings).IfNotBound();
			Container.BindInstance(GameSetup.ScreenBondarySettings).IfNotBound();
			Container.BindInstance(GameSetup.EnemySpawnerSettings).IfNotBound();

			Container.BindInstance(Player.PlayerHealthSettings).IfNotBound();
			Container.BindInstance(Player.PlayerMovementSettings).IfNotBound();
			Container.BindInstance(Player.PlayerShootSettings).IfNotBound();
			Container.BindInstance(Player.PlayerVisibilitySettings).IfNotBound();
			Container.BindInstance(Player.PlayerWarpDriveSettings).IfNotBound();

			Container.BindInstance(Enemy.LargeAsteroidSettings).WithId(AsteroidSize.Big).IfNotBound();
			Container.BindInstance(Enemy.MediumAsteroidSettings).WithId(AsteroidSize.Medium).IfNotBound();
			Container.BindInstance(Enemy.SmallAsteroidSettings).WithId(AsteroidSize.Small).IfNotBound();

			Container.BindInstance(Enemy.BigSaucerSettings).WithId(SaucerSize.Big).IfNotBound();
			Container.BindInstance(Enemy.SmallSaucerSettings).WithId(SaucerSize.Small).IfNotBound();
		}
	}
}