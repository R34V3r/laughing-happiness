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
        public class OrganisationController : ApiController
        {
            public List<Organisation> Get()
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                return OrganisationDA.GetOrganisations(p.Claims);
            }

            public HttpResponseMessage Post(Organisation c)
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                int id = OrganisationDA.InsertOrganisation(c, p.Claims);

                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(id.ToString());
                return message;
            }

            public HttpResponseMessage Put(Organisation c)
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                OrganisationDA.UpdateOrganisation(c, p.Claims);

                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            public HttpResponseMessage Delete(int id)
            {
                ClaimsPrincipal p = RequestContext.Principal as ClaimsPrincipal;
                OrganisationDA.DeleteOrganisation(id, p.Claims);
                return new HttpResponseMessage(HttpStatusCode.OK);
            }
        }

    
}
