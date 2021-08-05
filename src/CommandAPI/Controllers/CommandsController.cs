using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CommandAPI.Data;
using CommandAPI.Models;
using AutoMapper;
using CommandAPI.Dtos;


namespace CommandAPI.Controllers
{
    /*  [Route("api/commands")]*/
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ICommandAPIRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandAPIRepo repository, IMapper mapper)   
        {
            _repository = repository;   //dependencies injected here
            _mapper = mapper;  //An instance of IMapper will be injected by the DI system into our constructor.
        }

        /*
        [HttpGet]
        public ActionResult<IEnumerable<string>>Get()
        {
            return new string[] {"this", "is","hard", "coded"};
        }*/
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commandItems = _repository.GetAllCommands();
            //return Ok(commandItems);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));

        }

        [HttpGet("{id}", Name = "GetCommandById")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var commandItem =_repository.GetCommandById(id);
            if (commandItem == null)
            {
                return NotFound();
            }
            // return Ok(commandItem);
            return Ok(_mapper.Map<CommandReadDto>(commandItem));

        }


        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand (CommandCreateDto commandCreateDto)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDto = _mapper.Map<CommandReadDto>
            (commandModel);
            return CreatedAtRoute(nameof(GetCommandById),
            new { Id = commandReadDto.Id }, commandReadDto);
        }


        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id,CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo =  _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto,commandModelFromRepo);

            /*
             This is where the actual update occurs! We use a slightly different form
of the Map method on our _mapper instance to map the DTO to our
Command. By reference the Command object is updated in the DB
Context. Again, this is not yet re
            
             */
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();// nothing  rrequired here
            return NoContent();
        }



    }
}
