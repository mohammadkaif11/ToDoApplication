using Project4.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Webapi.Controllers
{
    public class ValuesController : ApiController
    {

        //Get All Notes
        [ActionName("GetNote")]
        public IHttpActionResult Get()
        {
            List<Note> notes = new List<Note>();
            string cs = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Testing;Integrated Security=true;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "select * from Kaif";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Note note = new Note();
                note.Id = sqlDataReader.GetInt32(0);
                note.Title = sqlDataReader.GetString(1);
                note.Description = sqlDataReader.GetString(2);
                notes.Add(note);
            }
            con.Close();
            if (notes == null)
            {
                return NotFound();
            }
            return Ok(notes);
        }


        public IHttpActionResult Get(int id)
        {
            Note note = new Note();
            string cs = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Testing;Integrated Security=true;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "select * from kaif where Id=" + id + "";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                note.Id = sqlDataReader.GetInt32(0);
                note.Title = sqlDataReader.GetString(1);
                note.Description = sqlDataReader.GetString(2);
            }
            con.Close();
            if (note== null)
            {
                return NotFound();
            }
            return Ok(note);
        }


        [HttpPut]
        public IHttpActionResult updateN([FromUri] int id,[FromBody] Note note)
        {
            string cs = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Testing;Integrated Security=true;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "Update kaif set title = @title,description=@description where Id = @id";
            // string query = "update Kaif set title=" + note.Title + ",description=" + note.Description + "where Id = " + id + "";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@title", note.Title);
            sqlCommand.Parameters.AddWithValue("@description", note.Description);
            sqlCommand.Parameters.AddWithValue("@id",id);
            int k = sqlCommand.ExecuteNonQuery();
            con.Close();
            if (k > 0)
            {
                return Ok(k);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]

        public IHttpActionResult Postdata([FromBody] Note note)
        {
            string cs = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Testing;Integrated Security=true;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "insert into Kaif values(@title,@description)";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            sqlCommand.Parameters.AddWithValue("@title", note.Title);
            sqlCommand.Parameters.AddWithValue("@description", note.Description);
            int k = sqlCommand.ExecuteNonQuery();
            con.Close();
            if (k > 0)
            {
                return Ok(k);
            }
            else
            {
                return NotFound();
            }
        }


        [HttpDelete]
        public IHttpActionResult DeleteNote( int id)
        {
            string cs = "Data Source=(localdb)\\ProjectModels;Initial Catalog=Testing;Integrated Security=true;";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string query = "delete from kaif where Id=" + id + "";
            SqlCommand sqlCommand = new SqlCommand(query, con);
            int k = sqlCommand.ExecuteNonQuery();
            con.Close();
            if (k > 0)
            {
                return Ok(k);
            }
            else
            {
                return NotFound();
            }
        }

  

    }
}
