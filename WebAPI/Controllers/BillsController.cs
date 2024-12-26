using Application.Dtos;
using Application.Extensions;
using Application.Helpers;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly IDataService _dataService;
        public BillsController(IDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/Bills/Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelVms = await _dataService.BillsService.Get();

            if (modelVms == null || modelVms.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get failed", "Record not found"));

            return Ok(ApiResponseBuilder.GenerateOK(modelVms, "OK", $"{modelVms.Count} record(s) fatched"));
        }

        // GET api/Bills/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            throw new Exception("Test Exception", new Exception("Test Inner Exception"));
            if (id == 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get failed", "Invalid Input"));

            var modelVms = await _dataService.BillsService.Get(id);
            if (modelVms == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get failed", $"Record with id {id} not found"));

            return Ok(ApiResponseBuilder.GenerateOK(modelVms, "OK", $"Record with id {modelVms.Id} fatched"));
        }

        // POST api/Bills/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillDto modelDto)
        {
            if (modelDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create failed", "Input not valid or null"));
            
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if (errors == null || errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create failed", msgBuilder.ToString()));
                }
            }
            var createdDto = await _dataService.BillsService.Create(modelDto);
            if (createdDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(createdDto, "Created Successfully", $"Record created successfully at api/Bills/Get/{createdDto.Id}"));
        }

        // PUT api/Bills/Update/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BillDto modelDto)
        {
            if (id == 0 || modelDto == null || modelDto.Id != id)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Update failed", "Invalid Input"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if (errors == null || errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Update failed", msgBuilder.ToString()));
                }
            }
            var updatedDto = await _dataService.BillsService.Update(modelDto);
            if (updatedDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Update failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(updatedDto, "OK", "Record updated successfully "));
        }
    

        // DELETE api/Bills/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Deleted failed", "Invalid Input"));

            var rowsAffected = await _dataService.BillsService.Delete(id);
            if (rowsAffected <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Deleted failed", "There might be active child record(s)"));

            return Ok(ApiResponseBuilder.GenerateOK(rowsAffected, "OK", $"Record with id {id} deleted"));
        }

        // POST api/Bills/CreateRange
        [HttpPost]
        public async Task<IActionResult> CreateRange([FromBody] List<BillDto> modelDtos)
        {
            if (modelDtos == null || modelDtos.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Create failed", "Input is null"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if (errors == null || errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Create failed", msgBuilder.ToString()));
                }
            }
            var createdDto = await _dataService.BillsService.CreateRange(modelDtos);
            if (createdDto == null || createdDto.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Create failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(createdDto, "OK", "Bulk Created Successfully"));
        }

        // PUT api/Bills/UpdateRange
        [HttpPost]
        public async Task<IActionResult> UpdateRange([FromBody] List<BillDto> modelDtos)
        {
            if (modelDtos == null || modelDtos.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Update failed", "Input is null"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if (errors == null || errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Update failed", msgBuilder.ToString()));
                }
            }
            var updatedDto = await _dataService.BillsService.UpdateRange(modelDtos);
            if (updatedDto == null || updatedDto.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Update failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(updatedDto, "OK", "Bulk updated successfully "));
        }
        // PUT api/Bills/DeleteRange
        [HttpPost]
        public async Task<IActionResult> DeleteRange([FromBody] List<BillDto> modelDtos)
        {
            if (modelDtos == null || modelDtos.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Delete failed", "Input is null"));

            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetModelStateError();
                if (errors == null || errors.Count > 0)
                {
                    var msgBuilder = new StringBuilder();
                    foreach (var error in errors)
                        msgBuilder.AppendLine(error.ToString());
                    return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Delete failed", msgBuilder.ToString()));
                }
            }
            var rowsAffected = await _dataService.BillsService.DeleteRange(modelDtos);
            if (rowsAffected <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Delete failed", "There might be active child record(s)"));
            return Ok(ApiResponseBuilder.GenerateOK(rowsAffected, "OK", "Bulk Delete success"));
        }
    }
}
