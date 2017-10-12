
namespace TestCoreNanny
{


    public class InMemoryContactRepository
    {


        public async System.Threading.Tasks.Task<string[]> GetAll()
        {
            return await System.Threading.Tasks.Task.FromResult("a,b,c".Split(','));
        }


    }


}
