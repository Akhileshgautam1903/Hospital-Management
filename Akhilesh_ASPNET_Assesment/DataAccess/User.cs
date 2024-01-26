using Akhilesh_ASPNET_Assesment.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Akhilesh_ASPNET_Assesment.DataAccess
{
    public class User : IUser
    {
        private IConfiguration _config;
        protected SqlConnection _connection;

        public User(IConfiguration configuration)
        {
            _config = configuration;
        }

        public SqlConnection GetConnection()
        {
            _connection = new SqlConnection(_config.GetSection("Data").GetSection("ConnectionStrings").Value);
            return _connection;
        }


        public bool RegisterUser(UserDetails user)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("REGISTER_USER", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@NAME", user.Name);
            cmd.Parameters.AddWithValue("@PHONE", user.PhoneNumber);
            cmd.Parameters.AddWithValue("@PASS", user.Password);
            cmd.Parameters.AddWithValue("@WALLET", user.Wallet);

            bool val = Convert.ToInt32(cmd.ExecuteScalar()) == 1;

            return val;
        }

        public bool LoginUser(long num, string pass)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT NAME FROM USER_DETAILS WHERE PHONE = @PHONE AND PASSWORD = @PASS COLLATE SQL_Latin1_General_CP1_CS_AS";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", num);
            cmd.Parameters.AddWithValue("@PASS", pass);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                UserDetails.UserPhoneNumber = num;
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                return false;
            }
        }

        public UserDetails FetchMyDetails()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "SELECT * FROM USER_DETAILS WHERE PHONE = @PHONE";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UserPhoneNumber);

            SqlDataReader reader = cmd.ExecuteReader();
            UserDetails user = new UserDetails();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Name = (string)reader["NAME"];
                    user.PhoneNumber = Convert.ToInt64(reader["PHONE"]);
                    user.Password = (string)reader["PASSWORD"];
                    user.Wallet = (int)reader["WALLET"];
                }
                reader.Close();
                connection.Close();
                return user;
            }
            else
            {
                return null;
            }

        }

        public bool UpdateWallet(decimal amt)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = "UPDATE USER_DETAILS SET WALLET = WALLET + @AMT WHERE PHONE = @PHONE";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@AMT", amt);
            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UserPhoneNumber);

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

        public int BookSlot(long doc_phone, DateTime date)
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            SqlCommand cmd = new SqlCommand("BOOK_SLOT", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@D_PHONE", doc_phone);
            cmd.Parameters.AddWithValue("@P_PHONE", UserDetails.UserPhoneNumber);
            cmd.Parameters.AddWithValue("@APPT_DATE", date);

            int val = Convert.ToInt32(cmd.ExecuteScalar());
            return val;
        }

        public List<DoctorDetails> FetchAllBookedSlots()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT A.ID, D.NAME, D.PHONE, D.FIELD, D.FEES, A.APPT_DATE, A.APPT_START_TIME, A.APPT_END_TIME
                            FROM DOCTOR_DETAILS D
                            JOIN APPT_DETAILS A
                            ON A.DOC_PHONE = D.PHONE
                            WHERE A.PATIENT_PHONE = @PHONE AND A.STATUS = 'Booked' ORDER BY INSERT_TIME DESC";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UserPhoneNumber);

            SqlDataReader reader = cmd.ExecuteReader();

            List<DoctorDetails> appts = new List<DoctorDetails>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DoctorDetails appt = new DoctorDetails
                    {
                        Id = (int)reader["ID"],
                        Name = (string)reader["NAME"],
                        PhoneNumber = Convert.ToInt64(reader["PHONE"]),
                        Field = (string)reader["FIELD"],
                        Fees = (int)reader["FEES"],
                        Appointment_date = reader.GetDateTime(reader.GetOrdinal("APPT_DATE")).ToString("yyyy-MM-dd"),
                        App_Start_time = reader.GetTimeSpan(reader.GetOrdinal("APPT_START_TIME")).ToString(@"hh\:mm\:ss"),
                        App_End_time = reader.GetTimeSpan(reader.GetOrdinal("APPT_END_TIME")).ToString(@"hh\:mm\:ss"),
                    };

                    appts.Add(appt);
                }
                reader.Close();
                connection.Close();
                return appts;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public List<DoctorDetails> FetchAllCancelledSlots()
        {
            SqlConnection connection = GetConnection();
            connection.Open();

            string query = @"SELECT A.ID, D.NAME, D.PHONE, D.FIELD, D.FEES, A.APPT_DATE, A.APPT_START_TIME, A.APPT_END_TIME
                            FROM DOCTOR_DETAILS D
                            JOIN APPT_DETAILS A
                            ON A.DOC_PHONE = D.PHONE
                            WHERE A.PATIENT_PHONE = @PHONE AND A.STATUS = 'Cancelled'";
            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@PHONE", UserDetails.UserPhoneNumber);

            SqlDataReader reader = cmd.ExecuteReader();

            List<DoctorDetails> appts = new List<DoctorDetails>();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DoctorDetails appt = new DoctorDetails
                    {
                        Id = (int)reader["ID"],
                        Name = (string)reader["NAME"],
                        PhoneNumber = Convert.ToInt64(reader["PHONE"]),
                        Field = (string)reader["FIELD"],
                        Fees = (int)reader["FEES"],
                        Appointment_date = reader.GetDateTime(reader.GetOrdinal("APPT_DATE")).ToString("yyyy-MM-dd"),
                        App_Start_time = reader.GetTimeSpan(reader.GetOrdinal("APPT_START_TIME")).ToString(@"hh\:mm\:ss"),
                        App_End_time = reader.GetTimeSpan(reader.GetOrdinal("APPT_END_TIME")).ToString(@"hh\:mm\:ss"),
                    };

                    appts.Add(appt);
                }
                reader.Close();
                connection.Close();
                return appts;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public bool CancelSlot(int id)
        {
            SqlConnection connection = GetConnection();
            connection.Open();
            
            SqlCommand cmd = new SqlCommand("CANCEL_SLOT", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", id);

            return Convert.ToInt32(cmd.ExecuteScalar()) == 1;
        }
    }
}
