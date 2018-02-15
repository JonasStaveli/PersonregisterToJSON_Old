using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PersonService;
using Newtonsoft;
using System.Xml;

namespace PersonregisterToJSON.Controllers
{
    [Route("[controller]")]
    public class PersonregisterController : Controller
    {
        // GET personregister
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET personregister/5
        [HttpGet("{id}")]
        public async Task<string> GetAsync(String id)
        {
            //Connect to service and get person
            PersonServiceClient client = new PersonServiceClient();

            LookupParameters lookupParameters = new LookupParameters();
            lookupParameters.NIN = id;
            lookupParameters.Date = null;

            // var xml = @"<Invoice> <Timestamp>1/1/2017 00:01</Timestamp><CustNumber>12345</CustNumber><AcctNumber>54321</AcctNumber></Invoice>"; // test data
            var xml =  await client.GetPersonAsync(lookupParameters);

            await client.CloseAsync();

            //XML to JSON
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.ToString());
            String json = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented, true);

            //return JSON
            return id + " " + json;
        }

        // POST personregister
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT personregister/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE personregister/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
