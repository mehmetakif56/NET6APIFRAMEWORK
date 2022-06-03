﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TTBS.Core.Entities;
using TTBS.Models;
using TTBS.Services;

namespace TTBS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StenografController : BaseController<StenografController>
    {
        private readonly IStenografService _stenoService;
        private readonly ILogger<StenografController> _logger;
        public readonly IMapper _mapper;

        public StenografController(IStenografService stenoService, ILogger<StenografController> logger, IMapper mapper)
        {
            _stenoService = stenoService;
            _logger = logger;
            _mapper = mapper;
        }
        #region StenoPlan
        [Microsoft.AspNetCore.Authorization.AllowAnonymous]
        [HttpGet("GetStenoPlan")]
        public IEnumerable<StenoPlanModel> GetStenoPlan()
        {
            var stenoEntity = _stenoService.GetStenoPlan();
            var model = _mapper.Map<IEnumerable<StenoPlanModel>>(stenoEntity);
            return model;
        }

        [HttpPost("CreateStenoPlan")]
        public IActionResult CreateStenoPlan(StenoPlanOlusturModel model)
        {
            try
            {
                var entity = Mapper.Map<StenoPlan>(model);
                _stenoService.CreateStenoPlan(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok();

        }

        [HttpDelete("DeleteStenoPlan")]
        public IActionResult DeleteStenoPlan(Guid id)
        {
            try
            {
                _stenoService.DeleteStenoPlan(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }

        [HttpGet("GetStenoPlanByStatus")]
        public List<StenoPlanModel> GetStenoPlanByStatus(int status=0)
        {
            var stenoEntity = _stenoService.GetStenoPlanByStatus(status);
            var model = _mapper.Map<List<StenoPlanModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoPlanByDateAndStatus")]
        public List<StenoPlanModel> GetStenoPlanByDateAndStatus(DateTime gorevTarihi, DateTime gorevBitTarihi, int gorevTuru)
        {
            var stenoEntity = _stenoService.GetStenoPlanByDateAndStatus(gorevTarihi, gorevBitTarihi, gorevTuru);
            var model = _mapper.Map<List<StenoPlanModel>>(stenoEntity);
            return model;
        }

        #endregion 
        #region StenoIzin
        [HttpGet("GetAllStenoIzin")]
        public IEnumerable<StenoIzinModel> GetAllStenoIzin()
        {
            var stenoEntity = _stenoService.GetAllStenoIzin();
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinByStenografId")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByStenografId(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoIzinByStenografId(id);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinByName")]
        public IEnumerable<StenoIzinModel> GetStenoIzinByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoIzinByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoIzinBetweenDateAndStenograf")]
        public IEnumerable<StenoIzinModel> GetStenoIzinBetweenDateAndStenograf(DateTime basTarihi,DateTime bitTarihi,Guid? stenograf)
        {
            var stenoEntity = _stenoService.GetStenoIzinBetweenDateAndStenograf(basTarihi, bitTarihi, stenograf);
            var model = _mapper.Map<IEnumerable<StenoIzinModel>>(stenoEntity);
            return model;
        }

        [HttpPost("CreateStenoIzin")]
        public IActionResult CreateStenoIzin(StenoIzinModel model)
        {
            var entity = Mapper.Map<StenoIzin>(model);
            _stenoService.CreateStenoIzin(entity);
            return Ok(entity);

        }
        #endregion

        #region StenoGorev

        [HttpGet("GetStenoGorevById")]
        public IEnumerable<StenoGorevModel> GetStenoGorevById(Guid id)
        {
            var stenoEntity = _stenoService.GetStenoGorevById(id);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByPlanId")]
        public List<StenoGorevModel> GetStenoGorevByPlanId(Guid planId)
        {
            var stenoEntity = _stenoService.GetStenoGorevByPlanId(planId);
            var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByName")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByName(string adSoyad)
        {
            var stenoEntity = _stenoService.GetStenoGorevByName(adSoyad);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByDateAndStatus")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByDateAndStatus(DateTime gorevAtamaTarihi, int status)
        {
            var stenoEntity = _stenoService.GetStenoGorevByDateAndStatus(gorevAtamaTarihi, status);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetStenoGorevByStenografAndDate")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByStenografAndDate(Guid stenografId, DateTime gorevBasTarihi, DateTime gorevBitTarihi)
        {
            var stenoEntity = _stenoService.GetStenoGorevByStenografAndDate(stenografId, gorevBasTarihi, gorevBitTarihi);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }
        [HttpGet("GetStenoGorevByPlanDateAndStatus")]
        public IEnumerable<StenoGorevPlanModel> GetStenoGorevByPlanDateAndStatus(DateTime gorevTarihi, int gorevturu)
        {
            var stenoEntity = _stenoService.GetStenoGorevByPlanDateAndStatus(gorevTarihi, gorevturu);
            var model = _mapper.Map<IEnumerable<StenoGorevPlanModel>>(stenoEntity);
            return model;
        }

        //[HttpPost("CreateStenoGorev")]
        //public IActionResult CreateStenoGorev(List<StenoGorevModel> model)
        //{
        //    try
        //    {
        //        foreach (var item in model)
        //        {
        //            var entity = Mapper.Map<StenoGorev>(item);
        //            _stenoService.CreateStenoGorev(entity);                   
        //        }
        //    }
        //    catch(Exception ex)
        //    { return BadRequest(ex.Message); }

        //    return Ok();


        //}

        [HttpPost("CreateStenoGorevAtama")]
        public IActionResult CreateStenoGorevAtama(StenoGorevAtamaModel model)
        {
            try
            {
                    var entity = Mapper.Map<StenoGorev>(model);
                    _stenoService.CreateStenoGorev(entity);             
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetStenoGorevByStatus")]
        public List<StenoGorevModel> GetStenoGorevByStatus(int status=0)
        {
            var stenoEntity = _stenoService.GetStenoGorevBySatatus(status);
            var model = _mapper.Map<List<StenoGorevModel>>(stenoEntity);
            return model;
        }
        #endregion

        # region Stenograf
        [HttpGet("GetAllStenografByGroupId")]
        public IEnumerable<StenoModel> GetAllStenografByGroupId(Guid? groupId)
        {
            var stenoEntity = _stenoService.GetAllStenografByGroupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }

        [HttpGet("GetAllStenografByGorevTuru")]
        public IEnumerable<StenoModel> GetAllStenografByGorevTuru(int gorevTuru)
        {
            var stenoEntity = _stenoService.GetAllStenografByGorevTuru(gorevTuru);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoEntity);
            return model;
        }
        [HttpPost("CreateStenograf")]
        public IActionResult CreateStenograf(StenoModel model)
        {
            try
            {
                var entity = Mapper.Map<Stenograf>(model);
                _stenoService.CreateStenograf(entity);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpGet("GetStenoGorevByGrupId")]
        public IEnumerable<StenoGorevModel> GetStenoGorevByGrupId(Guid groupId)
        {
            var stenoGrpEntity = _stenoService.GetStenoGorevByGrupId(groupId);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoGrpEntity);
            return model;
        }
        [HttpDelete("DeleteStenoGorev")]
        public IActionResult DeleteStenoGorev(Guid stenoGorevId)
        {
            try
            {
                _stenoService.DeleteStenoGorev(stenoGorevId);
            }
            catch (Exception ex)
            { return BadRequest(ex.Message); }

            return Ok();
        }

        [HttpPost("CreateStenoGroup")]
        public IActionResult CreateStenoGroup(StenoGrupModel model)
        {

            var entity = Mapper.Map<StenoGrup>(model);
            _stenoService.CreateStenoGroup(entity);
            return Ok();
        }

        [HttpDelete("DeleteStenoGroup")]
        public IActionResult DeleteStenoGroup(StenoGrupModel model)
        {

            var entity = Mapper.Map<StenoGrup>(model);
            _stenoService.DeleteStenoGroup(entity);
            return Ok();
        }

        [HttpGet("GetAllStenoGrupNotInclueded")]
        public IEnumerable<StenoModel> GetAllStenoGrupNotInclueded()
        {
            var stenoGrpEntity = _stenoService.GetAllStenoGrupNotInclueded();
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
            return model;
        }

        [HttpGet("GetAvaliableStenoBetweenDateBySteno")]
        public IEnumerable<StenoModel> GetAvaliableStenoBetweenDateBySteno(DateTime basTarihi, DateTime bitTarihi,int gorevTuru, int toplantiTur)
        {
            var stenoGrpEntity = _stenoService.GetAvaliableStenoBetweenDateBySteno(basTarihi, bitTarihi, gorevTuru, toplantiTur);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
            return model;
        }

        [HttpGet("GetAvaliableStenoBetweenDateByGroup")]
        public IEnumerable<StenoModel> GetAvaliableStenoBetweenDateByGroup(DateTime basTarihi, DateTime bitTarihi, Guid groupId, int toplantiTur)
        {
            var stenoGrpEntity = _stenoService.GetAvaliableStenoBetweenDateByGroup(basTarihi, bitTarihi, groupId, toplantiTur);
            var model = _mapper.Map<IEnumerable<StenoModel>>(stenoGrpEntity);
            return model;
        }
        [HttpPut("UpdateStenoPlan")]
        public IActionResult UpdateStenoPlan(StenoPlanGüncelleModel model)
        {

            var entity = Mapper.Map<StenoPlan>(model);
            _stenoService.UpdateStenoPlan(entity);
            return Ok();
        }
        [HttpGet("GetAssignedStenoByPlanIdAndGrorevTur")]
        public IEnumerable<StenoGorevModel> GetAssignedStenoByPlanIdAndGrorevTur(Guid planId, int gorevTuru)
        {
            var stenoEntity = _stenoService.GetAssignedStenoByPlanIdAndGrorevTur(planId, gorevTuru);
            var model = _mapper.Map<IEnumerable<StenoGorevModel>>(stenoEntity);
            return model;
        }

        [HttpPut("UpdateStenoSiraNo")]
        public IActionResult UpdateStenoSiraNo(List<StenoModel> model)
        {
            try
            {
                var entityList = Mapper.Map<List<Stenograf>>(model);
                _stenoService.UpdateStenoSiraNo(entityList);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
        #endregion

    }
}
