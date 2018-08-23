

namespace TestApp.API.Domain
{
    
using TestApp.API.Data.Context;
using TestApp.API.Data.Repositories;
using TestApp.API.Data.Repositories.Interfaces;
using TestApp.API.Domain.Base;
    public class DataService : IDataService
    {
        private TestAppDataContext dbontext;
        private IValuesRepository valuesService;


        public DataService(TestAppDataContext databaseContext)
        {
            dbontext = databaseContext;
        }

        public IValuesRepository ValuesService { 
            get {
                if(valuesService == null)
                {
                    valuesService = new ValuesRepository(dbontext);
                }
 
                return valuesService;
            }
         }
    }
}