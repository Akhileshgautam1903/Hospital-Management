using Akhilesh_ASPNET_Assesment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.DataAccess
{
    public class Admin : IAdmin
    {
        private IConfiguration _config;
        protected SqlConnection _connection;

        public Admin(IConfiguration configuration)
        {
            _config = configuration;
        }

        public SqlConnection GetConnection()
        {
            _connection = new SqlConnection(_config.GetSection("Data").GetSection("ConnectionStrings").Value);
            return _connection;
        }

        public bool LoginAdmin(long num, string pass)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT ID FROM DOCTOR_DETAILS WHERE PHONE = @PHONE AND PASSWORD = @PASS COLLATE SQL_Latin1_General_CP1_CS_AS";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", num);
            cmd.Parameters.AddWithValue("@PASS", pass);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                DoctorDetails.DocPhoneNumber = num; 
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public bool AddDoctor(DoctorDetails doc)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("ADD_DOC", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NAME", doc.Name);
            cmd.Parameters.AddWithValue("@PHONE", doc.PhoneNumber);
            cmd.Parameters.AddWithValue("@PASS", doc.Password);
            cmd.Parameters.AddWithValue("@FIELD", doc.Field);
            cmd.Parameters.AddWithValue("@STATUS", doc.Status);
            cmd.Parameters.AddWithValue("@START_TIME", doc.Start_time);
            cmd.Parameters.AddWithValue("@END_TIME", doc.End_time);
            cmd.Parameters.AddWithValue("@SLOTS", doc.Slots);
            cmd.Parameters.AddWithValue("@FEES", doc.Fees);

            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }
        
        public bool UpdateDoctor(DoctorDetails doc)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "UPDATE DOCTOR_DETAILS SET FEES = @FEES, AVAILABILITY_STATUS = @STATUS WHERE PHONE = @PHONE";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@FEES", doc.Fees);
            cmd.Parameters.AddWithValue("@STATUS", doc.Status);
            cmd.Parameters.AddWithValue("@PHONE", doc.PhoneNumber);

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public List<DoctorDetails> FetchAllDoctors()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM DOCTOR_DETAILS";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            List<DoctorDetails> doctors = new List<DoctorDetails>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DoctorDetails doc = new DoctorDetails
                    {
                        Id = (int)reader["ID"],

                        Name = (string)reader["NAME"],
                        PhoneNumber = Convert.ToInt64(reader["PHONE"]),
                        Password = (string)reader["PASSWORD"],
                        Field = (string)reader["FIELD"],
                        Status = (string)reader["AVAILABILITY_STATUS"],
                        Start_time = reader.GetTimeSpan(reader.GetOrdinal("START_TIME")),
                        End_time = reader.GetTimeSpan(reader.GetOrdinal("END_TIME")),
                        Slots = (int)reader["SLOTS"],
                        Fees = (int)reader["FEES"],
                    };

                    doctors.Add(doc);
                }
                reader.Close();
                connection.Close();
                return doctors;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteDoctor(int id)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "DELETE FROM DOCTOR_DETAILS WHERE ID = @ID";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@ID", id);

            if (cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public List<UserDetails> FetchUserDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT DISTINCT U.NAME, U.PHONE 
                            FROM USER_DETAILS U
                            JOIN APPT_DETAILS A
                            ON A.PATIENT_PHONE = U.PHONE";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            List<UserDetails> users = new List<UserDetails>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    UserDetails user = new UserDetails
                    {
                        Name = (string)reader["NAME"],
                        PhoneNumber = Convert.ToInt64(reader["PHONE"])
                    };

                    users.Add(user);
                }
                reader.Close();
                connection.Close();
                return users;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        //for adding image to database
        public bool AddImage(ImageDetails Data)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            //converting image into byte array and storing into imageData
            byte[] imageData = ConvertFormFileToByteArray(Data.Pic);

            string query = "INSERT INTO IMAGE_DETAILS VALUES (@NAME, @IMAGE)";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@NAME", Data.Name);
            cmd.Parameters.AddWithValue("@IMAGE", imageData);

            if(cmd.ExecuteNonQuery() > 0)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        //to convert image into byte array so that we can store in database
        private byte[] ConvertFormFileToByteArray(IFormFile file)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                file.CopyTo(ms);
                return ms.ToArray();
            }
        }
        
        // for image
        public List<ImageDetails> FetchImages()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM IMAGE_DETAILS";
            SqlCommand cmd = new SqlCommand(query, connection);

            SqlDataReader reader = cmd.ExecuteReader();

            List<ImageDetails> images = new List<ImageDetails>();
    
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ImageDetails image = new ImageDetails();
                    image.Name = (string)reader["NAME"];
                    if (reader["IMG"] != DBNull.Value)
                    {
                        image.RetriveData = (byte[])reader["IMG"];
                    }
                    images.Add(image);
                }
                connection.Close();
                return images;
            }
            else
            {
                connection.Close();
                return null;
            }
        }
               
    }
}
