using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace EFWithDapper
{
    public class DapperRepository<T> : IDapperRepository
    {
        private readonly IDbConnection _dbConnection;

        public DapperRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName}";
            return await _dbConnection.QueryAsync<T>(query);
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var tableName = typeof(T).Name;
            var query = $"SELECT * FROM {tableName} WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public async Task AddAsync<T>(T entity)
        {
            var tableName = typeof(T).Name;
            var query = $"INSERT INTO {tableName} VALUES (@Id, @Descricao, @Preco, @Desconto)";
            await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task UpdateAsync<T>(T entity)
        {
            var tableName = typeof(T).Name;
            var query = $"UPDATE {tableName} SET Descricao = @Descricao, preco = @Preco, desconto = @Desconto WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, entity);
        }

        public async Task DeleteAsync<T>(int id)
        {
            var tableName = typeof(T).Name;
            var query = $"DELETE FROM {tableName} WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(query, new { Id = id });
        }
    }
}
