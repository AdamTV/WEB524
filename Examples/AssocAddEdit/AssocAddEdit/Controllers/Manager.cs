using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
using AssocAddEdit.EntityModels;
using AssocAddEdit.Models;

namespace AssocAddEdit.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // TODO: 03 - Object mapper definitions

                cfg.CreateMap<EntityModels.Country, Models.CountryBaseViewModel>();

                cfg.CreateMap<EntityModels.Manufacturer, Models.ManufacturerBaseViewModel>();
                cfg.CreateMap<EntityModels.Manufacturer, Models.ManufacturerWithDetailViewModel>();

                cfg.CreateMap<EntityModels.Vehicle, Models.VehicleBaseViewModel>();
                cfg.CreateMap<EntityModels.Vehicle, Models.VehicleWithDetailViewModel>();
                cfg.CreateMap<Models.VehicleAddViewModel, EntityModels.Vehicle>();

                // TODO: 04 - Notice the definitions that help build the HTML Forms
                cfg.CreateMap<Models.VehicleBaseViewModel, Models.VehicleEditFormViewModel>();
                cfg.CreateMap<Models.VehicleWithDetailViewModel, Models.VehicleEditFormViewModel>();

                cfg.CreateMap<EntityModels.Classification, Models.ClassificationBaseViewModel>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add methods below
        // Controllers will call these methods
        // Ensure that the methods accept and deliver ONLY view model objects and collections
        // The collection return type is almost always IEnumerable<T>

        // Suggested naming convention: Entity + task/action
        // For example:
        // ProductGetAll()
        // ProductGetById()
        // ProductAdd()
        // ProductEdit()
        // ProductDelete()

        // ############################################################
        // Country

        // TODO: 07 - Get all country objects
        // Country objects were created with code, and then the code was deleted

        public IEnumerable<CountryBaseViewModel> CountryGetAll()
        {
            return mapper.Map<IEnumerable<Country>, IEnumerable<CountryBaseViewModel>>(ds.Countries.OrderBy(c => c.Name));
        }

        // ############################################################
        // Manufacturer

        public IEnumerable<ManufacturerBaseViewModel> ManufacturerGetAll()
        {
            return mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerBaseViewModel>>(ds.Manufacturers.OrderBy(m => m.Name));
        }

        public IEnumerable<ManufacturerWithDetailViewModel> ManufacturerGetAllWithDetail()
        {
            var c = ds.Manufacturers.Include("Country");

            return mapper.Map<IEnumerable<Manufacturer>, IEnumerable<ManufacturerWithDetailViewModel>>(c.OrderBy(m => m.Name));
        }

        public ManufacturerBaseViewModel ManufacturerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Manufacturers.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Manufacturer, ManufacturerBaseViewModel>(o);
        }

        public ManufacturerWithDetailViewModel ManufacturerGetByIdWithDetail(int id)
        {
            var o = ds.Manufacturers.Include("Country").Include("Vehicles")
                .SingleOrDefault(m => m.Id == id);

            return mapper.Map<Manufacturer, ManufacturerWithDetailViewModel>(o);
        }

        // ############################################################
        // Vehicle

        public IEnumerable<VehicleBaseViewModel> VehicleGetAll()
        {
            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleBaseViewModel>>(ds.Vehicles.OrderBy(v => v.Model).ThenBy(v => v.Trim));
        }

        public IEnumerable<VehicleWithDetailViewModel> VehicleGetAllWithDetail()
        {
            var c = ds.Vehicles.Include("Manufacturer.Country");

            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleWithDetailViewModel>>(c.OrderBy(v => v.Model).ThenBy(v => v.Trim));
        }

        public VehicleBaseViewModel VehicleGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Vehicles.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Vehicle, VehicleBaseViewModel>(o);
        }

        public VehicleWithDetailViewModel VehicleGetByIdWithDetail(int id)
        {
            var o = ds.Vehicles.Include("Manufacturer.Country")
                .SingleOrDefault(v => v.Id == id);

            return mapper.Map<Vehicle, VehicleWithDetailViewModel>(o);
        }

        public VehicleWithDetailViewModel VehicleAdd(VehicleAddViewModel newItem)
        {
            // This method is called from the Vehicles controller...
            // ...AND the Manufacturers controller

            // When adding an object with a required to-one association,
            // MUST fetch the associated object first

            // Attempt to find the associated object
            var manufacturer = ds.Manufacturers.Find(newItem.ManufacturerId);

            if (manufacturer == null)
            {
                return null;
            }
            else
            {
                // Attempt to add the new item
                var addedItem = ds.Vehicles.Add(mapper.Map<VehicleAddViewModel, Vehicle>(newItem));
                
                // Set the associated item property
                addedItem.Manufacturer = manufacturer;
                
                ds.SaveChanges();

                return (addedItem == null) ? null : mapper.Map<Vehicle, VehicleWithDetailViewModel>(addedItem);
            }
        }

        public VehicleWithDetailViewModel VehicleEditMSRP(VehicleEditViewModel newItem)
        {
            // Attempt to fetch the object

            // When editing an object with a required to-one association,
            // MUST fetch its associated object
            var vehicle = ds.Vehicles.Include("Manufacturer")
                .SingleOrDefault(v => v.Id == newItem.Id);

            if (vehicle == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(vehicle).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Vehicle, VehicleWithDetailViewModel>(vehicle);
            }
        }

        public bool VehicleDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Vehicles.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Vehicles.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }


        // ############################################################
        // Country

        // TODO: 07 - Get all country objects
        // Classification objects were created with code, and then the code was deleted

        public IEnumerable<ClassificationBaseViewModel> ClassificationGetAll()
        {
            return mapper.Map<IEnumerable<Classification>, IEnumerable<ClassificationBaseViewModel>>(ds.Classifications.OrderBy(c => c.Name));
        }

    }
}