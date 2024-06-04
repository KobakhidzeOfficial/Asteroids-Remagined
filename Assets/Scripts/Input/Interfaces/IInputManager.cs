using UnityEngine;

namespace AsteroidsGame.InputHandler
{
	public interface IInputManager
	{
		/// <summary>
		/// Returns the position of currently targeted location.
		/// </summary>
		Vector3 GetAimingPosition();

		/// <summary>
		/// Returns current throttle input value, between 0 to 1.
		/// </summary>
		float GetThrottle();

		/// <summary>
		/// Returns the state of shooting button.
		/// </summary>
		bool GetFireButton();

		/// <summary>
		/// Returns the state of the WarpDrive button.
		/// </summary>
		bool GetWarpDriveButton();
	}
}