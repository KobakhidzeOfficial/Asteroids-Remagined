using System;
using System.Collections;

using AsteroidsGame.Data.Types;
using AsteroidsGame.Enemies.Asteroid;
using AsteroidsGame.Enemies.Saucer;
using AsteroidsGame.Events;
using AsteroidsGame.Services;

using UnityEngine;

using Zenject;

using Random = UnityEngine.Random;

namespace AsteroidsGame.GameLogic
{
	public class WaveManager : IInitializable, ITickable
	{
		private enum GameState
		{
			Starting,
			InProgress
		}

		private int _asteroidEnemies;
		private int _saucerEnemies;

		private int _allEnemies => _asteroidEnemies + _saucerEnemies;

		private GameState _currentState = GameState.Starting;

		readonly Settings _settings;
		readonly IEnemySpawner _enemySpawner;
		readonly IScore _score;
		readonly SignalBus _signalBus;
		readonly CoroutineRunner _coroutineRunner;

		readonly AsteroidEnemy.Factory _asteroidFactory;
		readonly SaucerEnemy.Factory _bigSaucerFactory;
		readonly SaucerEnemy.Factory _smallSaucerFactory;

		public WaveManager(Settings settings,
			IEnemySpawner enemySpawner,
			IScore score,
			SignalBus signalBus,
			CoroutineRunner coroutineRunner,
			[Inject(Id = AsteroidSize.Big)] AsteroidEnemy.Factory asteroidFactory,
			[Inject(Id = SaucerSize.Big)] SaucerEnemy.Factory bigSaucerFactory,
			[Inject(Id = SaucerSize.Small)] SaucerEnemy.Factory smallSaucerFactory)
		{
			_settings = settings;
			_enemySpawner = enemySpawner;
			_score = score;
			_signalBus = signalBus;
			_coroutineRunner = coroutineRunner;

			_asteroidFactory = asteroidFactory;
			_bigSaucerFactory = bigSaucerFactory;
			_smallSaucerFactory = smallSaucerFactory;
		}

		public void Initialize()
		{
			SetupEventListeners();
			StartNewWave();
		}

		public void Tick()
		{
			CheckForWaveEnd();
		}

		private void SetupEventListeners()
		{
			_signalBus.Subscribe<AsteroidCreatedSignal>(() => _asteroidEnemies++);
			_signalBus.Subscribe<SaucerCreatedSignal>(() => _saucerEnemies++);
			_signalBus.Subscribe<AsteroidDiedSignal>(() => _asteroidEnemies--);
			_signalBus.Subscribe<SaucerDiedSignal>(() => _saucerEnemies--);
		}

		private void StartNewWave()
		{
			_currentState = GameState.Starting;

			_coroutineRunner.StopCoroutine(IE_CheckForSaucerSpawn());
			_coroutineRunner.StartCoroutine(IE_CreateNewWave());
		}

		private void CheckForWaveEnd()
		{
			if (_currentState == GameState.InProgress)
			{
				if (_allEnemies <= 0)
				{
					StartNewWave();
				}
			}
		}

		IEnumerator IE_CreateNewWave()
		{
			yield return new WaitForSeconds(_settings.WaveDelay);

			_currentState = GameState.InProgress;

			_enemySpawner.SpawnOnRandomLocation(_asteroidFactory, AsteroidSize.Big, CalculateAsteroidsToSpawn(), true);
			_coroutineRunner.StartCoroutine(IE_CheckForSaucerSpawn());
		}

		IEnumerator IE_CheckForSaucerSpawn()
		{
			while (_currentState == GameState.InProgress)
			{
				if (_allEnemies > _settings.SaucerSpawnEnemyThreshold || _saucerEnemies > 0)
				{
					yield return new WaitForEndOfFrame();
				}
				else
				{
					yield return new WaitForSeconds(_settings.SaucerSpawnCheckDelay);
					SpawnSaucer();
				}
			}
		}

		private void SpawnSaucer()
		{
			float currentSpawnChance = Random.value;

			if (_settings.SaucerSpawnChance >= currentSpawnChance)
			{
				SaucerEnemy.Factory saucerToSpawn = _bigSaucerFactory;
				SaucerSize saucerSize = SaucerSize.Big;

				if (_settings.SmallSaucerSpawnChance >= currentSpawnChance)
				{
					saucerToSpawn = _smallSaucerFactory;
					saucerSize = SaucerSize.Small;
				}

				_enemySpawner.SpawnOnRandomLocation(saucerToSpawn, saucerSize, 1, false);
			}
		}

		private int CalculateAsteroidsToSpawn()
		{
			int asteroidsToSpawn = _settings.AsteroidStartingCount;
			int incrementMultiplier = _score.GetCurrentScore / _settings.AsteroidIncrementScoreThreshold;
			asteroidsToSpawn += _settings.AsteroidIncrementCount * incrementMultiplier;

			return asteroidsToSpawn;
		}

		[Serializable]
		public class Settings
		{
			public int AsteroidStartingCount;
			public int AsteroidIncrementCount;
			public int AsteroidIncrementScoreThreshold;

			public float SaucerSpawnCheckDelay;
			public float SaucerSpawnChance;
			public float SmallSaucerSpawnChance;
			public int SaucerSpawnEnemyThreshold;

			public float WaveDelay;
		}
	}
}