using UnityEngine;

using Zenject;

namespace AsteroidsGame.Services
{
	public interface IEnemySpawner
	{
		void SpawnOnRandomLocation<U, T>(PlaceholderFactory<U, T> factory, U data, int amount, bool randomDir);
		void SpawnInsideCircle<U, T>(PlaceholderFactory<U, T> factory, U data, Vector3 _Position, int amount, bool randomDir);
	}
}