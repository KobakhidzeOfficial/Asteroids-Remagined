using AsteroidsGame.Data.Types;

using UnityEngine;

using Zenject;

namespace AsteroidsGame.Enemies.Saucer
{
	public class SaucerEnemy : MonoBehaviour, IEnemy
	{
		private IEnemyHealth _health;

		[Inject]
		private void Construct(IEnemyHealth health)
		{
			_health = health;
		}

		public void Setup(Vector3 pos, Quaternion rot)
		{
			transform.SetPositionAndRotation(pos, rot);
		}

		public void Destroy()
		{
			Destroy(this.gameObject);
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			_health.CheckForDamage(collision);
		}

		public class Factory : PlaceholderFactory<SaucerSize, SaucerEnemy>
		{

		}
	}
}