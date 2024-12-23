// **************************************************
// WEB524 Project Template V1 == 86dc9265-0167-48ee-a8aa-7bf7b98e4082
// Do not change this header.
// **************************************************

using AutoMapper;
using S2021A2AS.EntityModels;
using S2021A2AS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace S2021A2AS.Controllers
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

                cfg.CreateMap<Track, TrackBaseViewModel>();

                cfg.CreateMap<Invoice, InvoiceBaseViewModel>();
                cfg.CreateMap<Invoice, InvoiceWithDetailViewModel>();

                cfg.CreateMap<InvoiceLine, InvoiceLineBaseViewModel>();
                cfg.CreateMap<InvoiceLine, InvoiceLineWithDetailViewModel>();
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

        public IEnumerable<TrackBaseViewModel> TrackGetAll()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks).OrderBy(track => track.GenreId).ThenBy(track => track.AlbumId).ThenBy(track => track.Name);
        }
        public IEnumerable<TrackBaseViewModel> TrackGetAllBlues()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks).Where(t => t.GenreId == 6).OrderBy(t => t.AlbumId).ThenBy(t => t.Name);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllMikePatton()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks).Where(t => t.Composer?.Contains("Mike Patton") ?? false).OrderBy(t => t.Composer).ThenBy(t => t.Name);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllTop50Longest()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks).OrderByDescending(t => t.Milliseconds).Take(50);
        }

        public IEnumerable<TrackBaseViewModel> TrackGetAllTop50Shortest()
        {
            return mapper.Map<IEnumerable<Track>, IEnumerable<TrackBaseViewModel>>(ds.Tracks).OrderBy(t => t.Milliseconds).Take(50);
        }

        public IEnumerable<InvoiceBaseViewModel> InvoiceGetAll()
        {
            return mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceBaseViewModel>>(ds.Invoices).OrderByDescending(i => i.InvoiceDate);
        }

        public InvoiceBaseViewModel InvoiceGetOne(int id)
        {
            var obj = ds.Invoices.Find(id);

            return obj == null ? null : mapper.Map<Invoice, InvoiceBaseViewModel>(obj);
        }

        public InvoiceWithDetailViewModel InvoiceGetByIdWithDetail(int id)
        {
            var obj = ds.Invoices.Include("Customer.Employee").Include("InvoiceLines.Track.Album.Artist").Include("InvoiceLines.Track.MediaType").Where(i => i.InvoiceId == id).FirstOrDefault();

            return obj == null ? null : mapper.Map<Invoice, InvoiceWithDetailViewModel>(obj);
        }
    }
}