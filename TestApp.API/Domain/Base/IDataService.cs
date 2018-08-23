using TestApp.API.Data.Repositories.Interfaces;

namespace TestApp.API.Domain.Base
{
    public interface IDataService
    {
        IValuesRepository ValuesService {get;}
    }
}