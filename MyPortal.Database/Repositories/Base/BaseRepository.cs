using MyPortal.Database.Models.Connection;
using SqlKata.Compilers;

namespace MyPortal.Database.Repositories.Base
{
    public abstract class BaseRepository
    {
        protected readonly SqlServerCompiler Compiler;

        protected BaseRepository(DbUser dbUser)
        {
            DbUser = dbUser;
            Compiler = new SqlServerCompiler();
        }

        protected virtual DbUser DbUser { get; }
    }
}