using CarRental.Business.Interfaces;
using CarRental.Business.Models;
using CarRental.WebApp.Models;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WebApp.Controllers
{
    [AllowAnonymous]
    public class SearchController : Controller
    {
        private readonly ILocationBusiness _locationBusiness;
        private readonly IVehicleTypeBusiness _vehicleTypeBusiness;
        private readonly ISearchBusiness _searchBusiness;

        public SearchController(ILocationBusiness locationBusiness, IVehicleTypeBusiness vehicleTypeBusiness, ISearchBusiness searchBusiness)
        {
            _locationBusiness = locationBusiness;
            _vehicleTypeBusiness = vehicleTypeBusiness;
            _searchBusiness = searchBusiness;
        }

        public SearchViewModel GetPreapreSearchModel()
        {
            var locations = _locationBusiness.GetNames();
            var locationViewModels = Mapper.Map<List<LocationSearchModel>, List<LocationViewModel>>(locations);

            var vehicleTypes = _vehicleTypeBusiness.GetAll(false);
            var vehicleTypeViewModels = new List<VehicleTypeViewModel>();
            foreach (var item in vehicleTypes)
            {
                var viewModelItem = new VehicleTypeViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Path = item.Image.Path
                };
                vehicleTypeViewModels.Add(viewModelItem);
            }

            var model = new SearchViewModel
            {
                DateFrom = DateTime.Now,
                DateTo = DateTime.Now.AddDays(1),
                Locations = locationViewModels,
                LocationId = 0,
                VehicleTypes = vehicleTypeViewModels,
                VehicleTypeId = 0
            };

            return model;
        }

        public ActionResult Search()
        {
            var model = GetPreapreSearchModel();

            return View("Search", model);
        }

        [HttpPost]
        public ActionResult Search(SearchViewModel model)
        {
            if (model.DateFrom.Date < DateTime.Now.Date)
            {
                ModelState.AddModelError("", "Date from must be greater than date now or equals.");
            }
            if (model.DateFrom.Date >= model.DateTo.Date)
            {
                ModelState.AddModelError("", "Date to must be greater than date from.");
            }

            if (ModelState.IsValid)
            {
                var dbModel = Mapper.Map<SearchViewModel, SearchModel>(model);
                var results = _searchBusiness.GetResults(dbModel);
                var resultViewModels = Mapper.Map<List<SearchResultModel>, List<SearchResultViewModel>>(results);
                return View("Results", resultViewModels);
            }

            model = GetPreapreSearchModel();

            return View("Search", model);
        }
    }
}