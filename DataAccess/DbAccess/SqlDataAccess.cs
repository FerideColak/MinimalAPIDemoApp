using Dapper;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbAccess;
public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfiguration _config;

    public SqlDataAccess(IConfiguration config)
    {
        _config = config;
    }

    public async Task<IEnumerable<T>> LoadData<T, U>( //Generics = return type
        string storedProcedure,
        U parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
        /* using kullanımı con.open() ve con.close() gibi bağlantıyı
         * her seferinde açıp kapamanın önüne geçer. using ile
         * metod kullanılırken yani ihtiyaç halinde bağlantı sağlanır,
         * metodun sonuna gelindiğinde bağlantı kesilir. Yanlışlıkla
         * bağlantının açık bırakılması önlenmiş olur.
         */

        return await connection.QueryAsync<T>(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
        /* Bu line asenkronize olarak connectiona der ki, sözkonusu stored procedure'ü
         * execute et, parametreler bunlar ve bu bir stored procedure diye
         * tanımlar.
         */
         /* LoadData(); sql bağlantısı yapar, 
         * query'yi icra eder,
         * IEnumerable return eder.
         */
    }

    public async Task SaveData<T>(
        string storedProcedure,
        T parameters,
        string connectionId = "Default")
    {
        using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

        await connection.ExecuteAsync(storedProcedure, parameters,
            commandType: CommandType.StoredProcedure);
        //SaveData'da veri return edilmiyor.
    }

}

