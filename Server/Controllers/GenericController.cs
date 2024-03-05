using Microsoft.AspNetCore.Mvc;

namespace EInvoiceDemo.Server.Controllers
{
    public class GenericController<TRepositoryInterface> : ControllerBase where TRepositoryInterface : class
    {
        protected readonly TRepositoryInterface _repository;
        public GenericController(TRepositoryInterface repository)
        {
            _repository = repository;
        }
    }
}
