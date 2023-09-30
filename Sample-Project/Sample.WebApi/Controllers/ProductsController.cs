using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Sample.WebApi.DTO;
using Sample.WebApi.Models;
using System.Net.NetworkInformation;
using System;
using System.Xml;
using System.Xml.Serialization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        // GET: api/<ProductsController>
        private readonly App_BlazorDBContext _app_BlazorDBContext;
        private readonly IMapper mapper;
        private readonly ILogger<ProductsController> logger;
        private IConfiguration _Configuration;
        public ProductsController(IConfiguration Configuration, App_BlazorDBContext app_BlazorDBContext, ILogger<ProductsController> logger, IMapper mapper)
        {
            this.mapper = mapper;
            this.logger = logger;
            _app_BlazorDBContext = app_BlazorDBContext;
            _Configuration = Configuration;
        }
        [HttpGet]
        [Route("GetProducts")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get()
        {
            List<ProductDTO> productlist = new List<ProductDTO>();
            try
            {
                string connectionString = _Configuration.GetConnectionString("DefaultConnection");
                string query = "SELECT * FROM Products FOR XML PATH('item'), ROOT('root')";

                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            reader.Read();
                            string xmlData = reader[0].ToString();
                            // Now you have the XML data in the "xmlData" string.
                            productlist = ParseAndBindXmlToModel(xmlData);
                        }
                        reader.Close();
                    }
                }
                //var list = await _app_BlazorDBContext.Products.ToListAsync();
                if (productlist == null)
                {
                    logger.LogError($"Record Not Found:{nameof(Get)}");
                    return NotFound();
                }
                // productlist = (List<ProductDTO>)mapper.Map<IEnumerable<ProductDTO>>(list);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,$"Error performing GetProduct in {nameof(Get)}");
            }
            return Ok(productlist);
        }

        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
      
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            ProductDTO products = new ProductDTO();
            try
            {
            var product = await _app_BlazorDBContext.Products.FindAsync(id);
            if (product == null)
            {
                logger.LogError($"Record Not Found:{nameof(Get)}-ID:{id}");
                return NotFound();
            }
             products = mapper.Map<ProductDTO>(product);
            }
            catch (Exception ex)
            {

                logger.LogError(ex, $"Error performing GetProduct By Id in {nameof(Get)}-ID:{id}");
            }
            return Ok(products);
        }

        // POST api/<ProductsController>
        [HttpPost]
        [Route("AddProduct")]
        [Authorize]
        public async Task<ActionResult<ProductDTO>> Post(ProductDTO productDTO)
        {
            try
            {
                if (productDTO == null)
                {
                    logger.LogError($"Product Info is required:{nameof(Post)}");
                    return NotFound();
                }
                var product = mapper.Map<Product>(productDTO);
                await _app_BlazorDBContext.Products.AddAsync(product);
                await _app_BlazorDBContext.SaveChangesAsync();
                return CreatedAtAction(nameof(Post), new { id = product.ProductId }, product);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing save Product in {nameof(Post)}");
                return StatusCode(500, ex.ToString());
            }
        }

        // PUT api/<ProductsController>/5
        [HttpPut("{id}")]

        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Put(int id, ProductDTO productDTO)
        {
            if (id <= 0)
            {
                logger.LogError($"Update Id is Invalid in :{nameof(Put)}-ID:{id}");
                return BadRequest();
            }
            var product = await _app_BlazorDBContext.Products.FindAsync(id);
            if (product == null)
            {
                logger.LogError($"Record Not Found:{nameof(Put)}-ID:{id}");
                return NotFound();
            }
            mapper.Map(productDTO, product);
            _app_BlazorDBContext.Entry(product).State = EntityState.Modified;
            await _app_BlazorDBContext.SaveChangesAsync();
            return NoContent();
        }

        // DELETE api/<ProductsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var product = await _app_BlazorDBContext.Products.FindAsync(id);
            if (product == null)
            {
                logger.LogError($"Record Not Found:{nameof(Delete)}-ID:{id}");
                return NotFound();
            }
            _app_BlazorDBContext.Products.Remove(product);
            await _app_BlazorDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error performing Delete Product in {nameof(Delete)}");
               
            }
            return Ok();
        }
      private List<ProductDTO> ParseAndBindXmlToModel(string xmlData)
        {
            var list = new List<Products>();
            List<ProductDTO> productList = new List<ProductDTO>();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlData);

            XmlNodeList productNodes = xmlDoc.SelectNodes("//item");
        

            foreach (XmlNode productNode in productNodes)
            {
                ProductDTO product = new ProductDTO();

                product.ProductId = Convert.ToInt32(productNode.SelectSingleNode("ProductId").InnerText);
                product.ProductName = productNode.SelectSingleNode("ProductName").InnerText;
                product.ProductCategory = productNode.SelectSingleNode("ProductCategory").InnerText;
                product.ProductImage = productNode.SelectSingleNode("ProductImage").InnerText;
                product.ProductDescription = productNode.SelectSingleNode("ProductDescription").InnerText;
                product.EntryDate = Convert.ToDateTime(productNode.SelectSingleNode("EntryDate").InnerText);
                product.ProductPrice = Convert.ToDecimal(productNode.SelectSingleNode("ProductPrice").InnerText);
           

                productList.Add(product);
            }
            //XmlDocument xmlDoc = new XmlDocument();
            //XmlSerializer serializer = new XmlSerializer(typeof(ProductDTO));

            //using (TextReader reader = new StringReader(xmlData.ToString()))
            //{
            //     var items = (ProductDTO)serializer.Deserialize(reader);
            //}



            // Now you have the data mapped to your model.
            return productList;
        }
    }
}
