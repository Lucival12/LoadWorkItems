using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace LoadWorkItemsWebApiService.Controllers
{
    public class ItemController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public IEnumerable<Item> Get()
        {

            using(WorkItemsEntities entities = new WorkItemsEntities())
            {
               return entities.Items.ToList();
            }
        }


    }
}
