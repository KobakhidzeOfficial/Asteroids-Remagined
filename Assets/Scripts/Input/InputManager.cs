using UnityEngine;

namespace AsteroidsGame.InputHandler
{
	public class InputManager : IInputManager
	{
		public Vector3 GetAimingPosition()
		{
			return Input.mousePosition;
		}

		public float GetThrottle()
		{
			return Input.GetKey(KeyCode.W) ? 1f : 0f;
		}

		public bool GetFireButton()
		{
			return Input.GetMouseButtonDown(0);
		}

		public bool GetWarpDriveButton()
		{
			return Input.GetKeyDown(KeyCode.LeftShift);
		}
	}
}