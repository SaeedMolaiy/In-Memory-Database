using Microsoft.EntityFrameworkCore;

namespace SomeApplication.Tests
{
    /// <summary>
    /// Test Environment Configurations and Implementations
    /// </summary>
    public static class TestEnvironment
    {
        private static SomeDbContext _context;

        /// <summary>
        /// Just In Case :
        /// Never Use InMemory Database in Production Environment!!
        /// InMemory DataBase Is Only Designed For Testing Environment.
        /// Using InMemory DataBase in Production Has Security Risk.
        /// </summary>
        private static readonly DbContextOptions<SomeDbContext> _options = new DbContextOptionsBuilder<SomeDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDbForTesting")
                .Options;


        public static SomeDbContext GetDbContextForTestingEnvironment()
        {
            if (_context != null)
            {
                //Because All Tests, Get A Single Instance Of SomeDbContext(In Memory),
                //If a TestMethod Adds a Data To This Context, it may effect Other Tests,
                //So We Clear Context DataBase Property Data Every Time We Return It.
                //So Tests Won't Effect Each Other.
                _context.Database.EnsureDeleted();
            }
            _context = new SomeDbContext(_options);
            return _context;
        }
    }
}