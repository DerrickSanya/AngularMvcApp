using TestApp.API.Data.Base;
using TestApp.API.Data.Context;
using TestApp.API.Data.Repositories.Base;
using TestApp.API.Data.Repositories.Interfaces;
using TestApp.API.Models;

namespace TestApp.API.Data.Repositories {
    public class ValuesRepository : BaseRepository<Value>, IValuesRepository {
        public ValuesRepository (TestAppDataContext context) : base (context) {

        }
    }
}