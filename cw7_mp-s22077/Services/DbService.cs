using cw7_mp_s22077.Models;
using cw7_mp_s22077.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cw7_mp_s22077.Services
{
    public class DbService : IDbService
    {
        private readonly localhostContext _dbContext;
        public DbService(localhostContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SomeSortOfTrip>> GetTrips()
        {
            return await _dbContext.Trips
                .Include(e => e.CountryTrips)
                .Select(e => new SomeSortOfTrip
                {
                    Name = e.Name,
                    Description = e.Description,
                    MaxPeople = e.MaxPeople,
                    DateFrom = e.DateFrom,
                    DateTo = e.DateTo,
                    Countries = e.CountryTrips.Select(e => new SomeSortOfCountry
                    {
                        Name = e.IdCountryNavigation.Name
                    }).ToList(),
                    Clients = e.ClientTrips.Select(e => new SomeSortOfClient
                    {
                        FirstName = e.IdClientNavigation.FirstName,
                        LastName = e.IdClientNavigation.LastName
                    }).ToList()
                })
                .OrderByDescending(e => e.DateFrom)
                .ToListAsync();
        }

        public async Task<bool> RemoveClient(int id)
        {
            //var trip = await _dbContext.Trips.Where(e => e.IdTrip == id).FirstOrDefaultAsync();

            //dodawanie
            /*var addTrip = new Trip { IdTrip = id, Name = "nazwaWycieczki" };
            _dbContext.Add(addTrip);

            //edycja
            var editTrip = await _dbContext.Trips.Where(e => e.IdTrip == id).FirstOrDefaultAsync();
            editTrip.Name = "aaa";*/

            var client = new Client() { IdClient = id };
            var clientTrips = _dbContext.Clients
                .Where(c => c.IdClient == id)
                .Select(c => c.ClientTrips)
                .ToList();

            if (clientTrips.Count != 0)
            {
                return false;
            }

            _dbContext.Attach(client);
            _dbContext.Remove(client);

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<string> AssignClient(SomeSortOfClientTrip someSortOfClientTrip)
        {
            var client = await _dbContext.Clients
                .Where(c => c.Pesel.Equals(someSortOfClientTrip.Pesel)).FirstOrDefaultAsync();


            Client addClient = new Client
            {
                FirstName = someSortOfClientTrip.FirstName,
                LastName = someSortOfClientTrip.LastName,
                Email = someSortOfClientTrip.Email,
                Telephone = someSortOfClientTrip.Telephone,
                Pesel = someSortOfClientTrip.Pesel
            };

            ClientTrip clientTrip = new ClientTrip
            {
                IdClient = addClient.IdClient,
                IdTrip = someSortOfClientTrip.IdTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = null,
                IdClientNavigation = addClient,
                IdTripNavigation = _dbContext.Trips.Where(t => t.IdTrip == someSortOfClientTrip.IdTrip).FirstOrDefault()
            };

            if (client == default)
            {
                _dbContext.Add(addClient);
                _dbContext.Add(clientTrip);
            }


            var tripClients = await _dbContext.ClientTrips
                .Where(t => t.IdTripNavigation.IdTrip == someSortOfClientTrip.IdTrip && t.IdClientNavigation.Pesel.Equals(someSortOfClientTrip.Pesel)).FirstOrDefaultAsync();

            if (tripClients != default)
                return "The client is already signed up for the trip";

            var trip = await _dbContext.Trips
                .Where(t => t.IdTrip == someSortOfClientTrip.IdTrip).FirstOrDefaultAsync();

            if (trip == default)
                return "No such trip exists";


            await _dbContext.SaveChangesAsync();

            return "Ok";
        }
    }
}

