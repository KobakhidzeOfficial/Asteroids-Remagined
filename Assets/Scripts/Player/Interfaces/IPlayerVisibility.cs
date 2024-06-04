namespace AsteroidsGame.PlayerShip
{
	public interface IPlayerVisibility
	{
		bool IsDisabled { get; }
		void OnSoftDeath();
		void OnDeath();
	}
}