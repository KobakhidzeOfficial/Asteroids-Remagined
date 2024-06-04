using System;
using System.Collections;

using AsteroidsGame.Events;
using AsteroidsGame.Services;

using UnityEngine;
using UnityEngine.SceneManagement;

using Zenject;

namespace AsteroidsGame.GameLogic
{
	public class GameManager : IInitializable
	{
		readonly Settings _settings;
		readonly CoroutineRunner _coroutineRunner;
		readonly SignalBus _signalBus;

		public GameManager(Settings settings,
			CoroutineRunner coroutineRunner,
			SignalBus signalBus)
		{
			_settings = settings;
			_coroutineRunner = coroutineRunner;
			_signalBus = signalBus;
		}

		public void Initialize()
		{
			_signalBus.Subscribe<GameOverSignal>(OnGameOver);
		}

		private void OnGameOver()
		{
			_coroutineRunner.StartCoroutine(IE_GameOver());
		}

		IEnumerator IE_GameOver()
		{
			yield return new WaitForSeconds(_settings.GameOverRestartDelay);

			EndGame();
		}

		private void EndGame()
		{
			SceneManager.LoadScene(0);
		}

		[Serializable]
		public class Settings
		{
			public float GameOverRestartDelay;
		}
	}
}