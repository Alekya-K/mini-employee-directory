using employeedirectorysystemApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;



namespace employeedirectorysystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpPost]
        public IActionResult SaveEmployeeData([FromBody] EmployeeRequestDto requestDto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User ID=sa;Password=Amma@80;Trust Server Certificate=True"))
                {
                    connection.Open();

                    string query = @"INSERT INTO Employees (EmployeeId,FullName, Department, Email, Phone, HireDate)
                             VALUES (@EmployeeId,@FullName, @Department, @Email, @Phone, @HireDate)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@EmployeeId", requestDto.EmployeeId);
                        command.Parameters.AddWithValue("@FullName", requestDto.FullName);
                        command.Parameters.AddWithValue("@Department", requestDto.Department);
                        command.Parameters.AddWithValue("@Email", requestDto.Email);
                        command.Parameters.AddWithValue("@Phone", requestDto.Phone);
                        command.Parameters.AddWithValue("@HireDate", requestDto.HireDate);

                        command.ExecuteNonQuery();
                    }
                }

                return Ok(new { success = true, message = "Employee Saved Successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet]
        public IActionResult GetEmployeeData()
        {
            using (SqlConnection connection = new SqlConnection("Server=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User Id=sa;Password=Amma@80;TrustServerCertificate=True;"))
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_GetEmployeeDetails",
                    CommandType = CommandType.StoredProcedure,
                    Connection = connection
                };

                connection.Open();

                List<employeeDto> response = new List<employeeDto>();

                using (SqlDataReader sqlDataReader = command.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        employeeDto emp = new employeeDto();
                        emp.EmployeeId = Convert.ToInt32(sqlDataReader["EmployeeId"]);
                        emp.FullName = Convert.ToString(sqlDataReader["FullName"]);
                        emp.Department = Convert.ToString(sqlDataReader["Department"]);
                        emp.Phone = Convert.ToString(sqlDataReader["Phone"]);
                        emp.Email = Convert.ToString(sqlDataReader["Email"]);
                        emp.HireDate = Convert.ToString(sqlDataReader["HireDate"]);

                        response.Add(emp);
                    }
                }

                return Ok(response);
            }
        }

        [HttpDelete]

        public IActionResult DeleteEmployeeData(int employeeId)

        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User Id=sa;Password=Amma@80;TrustServerCertificate=True;"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteEmployeeDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection

            };

            command.Parameters.AddWithValue("@EmployeeId", employeeId);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return Ok();
        }
        
     [HttpPut]
        public ActionResult UpdateEmployeeData(EmployeeRequestDto employeeRequest)

        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Data Source=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User ID=sa;Password=Amma@80;Trust Server Certificate=True"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateEmployeeDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection

            };

            connection.Open();

            command.Parameters.AddWithValue("@EmployeeId", employeeRequest.EmployeeId);
            command.Parameters.AddWithValue("@FullName", employeeRequest.FullName);
            command.Parameters.AddWithValue("@Department", employeeRequest.Department);
            command.Parameters.AddWithValue("@Email", employeeRequest.Email);
            command.Parameters.AddWithValue("@Phone", employeeRequest.Phone);
            command.Parameters.AddWithValue("@HireDate", employeeRequest.HireDate);

            command.ExecuteNonQuery();

            connection.Close();
            return Ok();
        }


    }

    }
