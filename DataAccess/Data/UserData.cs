using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _db;

    public UserData(ISqlDataAccess db)
    {
        _db = db;
    }

    //GetUsers(), bütün userları getirir.
    public Task<IEnumerable<UserModel>> GetUsers() =>
        _db.LoadData<UserModel, dynamic>(storedProcedure: "dbo.spUser_GetAll", new { });
    //dynamic; bütün tiplerdeki parametrelerle işlem yapılabilir.
    //dynamic olduğu ve parametre olmadığından boş nesne gönderilmesi için new{} yazılır.
    //IEnumerable, koleksiyon üzerinde iterasyon yapılmasını sağlar (foreach gibi).
    public async Task<UserModel?> GetUser(int id)
    {
        var results = await _db.LoadData<UserModel, dynamic>(
            storedProcedure: "dbo.spUser_Get",
            new { Id = id });
        return results.FirstOrDefault(); //Ya IEnumerable'daki ilk elemanı döndürür ya da default value'yu yani null döndürür.
    }

    public Task InsertUser(UserModel user) =>
        _db.SaveData(storedProcedure: "dbo.spUser_Insert", new { user.FirstName, user.LastName });


    public Task UpdateUser(UserModel user) =>
        _db.SaveData(storedProcedure: "dbo.spUser_Update", user);

    public Task DeleteUser(int id) =>
        _db.SaveData(storedProcedure: "dbo.spUser_Delete", new { Id = id });
}

