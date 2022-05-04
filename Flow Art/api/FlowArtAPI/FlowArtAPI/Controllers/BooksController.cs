using FlowArtAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace FlowArtAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public BooksController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select BookID, BookIcon, BookTitle, BookAuthor, BookGenre, PublishDate
            from Books";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(dt);
        }
        [HttpPost]
        public JsonResult Post(Books b)
        {
            string query = @"
            insert into Books values (@BookIcon, @BookTitle, @BookAuthor, @BookGenre, @PublishDate)";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BookIcon", b.BookIcon);
                    myCommand.Parameters.AddWithValue("@BookTitle", b.BookTitle);
                    myCommand.Parameters.AddWithValue("@BookAuthor", b.BookAuthor);
                    myCommand.Parameters.AddWithValue("@BookGenre", b.BookGenre);
                    myCommand.Parameters.AddWithValue("@PublishDate", b.PublishDate);
                    myReader = myCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Succesfully!");
        }

        [HttpPut]
        public JsonResult Put(Books b)
        {
            string query = @"
            update Books set BookIcon = @BookIcon, BookTitle = @BookTitle, BookAuthor = @BookAuthor, BookGenre = @BookGenre, PublishDate = @PublishDate
            where BookID = @BookID";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BookID", b.BookID);
                    myCommand.Parameters.AddWithValue("@BookIcon", b.BookIcon);
                    myCommand.Parameters.AddWithValue("@BookTitle", b.BookTitle);
                    myCommand.Parameters.AddWithValue("@BookAuthor", b.BookAuthor);
                    myCommand.Parameters.AddWithValue("@BookGenre", b.BookGenre);
                    myCommand.Parameters.AddWithValue("@PublishDate", b.PublishDate);
                    myReader = myCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Succesfully!");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
            delete from Books
            where BookID = @BookID";

            DataTable dt = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@BookID", id);
                    myReader = myCommand.ExecuteReader();
                    dt.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Succesfully!");
        }

        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception ex)
            {
                return new JsonResult("icon.png");
            }
        }
    }
}
