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
        WorkItemsEntities _context;

        public ItemController()
        {
            _context = new WorkItemsEntities();
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Tuple<IEnumerable<Item>, int> Get(string pageNumber, string type)
        {

            var source = (from customer in _context.Items.Where(x => string.IsNullOrEmpty(type) || x.Type.Equals(type)).
                    OrderBy(a => a.Data)
                          select customer).AsQueryable();


            int count = source.Count();
            int CurrentPage = int.Parse(pageNumber);
            int PageSize = 10;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };
            return new Tuple<IEnumerable<Item>, int>(items, paginationMetadata.totalCount);

        }

    }
}
