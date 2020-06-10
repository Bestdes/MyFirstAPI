using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FinalCapStoneAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using ControllerBase = Microsoft.AspNetCore.Mvc.ControllerBase;
using HttpDeleteAttribute = Microsoft.AspNetCore.Mvc.HttpDeleteAttribute;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace FinalCapStoneAPI.Controllers
{
    [RoutePrefix("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly CarDbContext _context;

        public CarController(CarDbContext context)
        {
            _context = context;
        }

        //GET: api/Car (Going into CarController)
        [HttpGet] //Using the real class instead of the interface IActionResult
        public async Task<ActionResult<List<Car>>> GetCars()
        {
            var cars = await _context.Car.ToListAsync(); //This requires a using statement: using Microsoft.EntityFrameworkCore;
            return cars; // The await keywords stops the complier from leaving this method before all cars are gathered
        }

        //GET:api/Car/1
        [Route("{id:int}")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound(); //This returns a 404 error code if an employee with the given id does not exist in the database
            }
            else
            {
                return car;
            }
        }

        //GET:api/Car/Audi
        [Route("{carMake:string}")]
        [HttpGet("{make}")]
        public async Task<ActionResult<List<Car>>> SearchForCarByMake(string carMake)
        {
            var cars = await _context.Car.ToListAsync();
            List<Car> carList = new List<Car>();
            if (cars == null)
            {
                return NotFound(); //This returns a 404 error code if an employee with the given id does not exist in the database
            }
            else
            {
                foreach (Car car in cars)
                {
                    if(carMake == car.Make)
                    {
                        carList.Add(car);
                    }
                }
                return carList;
            }
        }

        //DELETE: api/car/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var car = await _context.Car.FindAsync(id);
            if (car == null)
            {
                return NotFound();
            }
            else
            {
                _context.Car.Remove(car);
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }

        //POST: api/Employee
        [HttpPost] //We do not to specify anything for the HttpCrud action becuase
        public async Task<ActionResult<Car>> AddCar(Car newCar)
        {
            if (ModelState.IsValid)
            {
                _context.Car.Add(newCar);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar);
                //returns HTTP 201 status code - standard response for HTTP Post methods that create new resources on the server
                //nameof(GetCar) - adds a location to the response, specifies the URI of the newly created employee(AKA where we can access the new employee)
                //C# "nameof" is used to avoid hard-coding the action in the CreatedAtAction call
            }
            else
            {
                return BadRequest(); // This 
            }
        }


        //PUT: api/Employee/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> PutCar(int id, Car updatingCar)
        {
            if (id != updatingCar.Id || !ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                _context.Entry(updatingCar).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
        }


    }

}