using UnityEngine;

namespace AsteroidsGame.PlayerShip
{
	public interface IPlayerHealth
	{
		int CurrentHealth { get; }
		void CheckForDamage(Collider2D col);
	}
}