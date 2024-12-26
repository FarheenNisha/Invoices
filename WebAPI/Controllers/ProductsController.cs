using Application.Dtos;
using Application.Extensions;
using Application.Helpers;
using Application.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IDataService _dataService;
        public ProductsController(IDataService dataService)
        {
            _dataService = dataService;
        }
        // GET: api/Products/Get
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var modelVms = await _dataService.ProductsService.Get();

            if (modelVms == null || modelVms.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get failed", "Record not found"));

            return Ok(ApiResponseBuilder.GenerateOK(modelVms, "OK", $"{modelVms.Count} record(s) fatched"));
        }

        // GET api/Products/Get/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id == 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get failed", "Invalid Input"));

            var modelVms = await _dataService.ProductsService.Get(id);
            if (modelVms == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Get failed", $"Record with id {id} not found"));

            return Ok(ApiResponseBuilder.GenerateOK(modelVms, "OK", $"Record with id {modelVms.Id} fatched"));
        }

        // POST api/Products/Create
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto modelDto)
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
            var createdDto = await _dataService.ProductsService.Create(modelDto);
            if (createdDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Create failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(createdDto, "Created Successfully", $"Record created successfully at api/Products/Get/{createdDto.Id}"));
        }

        // PUT api/Products/Update/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto modelDto)
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
            var updatedDto = await _dataService.ProductsService.Update(modelDto);
            if (updatedDto == null)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Update failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(updatedDto, "OK", "Record updated successfully "));
        }
    

        // DELETE api/Products/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Deleted failed", "Invalid Input"));

            var rowsAffected = await _dataService.ProductsService.Delete(id);
            if (rowsAffected <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Deleted failed", "There might be active child record(s)"));

            return Ok(ApiResponseBuilder.GenerateOK(rowsAffected, "OK", $"Record with id {id} deleted"));
        }

        // POST api/Products/CreateRange
        [HttpPost]
        public async Task<IActionResult> CreateRange([FromBody] List<ProductDto> modelDtos)
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
            var createdDto = await _dataService.ProductsService.CreateRange(modelDtos);
            if (createdDto == null || createdDto.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Create failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(createdDto, "OK", "Bulk Created Successfully"));
        }

        // PUT api/Products/UpdateRange
        [HttpPost]
        public async Task<IActionResult> UpdateRange([FromBody] List<ProductDto> modelDtos)
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
            var updatedDto = await _dataService.ProductsService.UpdateRange(modelDtos);
            if (updatedDto == null || updatedDto.Count <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Update failed", "Some error occured"));
            return Ok(ApiResponseBuilder.GenerateOK(updatedDto, "OK", "Bulk updated successfully "));
        }
        // PUT api/Products/DeleteRange
        [HttpPost]
        public async Task<IActionResult> DeleteRange([FromBody] List<ProductDto> modelDtos)
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
            var rowsAffected = await _dataService.ProductsService.DeleteRange(modelDtos);
            if (rowsAffected <= 0)
                return BadRequest(ApiResponseBuilder.GenerateBadRequest("Bulk Delete failed", "There might be active child record(s)"));
            return Ok(ApiResponseBuilder.GenerateOK(rowsAffected, "OK", "Bulk Delete success"));
        }
    }
}
