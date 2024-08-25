using Microsoft.AspNetCore.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UniversityProject.Models;
using UniversityProject.Models.DTO;

namespace UniversityProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public UniversityController(ApiContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public JsonResult Create(UniversityDTO uniDTO)
        {
            string Msg = string.Empty;
            var response = new ApiResponse { };
            University university = new University();

            if (string.IsNullOrEmpty(uniDTO.Id))
            {
                Msg += "Id cannot be blank." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(uniDTO.Name))
            {
                Msg += "Name cannot be blank." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(uniDTO.Country))
            {
                Msg += "Country cannot be blank." + Environment.NewLine;
            }

            if (string.IsNullOrEmpty(Msg))
            {
           

                university = _mapper.Map<University>(uniDTO);
                university.IsBookmark = false;
                university.Created = DateTime.Now;
                university.LastModeified = DateTime.Now;
                university.IsActive = true;

                _context.Universities.Add(university);

                _context.SaveChanges();
                response.Message = "Record has successfully created.";
                response.Status = "Success";
                response.Data = uniDTO;

                return new JsonResult(Ok(response));
            
            }

            response.Message = Msg;
            response.Status = "Fail";
            response.Data = null;

            return new JsonResult(Ok(response));

        }

        [HttpGet("GetAll")]
        public IActionResult GetAll([FromQuery] string? Id, [FromQuery] string? Name, [FromQuery] string? country, [FromQuery] string? webpage)
        {
            var university = _context.Universities.AsQueryable();

            if (!string.IsNullOrEmpty(Id))
            {
                university = university.Where(a => a.Id.Contains(Id));
            }

            if (!string.IsNullOrEmpty(Name))
            {
                university = university.Where(a => a.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(country))
            {
                university = university.Where(a => a.Country.Contains(country));
            }

            if (!string.IsNullOrEmpty(webpage))
            {
                university = university.Where(a => a.Webpages.Contains(webpage));
            }

            return Ok(university.ToList());
        }

        [HttpGet("{id}")]
        public IActionResult GetId(string id)
        {
            UniversityDTO dto = new UniversityDTO();
            var response = new ApiResponse { };
            var university = _context.Universities.Find(id);

            if (university == null)
            {
                response.Message = "No record found.";

            }
            else
            {
                dto = _mapper.Map<UniversityDTO>(university);
                response.Message = "Record found.";
                response.Data = dto;
            }

            

            return Ok(response);
        }

        [HttpPut("{Id}")]
        public IActionResult Update(string Id, [FromBody] UniversityDTO dto)
        {
            var result = _context.Universities.Find(Id);
            var response = new ApiResponse { };

            if (result == null)
            {
                response.Message = "No record found.";
            }
            else
            {
                result.Name = dto.Name;
                result.Webpages = dto.Webpages;
                result.Country = dto.Country;
                result.LastModeified = DateTime.Now;

                _context.SaveChanges();
                response.Message = "Record updated.";
                response.Status = "Success";
                response.Data = result;

            }

            return Ok(response);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(string Id) 
        {
            var result = _context.Universities.Find(Id);
            var response = new ApiResponse { };

            if (result == null)
            {
                response.Message = "No record found.";
            }
            else
            {
                _context.Universities.Remove(result);
                _context.SaveChanges();

                response.Message = "Record has been removed.";
                response.Status = "Success";
            }
            return Ok(response);
        }

        [HttpPost("bookmark/{Id}")]
        public IActionResult BookMark(string Id)
        {
            var result = _context.Universities.Find(Id);
            var response = new ApiResponse { };

            if (result == null)
            {
                response.Message = "No record found.";
            }
            else
            {
                if (result.IsBookmark)
                {
                    result.IsBookmark = false;
                    response.Message = "Unbookmarked";
                }
                else
                {
                    result.IsBookmark = true;
                    response.Message = "Bookmarked";
                }
                _context.SaveChanges();

                
                response.Status = "Success";
                response.Data = new { result.Id, result.IsBookmark };
            }

            return Ok(response);

        }
    }
}
