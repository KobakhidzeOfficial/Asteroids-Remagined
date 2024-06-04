using UnityEngine;

namespace AsteroidsGame.Enemies
{
	public interface IEnemyHealth
	{
		void CheckForDamage(Collider2D col);
	}
}