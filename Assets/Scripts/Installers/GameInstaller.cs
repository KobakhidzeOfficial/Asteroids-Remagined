using AsteroidsGame.Bullets;
using AsteroidsGame.Data.Types;
using AsteroidsGame.Enemies.Asteroid;
using AsteroidsGame.Enemies.Saucer;
using AsteroidsGame.Events;
using AsteroidsGame.GameLogic;
using AsteroidsGame.PlayerShip;
using AsteroidsGame.Services;
using AsteroidsGame.UI;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Installers
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private GameObject _PlayerRoot;
		[SerializeField] private GameObject _UIStatsPanel;
		[SerializeField] private GameObject _MainCamera;
		[SerializeField] private GameObject _CoroutineRunner;

		[SerializeField] private GameObject _StandardBulletPrefab;
		[SerializeField] private GameObject _AsteroidBigPrefab;
		[SerializeField] private GameObject _AsteroidMediumPrefab;
		[SerializeField] private GameObject _AsteroidSmallPrefab;
		[SerializeField] private GameObject _SaucerBigPrefab;
		[SerializeField] private GameObject _SaucerSmallPrefab;

		public override void InstallBindings()
		{
			Container.BindInterfacesTo<Player>().FromComponentOn(_PlayerRoot).AsSingle();

			Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
			Container.BindInterfacesAndSelfTo<WaveManager>().AsSingle().NonLazy();
			Container.BindInterfacesTo<ScoreManager>().AsSingle().NonLazy();

			Container.BindInterfacesTo<EnemySpawner>().AsSingle();
			Container.BindInterfacesTo(typeof(ObjectPooler<IBullet>)).AsSingle();

			Container.BindInterfacesTo<ScreenEdgesGenerator>().AsSingle().NonLazy();

			Container.Bind<UIStatsView>().FromComponentOn(_UIStatsPanel).AsSingle();
			Container.BindInterfacesTo<UIStatsViewModel>().AsSingle().NonLazy();

			Container.Bind<Camera>().FromComponentOn(_MainCamera).AsSingle();

			Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromComponentOn(_CoroutineRunner).AsSingle();

			InstallFactories();
			DeclareSignals();
		}

		private void InstallFactories()
		{
			Container.BindFactory<IBullet, Bullet.Factory>().
				FromComponentInNewPrefab(_StandardBulletPrefab).
				WithGameObjectName("StandardBullet").
				UnderTransformGroup("Bullets");

			Container.BindFactory<AsteroidSize, AsteroidEnemy, AsteroidEnemy.Factory>().
				WithId(AsteroidSize.Big).
				FromSubContainerResolve().
				ByNewPrefabInstaller<AsteroidInstaller>(_AsteroidBigPrefab).
				WithGameObjectName("BigAsteroid").
				UnderTransformGroup("Asteroids");

			Container.BindFactory<AsteroidSize, AsteroidEnemy, AsteroidEnemy.Factory>().
				WithId(AsteroidSize.Medium).
				FromSubContainerResolve().
				ByNewPrefabInstaller<AsteroidInstaller>(_AsteroidMediumPrefab).
				WithGameObjectName("MediumAsteroid").
				UnderTransformGroup("Asteroids");

			Container.BindFactory<AsteroidSize, AsteroidEnemy, AsteroidEnemy.Factory>().
				WithId(AsteroidSize.Small).
				FromSubContainerResolve().
				ByNewPrefabInstaller<AsteroidInstaller>(_AsteroidSmallPrefab).
				WithGameObjectName("SmallAsteroid").
				UnderTransformGroup("Asteroids");

			Container.BindFactory<SaucerSize, SaucerEnemy, SaucerEnemy.Factory>().
				WithId(SaucerSize.Big).
				FromSubContainerResolve().
				ByNewPrefabInstaller<SaucerInstaller>(_SaucerBigPrefab).
				WithGameObjectName("BigSaucer").
				UnderTransformGroup("Saucers");

			Container.BindFactory<SaucerSize, SaucerEnemy, SaucerEnemy.Factory>().
				WithId(SaucerSize.Small).
				FromSubContainerResolve().
				ByNewPrefabInstaller<SaucerInstaller>(_SaucerSmallPrefab).
				WithGameObjectName("SmallSaucer").
				UnderTransformGroup("Saucers");
		}

		private void DeclareSignals()
		{
			SignalBusInstaller.Install(Container);

			Container.DeclareSignal<AsteroidCreatedSignal>().OptionalSubscriber();
			Container.DeclareSignal<AsteroidDiedSignal>().OptionalSubscriber();

			Container.DeclareSignal<SaucerCreatedSignal>().OptionalSubscriber();
			Container.DeclareSignal<SaucerDiedSignal>().OptionalSubscriber();

			Container.DeclareSignal<PlayerDiedSignal>().OptionalSubscriber();
			Container.DeclareSignal<GameOverSignal>().OptionalSubscriber();
		}
	}
}