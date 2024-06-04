using UnityEngine;

namespace AsteroidsGame.PlayerShip
{
	public interface IPlayer
	{
		int Health { get; }
		Vector3 Position { get; }
	}
}