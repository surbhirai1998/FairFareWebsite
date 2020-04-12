using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using FairFareML.Model;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FairFareWebsite.Controllers
{
    public class PricePredictionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // GET: /<controller>/
        public IActionResult Predict_price(ModelInput input)
        {
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(@"..\FairFareML.Model\MLModel.zip", out var modelInputSchema);
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);

            ModelOutput result = predEngine.Predict(input);

            ViewBag.Price = result.Score;
            return View(input);
        }
    }
}
  