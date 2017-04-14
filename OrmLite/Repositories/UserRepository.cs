using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using OrmLiteConnection;
using ServiceStack.OrmLite;

namespace OrmLite.Repositories
{
	public class UserRepository
	{
		private readonly OrmLiteConnectionFactory dbFactory =
			new OrmLiteConnectionFactory(ConfigurationManager.ConnectionStrings["OrmLiteConnection"].ConnectionString,
				SqlServerDialect.Provider);

		public async Task<IEnumerable<User>> GetUsers()
		{
			IEnumerable<User> users;
			using (var db = dbFactory.Open())
			{
				users = await db.SelectAsync<User>();
			}
			return users;
		}

		public async Task<User> Get(int id)
		{
			User user;
			using (var db = dbFactory.Open())
			{
				var awaiter = await db.SelectAsync<User>(u => u.Id == id);
				user = awaiter.FirstOrDefault();
			}
			return user;
		}

		public async Task<User> Create(User user)
		{
			using (var db = dbFactory.Open())
			{
				var awaiter = await db.InsertAsync(user);
				user.Id = (int) awaiter;
			}
			return user;
		}

		public async Task Update(User user)
		{
			using (var db = dbFactory.Open())
			{
				await db.UpdateAsync(user);
			}
		}

		public async Task Delete(int id)
		{
			using (var db = dbFactory.Open())
			{
				await db.DeleteAsync<User>(u => u.Id == id);
			}
		}
	}
}