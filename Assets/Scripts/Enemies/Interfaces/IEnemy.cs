using UnityEngine;

namespace AsteroidsGame.Enemies
{
	public interface IEnemy
	{
		void Setup(Vector3 pos, Quaternion rot);
		void Destroy();
	}
}