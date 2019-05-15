using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreWebAPI
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ValueService _valueService;
        private readonly PersonService _personService;

        public ValuesController(ValueService valueService, PersonService personService)
        {
            _valueService = valueService;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Ok();
            return Ok(_valueService.GetValues());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_valueService.GetValueById(id));
        }

        [HttpPost("{id}")]
        public IActionResult Post(int id)
        {
            _valueService.AddNumber(id);
            return Ok();
        }

        [HttpGet("person")]
        public IActionResult GetPeople()
        {
            return Ok(_personService.GetAll());
        }

        [HttpGet("person/{id}")]
        public IActionResult GetPerson(int id)
        {
            var person = _personService.GetById(id);

            if (person == null)
                return NotFound();

            return Ok(_personService.GetById(id));
        }

        [HttpPost("person")]
        public IActionResult PostPerson([FromBody] Person person)
        {
            if (person == null)
                return BadRequest();

            var addPerson = _personService.AddPerson(person);

            if (addPerson == null)
                return BadRequest();

            return Ok(person);
        }
    }



    public class ValueService
    {
        private List<int> _values;

        public ValueService()
        {
            _values = new List<int>() { 1, 4, 10, 2, 8 };
        }

        public List<int> GetValues()
        {
            return _values;
        }

        public int GetValueById(int id)
        {
            //int match;
            //match =_values.Find(id);

            //_values.First(whatever => whatever == id);
            // FIRST  WHERE  (Eventually)SINGLE   (Later)SELECT + "Serivce repo pattern"
            return _values.First(whatever => whatever == id);
        }

        public List<int> GetValuesGreatherThan(int someValue)
        {
            return _values.Where(x => x > someValue).ToList();
        }

        public void AddNumber(int number)
        {
            _values.Add(number);
        }
    }

    public class PersonService
    {
        private List<Person> _people;

        public PersonService()
        {
            _people = new List<Person>() { new Person() { Id = 1, Age = 20, Name = "Chris" }, new Person() { Id = 2, Age = 30, Name = "Scott" }, new Person() { Id = 3, Age = 40, Name = "Keating" } };
        }

        public List<Person> GetAll()
        {
            return _people;
        }

        public Person GetById(int id)
        {
            return _people.FirstOrDefault(person => person.Id == id);
        }

        public Person AddPerson(Person person)
        {
            _people.Add(person);

            return person;
        }
    }

    // Model
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
/*
 List<Employee> employees = new List<Employee>()
            {
                new Employee() { ID = 0, First = "work", Last = "the", Email = "problem", Password ="!" },
                new Employee() { ID = 1, First = "code", Last = "flow", Email = "is", Password ="easy" },
                new Employee() { ID = 2, First = "alpha", Last = "beta", Email = "charlie", Password ="delta" }
            };
*/
