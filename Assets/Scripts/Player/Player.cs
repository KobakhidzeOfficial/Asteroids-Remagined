using UnityEngine;

using Zenject;

namespace AsteroidsGame.PlayerShip
{
	public class Player : MonoBehaviour, IPlayer
	{
		public int Health => _Health.CurrentHealth;
		public Vector3 Position => transform.position;

		private IPlayerHealth _Health;

		[Inject]
		private void Construct(IPlayerHealth health)
		{
			_Health = health;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			_Health.CheckForDamage(collision);
		}
	}
}