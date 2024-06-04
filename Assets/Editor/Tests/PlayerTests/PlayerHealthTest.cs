using AsteroidsGame.Bullets;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Events;
using AsteroidsGame.PlayerShip;

using NUnit.Framework;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Tests.Player
{
	[TestFixture]
	public class PlayerHealthTests : ZenjectUnitTestFixture
	{
		[SetUp]
		public void CommonInstall()
		{
			Container.Bind<IPlayerVisibility>().FromSubstitute();

			SignalBusInstaller.Install(Container);
			Container.DeclareSignal<PlayerDiedSignal>();
		}

		[Test]
		public void Test_CheckDefaultHealth()
		{
			PlayerHealth.Settings settings = new PlayerHealth.Settings { Health = 3 };

			Container.BindInstance(settings);
			Container.Bind<PlayerHealth>().AsSingle();

			Container.Inject(this);

			PlayerHealth playerHealth = Container.Resolve<PlayerHealth>();
			playerHealth.Initialize();

			Assert.AreEqual(settings.Health, playerHealth.CurrentHealth);
		}

		[Test]
		[TestCase(EnemyTypes.Asteroid, ExpectedResult = 2)]
		[TestCase(EnemyTypes.Saucer, ExpectedResult = 2)]
		[TestCase(EnemyTypes.Player, ExpectedResult = 3)]
		public int Test_TakeDamage(EnemyTypes enemyType)
		{
			PlayerHealth.Settings settings = new PlayerHealth.Settings { Health = 3 };

			Container.BindInstance(settings);
			Container.Bind<PlayerHealth>().AsSingle();

			Container.Inject(this);

			PlayerHealth playerHealth = Container.Resolve<PlayerHealth>();
			playerHealth.Initialize();

			playerHealth.CheckForDamage(CreateBulletCollider(enemyType));

			return playerHealth.CurrentHealth;
		}

		private Collider2D CreateBulletCollider(EnemyTypes enemyType)
		{
			GameObject bulletObj = new GameObject();
			Collider2D bulletCollider = bulletObj.AddComponent<BoxCollider2D>();
			IBullet bullet = bulletObj.AddComponent<MockBullet>();
			bullet.SetupBullet(Vector3.zero, Quaternion.identity, 0f, 0f, enemyType);

			return bulletCollider;
		}

		public class MockBullet : MonoBehaviour, IBullet
		{
			public EnemyTypes OriginType { get; private set; }

			public void OnHit()
			{

			}

			public void SetupBullet(Vector3 pos, Quaternion rot, float speed, float timeToLive, EnemyTypes originType)
			{
				OriginType = originType;
			}
		}
	}
}