//using System;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace Exercise_1.Api.Receptors
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class ReceptorController : ControllerBase
//    {
//        private readonly IReceptorService _receptorService;
//        private readonly IQuery _query;

//        public ReceptorController(IReceptorService receptorService, IQuery query)
//        {
//            _receptorService = receptorService;
//            _query = query;
//        }
        
//        [HttpPost]
//        public async Task<IActionResult> Post([FromBody] RegisterReceptorDto dto)
//        {
//            try
//            {
//                await _receptorService.Register(dto);
//                return Ok(dto);
//            }
//            catch (Exception e)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new Response
//                    {
//                        Status = "Error",
//                        Message = e.Message
//                    });
//            }
//        }

//        [HttpGet]
//        [Route("GetAll")]
//        public async Task<IActionResult> GetAll()
//        {
//            try
//            {
//                var queryResult = await _query.GetAllReceptors();
//                return queryResult.Any() ? Ok(queryResult) : NotFound();
//            }
//            catch (Exception e)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new Response
//                    {
//                        Status = "Error",
//                        Message = e.Message
//                    });
//            }
//        }

//        [HttpPut]
//        public async Task<IActionResult> UpdateUrgencyState(UrgencyUpdateDto urgencyUpdateDto)
//        {
            
//             try
//            {
//                await _receptorService.UpdateUrgencyState(urgencyUpdateDto);
//                return Ok(urgencyUpdateDto);
//            }
//            catch (Exception e)//            {
//                return StatusCode(StatusCodes.Status500InternalServerError,
//                    new Response
//                    {
//                        Status = "Error",
//                        Message = e.Message
//                    });
//            }
//        }
//    }
//}