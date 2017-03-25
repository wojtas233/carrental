using CarRental.Business.Models;
using CarRental.WebApp.Areas.AdminPanel.Models;
using CarRental.WebApp.Areas.UserPanel.Models;
using ExpressMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WebApp.App_Start
{
    public class ExpressMapperConfig
    {
        public static void RegisterMapping()
        {
            // Business to Web
            Mapper.Register<LocationModel, Areas.AdminPanel.Models.LocationViewModel>();
            Mapper.Register<LocationModel, Areas.UserPanel.Models.LocationViewModel>();
            Mapper.Register<AvailabilityModel, AvailabilityViewModel>();
            Mapper.Register<VehicleModel, VehicleViewModel>();
            Mapper.Register<AvailabilityModel, AvailabilityDetailsViewModel>();
            Mapper.Register<UserModel, UserViewModel>();
            Mapper.Register<ReservationModel, ReservationDetailsViewModel>();
            Mapper.Register<ReservationEquipmentModel, ReservationEquipmentListItemVM>();
            Mapper.Register<SearchReservationDetailsModel, Models.ReservationDetailsViewModel>();

            // Web to Business
            Mapper.Register<Areas.AdminPanel.Models.LocationViewModel, LocationModel>();
            Mapper.Register<Areas.UserPanel.Models.LocationViewModel, LocationModel>();
            Mapper.Register<AvailabilityViewModel, AvailabilityModel>();
            Mapper.Register<VehicleViewModel, VehicleModel>();
            Mapper.Register<ReservationDetailsViewModel, ReservationModel>();
            Mapper.Register<ReservationEquipmentListItemVM, ReservationEquipmentModel>();
            Mapper.Register<ModifyReservationEquipmentVM, ReservationEquipmentModel>();
            Mapper.Register<Models.ReservationDetailsViewModel, SearchReservationDetailsModel>();

            // Web to Web
            Mapper.Register<Models.ReservationDetailsViewModel, ReservationDetailsViewModel>();

            Mapper.Compile();
        }
    }
}