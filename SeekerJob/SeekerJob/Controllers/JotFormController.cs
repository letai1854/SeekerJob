
using SeekerJob.Services;
using System.Web.UI.WebControls;
using SeekerJob.DTO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Net.Http;


namespace SeekerJob.Controllers
{

    public class JotFormController : Controller
    {
        private readonly JotFormService _jotFormService;
        public JotFormController()
        {
            // Retrieve API key from web.config
            string apiKey = ConfigurationManager.AppSettings["JotFormApiKey"];
            _jotFormService = new JotFormService(apiKey);
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> ShowFormWithDownload(string formId)
        {
            if (string.IsNullOrEmpty(formId))
            {
                return HttpNotFound();
            }

            var formDetails = await _jotFormService.GetFormDetails(formId);

            if (formDetails == null || formDetails["content"] == null)
            {
                return View("Error", "Could not retrieve form details.");
            }

            string formUrl = (string)formDetails["content"]["url"];
            string formHtml = await _jotFormService.GetFormHtmlAsync(formUrl);


            if (string.IsNullOrEmpty(formHtml))
            {
                return View("Error", "Failed to retrieve form HTML.");
            }

           
            var viewModel = new FormViewModel
            {
                FormHtml = formHtml,
                FormId = formId // Pass the FormId to the View
            };


            return View(viewModel);
        }
    }
    
}
