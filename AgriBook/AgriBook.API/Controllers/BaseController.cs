using System.Web.Http;
using AgriBook.DB.Models;
using System.Web.Http.Cors;
using AgriBook.API.Helpers;

namespace AgriBook.API.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly AgriBookContext Context;
        private readonly ErrorMessageHelper _errorMessageHelper;

        public ErrorMessageHelper ExceptionMessageHelper { get; }

        public BaseController()
        {
            if (Context == null)
            {
                Context = new AgriBookContext();
            }

            if (_errorMessageHelper == null)
            {
                _errorMessageHelper = new ErrorMessageHelper();
            }

            ExceptionMessageHelper = _errorMessageHelper;
        }
    }
}