namespace HomeMyDay.Web.Home.Repository
{
	public interface INewspaperRepository
	{
		/// <summary>
		/// The email of the user to subscribe
		/// </summary>
		/// <param name="email"></param>
		/// <returns></returns>
		bool Subscribe(string email);
	}
}
