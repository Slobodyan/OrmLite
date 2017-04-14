using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using OrmLiteConnection;
using ServiceStack.OrmLite.Dapper;

namespace OrmLite.Repositories
{
	public class UserRepositoryWithOrmLiteDapper
	{
		private readonly string _connectionString =
			ConfigurationManager.ConnectionStrings["OrmLiteConnection"].ConnectionString;

		public async Task<IEnumerable<User>> GetUsers()
		{
			IEnumerable<User> users;
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				users = await db.QueryAsync<User>("SELECT * FROM [User]");
			}
			return users;
		}

		public async Task<User> Get(int id)
		{
			User user;
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var awaiter = await db.QueryAsync<User>("SELECT * FROM [User] WHERE [Id] = @id", new {id});
				user = awaiter.FirstOrDefault();
			}
			return user;
		}

		public async Task<User> Create(User user)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var sqlQuery =
					"INSERT INTO [User] ([FirstName], [LastName]) VALUES(@FirstName, @LastName); SELECT CAST(SCOPE_IDENTITY() AS INT)";
				var awaiter = await db.QueryAsync<int>(sqlQuery, user);
				user.Id = awaiter.FirstOrDefault();
			}
			return user;
		}

		public async Task Update(User user)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var sqlQuery = "UPDATE [User] SET [FirstName] = @FirstName, [LastName] = @LastName WHERE [Id] = @Id";
				await db.ExecuteAsync(sqlQuery, user);
			}
		}

		public async Task Delete(int id)
		{
			using (IDbConnection db = new SqlConnection(_connectionString))
			{
				var sqlQuery = "DELETE FROM [User] WHERE [Id] = @id";
				await db.ExecuteAsync(sqlQuery, new {id});
			}
		}
	}
}