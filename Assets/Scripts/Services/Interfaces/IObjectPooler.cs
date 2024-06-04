namespace AsteroidsGame.Services
{
	public interface IObjectPooler<T>
	{
		bool IsAvailable { get; }

		/// <summary>
		/// Returns an object from pool.
		/// </summary>
		bool GetObject(out T obj);
		void AddObjectToAvailable(T obj);
	}
}