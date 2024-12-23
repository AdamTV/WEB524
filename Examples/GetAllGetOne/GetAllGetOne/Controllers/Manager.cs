using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GetAllGetOne.EntityModels;
using GetAllGetOne.Models;

namespace GetAllGetOne.Controllers
{
    public class Manager
    {
        // Reference to the data context
        private DataContext ds = new DataContext();

        // AutoMapper instance
        public IMapper mapper;

        public Manager()
        {
            // If necessary, add more constructor code here...

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Map from Customer design model to CustomerBaseViewModel.
                cfg.CreateMap<Customer, CustomerBaseViewModel>();

                // Map from CustomerAddViewModel to Customer design model.
                cfg.CreateMap<CustomerAddViewModel, Customer>();
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

        public IEnumerable<CustomerBaseViewModel> CustomerGetAll()
        {
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerBaseViewModel>>(ds.Customers);
        }

        public CustomerBaseViewModel CustomerGetById(int id)
        {
            // Attempt to fetch the object.
            var obj = ds.Customers.Find(id);

            // Return the result (or null if not found).
            return obj == null ? null : mapper.Map<Customer, CustomerBaseViewModel>(obj);
        }

        public CustomerBaseViewModel CustomerAdd(CustomerAddViewModel newCustomer)
        {
            // Attempt to add the new item.
            // Notice how we map the incoming data to the Customer design model class.
            var addedItem = ds.Customers.Add(mapper.Map<CustomerAddViewModel, Customer>(newCustomer));
            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Customer, CustomerBaseViewModel>(addedItem);
        }
    }
}