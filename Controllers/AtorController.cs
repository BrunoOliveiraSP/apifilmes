using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;


namespace apifilmes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AtorController : ControllerBase
    {

        [HttpPost]
        public Models.TbAtor Salvar(Models.TbAtor ator)
        {
            Models.apidbContext ctx = new Models.apidbContext();
            
            ctx.TbAtor.Add(ator);
            ctx.SaveChanges();

            return ator;
        }


        
    }
}