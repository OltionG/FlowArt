using FlowArtAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace FlowArtAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]
        public class SongsController : ControllerBase
        {

            private readonly IConfiguration _configuration;
            private readonly IWebHostEnvironment _env;

            public SongsController(IConfiguration configuration, IWebHostEnvironment env)
            {
                _configuration = configuration;
                _env = env;
            }

            [HttpGet]
            public JsonResult Get()
            {
                string query = @"
            select SongID, SongIcon, SongTitle, SongArtist, SongGenre, Album, ReleaseDate
            from Songs";

                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
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
            public JsonResult Post(Songs s)
            {
                string query = @"
            insert into Songs values (@SongIcon, @SongTitle, @SongArtist, @SongGenre, @Album, @ReleaseDate)";

                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {

                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@SongIcon", s.SongIcon);
                        myCommand.Parameters.AddWithValue("@SongTitle", s.SongTitle);
                        myCommand.Parameters.AddWithValue("@SongArtist", s.SongArtist);
                        myCommand.Parameters.AddWithValue("@SongGenre", s.SongGenre);
                        myCommand.Parameters.AddWithValue("@Album", s.Album);
                        myCommand.Parameters.AddWithValue("@ReleaseDate", s.ReleaseDate);
                        myReader = myCommand.ExecuteReader();
                        dt.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Added Succesfully!");
            }

            [HttpPut]
            public JsonResult Put(Songs s)
            {
                string query = @"
            update Songs set SongIcon = @SongIcon, SongTitle = @SongTitle, SongArtist = @SongArtist, SongGenre = @SongGenre, Album = @Album, ReleaseDate = @ReleaseDate
            where SongID = @SongID";

                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {

                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@SongID", s.SongID);
                        myCommand.Parameters.AddWithValue("@SongIcon", s.SongIcon);
                        myCommand.Parameters.AddWithValue("@SongTitle", s.SongTitle);
                        myCommand.Parameters.AddWithValue("@SongArtist", s.SongArtist);
                        myCommand.Parameters.AddWithValue("@SongGenre", s.SongGenre);
                        myCommand.Parameters.AddWithValue("@Album", s.Album);
                        myCommand.Parameters.AddWithValue("@ReleaseDate", s.ReleaseDate);
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
            delete from Songs
            where SongID = @SongID";

                DataTable dt = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {

                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@SongID", id);
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
