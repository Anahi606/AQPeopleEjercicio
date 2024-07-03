using People.Models;
using SQLite;

namespace People;



public class PersonRepository
{
    private SQLiteAsyncConnection conn;

    string _dbPath;

    public string StatusMessage { get; set; }

    // TODO: Add variable for the SQLite connection

    private async Task Init()
    {
        if (conn != null)
            return;

        conn = new SQLiteAsyncConnection(_dbPath);

        await conn.CreateTableAsync<AQPerson>();
    }

    public PersonRepository(string dbPath)
    {
        _dbPath = dbPath;                        
    }

    public async Task AddNewPerson(string name)
    {
        int result = 0;
        try
        {
            // Call Init()
            await Init();

            // basic validation to ensure a name was entered
            if (string.IsNullOrEmpty(name))
                throw new Exception("Valid name required");

            result = await conn.InsertAsync(new AQPerson { Name = name });

            StatusMessage = string.Format("{0} record(s) added [Name: {1})", result, name);
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to add {0}. Error: {1}", name, ex.Message);
        }
    }

    public async Task<List<AQPerson>> GetAllPeople()
    {
        try
        {
            await Init();
            return await conn.Table<AQPerson>().ToListAsync();
        }
        catch (Exception ex)
        {
            StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
        }

        return new List<AQPerson>();
    }
}
