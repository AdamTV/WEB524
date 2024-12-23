// **************************************************
// WEB524 Project Template V1 == b7e91cba-6bf0-4d8d-bd56-1366b118e89c
// Do not change this header.
// **************************************************

using AutoMapper;
using S2021A1AS.EntityModels;
using S2021A1AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A1AS.Controllers
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
                //cfg.CreateMap<SourceType, DestinationType>();

                // Map from Employee design model to EmployeeBaseViewModel.
                cfg.CreateMap<Employee, EmployeeBaseViewModel>();

                // Map from EmployeeAddViewModel to Employee design model.
                cfg.CreateMap<EmployeeAddViewModel, Employee>();

                // Map from EmployeeBaseViewModel to EmployeeEditViewModel.
                cfg.CreateMap<EmployeeBaseViewModel, EmployeeEditViewModel>();

            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }

        // Add your methods and call them from controllers.  Use the suggested naming convention.
        // Ensure that your methods accept and deliver ONLY view model objects and collections.
        // When working with collections, the return type is almost always IEnumerable<T>.

        public IEnumerable<EmployeeBaseViewModel> EmployeeGetAll()
        {
            return mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeBaseViewModel>>(ds.Employees).OrderBy(employee => employee.LastName).ThenBy(employee => employee.FirstName);
        }

        public EmployeeBaseViewModel EmployeeGetById(int id)
        {
            // Attempt to fetch the object.
            var obj = ds.Employees.Find(id);

            // Return the result (or null if not found).
            return obj == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(obj);
        }

        public EmployeeBaseViewModel EmployeeAdd(EmployeeAddViewModel newEmployee)
        {
            // Attempt to add the new item.
            // Notice how we map the incoming data to the Customer design model class.
            var addedItem = ds.Employees.Add(mapper.Map<EmployeeAddViewModel, Employee>(newEmployee));
            ds.SaveChanges();

            // If successful, return the added item (mapped to a view model class).
            return addedItem == null ? null : mapper.Map<Employee, EmployeeBaseViewModel>(addedItem);
        }

        public EmployeeBaseViewModel EmployeeEdit(EmployeeEditViewModel updatedEmployeeInfo)
        {
            var obj = ds.Employees.Find(updatedEmployeeInfo.EmployeeId);

            if (obj == null) return null;

            ds.Entry(obj).CurrentValues.SetValues(updatedEmployeeInfo);
            ds.SaveChanges();

            return mapper.Map<Employee, EmployeeBaseViewModel>(obj);
        }

    }
}