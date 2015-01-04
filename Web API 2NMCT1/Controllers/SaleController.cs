using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Web_API_2NMCT1.Models;

namespace Web_API_2NMCT1.Controllers
{
    
        [Authorize]
        public class SaleController : ApiController
        {
            public List<Sale> Get()
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                return SaleDA.GetSales(p.Claims);
            }

            public HttpResponseMessage Post(Sale c)
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = SaleDA.InsertSale(c, p.Claims);

                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(id.ToString());
                return message;
            }

            public HttpResponseMessage Put(Sale c)
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                SaleDA.UpdateSale(c, p.Claims);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            public HttpResponseMessage Delete(int id)
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                SaleDA.DeleteSale(id, p.Claims);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

    
}
