using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
// new...
using AutoMapper;
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
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Employee, EmployeeBase>();

                // Attention 01 - Object mapper definitions

                // Attention 11 - Error - mapping error, missing AutoMapper map

                //cfg.CreateMap<EntityModels.Customer, Controllers.CustomerBaseViewModel>();
                cfg.CreateMap<Models.CustomerBaseViewModel, Models.CustomerEditContactInfoFormViewModel>();
                cfg.CreateMap<Models.CustomerAddViewModel, EntityModels.Customer>();
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

        // Use AutoMapper to map objects, source to target
        public IEnumerable<CustomerBaseViewModel> CustomerGetAll()
        {
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerBaseViewModel>>(ds.Customers);
        }

        public CustomerBaseViewModel CustomerGetById(int id)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Find(id);

            // Return the result, or null if not found
            return (o == null) ? null : mapper.Map<Customer, CustomerBaseViewModel>(o);
        }

        public CustomerBaseViewModel CustomerAdd(CustomerAddViewModel newItem)
        {
            // Attempt to add the new item
            // Notice how we map the incoming data to the design model object
            var addedItem = ds.Customers.Add(mapper.Map<CustomerAddViewModel, Customer>(newItem));
            ds.SaveChanges();

            // If successful, return the added item, mapped to a view model object
            return (addedItem == null) ? null : mapper.Map<Customer, CustomerBaseViewModel>(addedItem);
        }

        public CustomerBaseViewModel CustomerEditContactInfo(CustomerEditContactInfoViewModel newItem)
        {
            // Attempt to fetch the object
            var o = ds.Customers.Find(newItem.CustomerId);

            if (o == null)
            {
                // Problem - item was not found, so return
                return null;
            }
            else
            {
                // Update the object with the incoming values
                ds.Entry(o).CurrentValues.SetValues(newItem);
                ds.SaveChanges();

                // Prepare and return the object
                return mapper.Map<Customer, CustomerBaseViewModel>(o);
            }
        }

        public bool CustomerDelete(int id)
        {
            // Attempt to fetch the object to be deleted
            var itemToDelete = ds.Customers.Find(id);

            if (itemToDelete == null)
            {
                return false;
            }
            else
            {
                // Remove the object
                ds.Customers.Remove(itemToDelete);
                ds.SaveChanges();

                return true;
            }
        }
    }
}