using Newtonsoft.Json;
using PersonregisterToJSON.PersonService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace PersonregisterToJSON.Controllers
{
    public class PersonregisterController : ApiController
    {
        //GET /api/personregister/25065041963
        [HttpGet]
        public HttpResponseMessage Get(String id)
        {
            //Connect to person service
            PersonServiceClient client = new PersonServiceClient();
            client.ClientCredentials.UserName.UserName = "test";
            client.ClientCredentials.UserName.Password = "BF32511";

            //Add id to lookupparameters and fetch data
            LookupParameters lookupParameters = new LookupParameters();
            lookupParameters.NIN = id;
            Person xml = client.GetPerson(lookupParameters);

            client.Close();

            //serialize Person data to string
            var stringwriter = new System.IO.StringWriter();
            var serializer = new XmlSerializer(xml.GetType());
            serializer.Serialize(stringwriter, xml);

            //convert from xml to json
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(stringwriter.ToString());

            var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, false);

            //return json
            var res = Request.CreateResponse(HttpStatusCode.OK);
            res.Content = new StringContent(json, Encoding.UTF8, "application/json");

            return res;
        }
    }
}
