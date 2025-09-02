using employeedirectorysystemApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json.Serialization;

namespace employeedirectorysystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        [HttpPost]
        public IActionResult SaveDepartmentData([FromBody] departmentRequestDto requestDto)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User Id=sa;Password=Amma@80;TrustServerCertificate=True;"))
                {
                    connection.Open();

                    string query = @"INSERT INTO Departments (DepartmentId, DepartmentName, Location)
                             VALUES (@DepartmentId, @DepartmentName, @Location)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@DepartmentId", requestDto.DepartmentId);
                        command.Parameters.AddWithValue("@DepartmentName", requestDto.DepartmentName);
                        command.Parameters.AddWithValue("@Location", requestDto.Location);

                        command.ExecuteNonQuery();
                    }
                }

                // ✅ Return JSON instead of plain text

                return new JsonResult(new
                {
                    success = true,
                    message = "Department Details Saved Successfully!",
                    data = requestDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }


        }

        [HttpGet]
        public IActionResult GetDepartmentData()
        {
            using (SqlConnection connection = new SqlConnection("Server=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User Id=sa;Password=Amma@80;TrustServerCertificate=True;"))
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = "sp_GetDepartmentData",
                    CommandType = CommandType.StoredProcedure,
                    Connection = connection
                };

                connection.Open();

                List<departmentDto> response = new List<departmentDto>();

                using (SqlDataReader sqlDataReader = command.ExecuteReader())
                {
                    while (sqlDataReader.Read())
                    {
                        departmentDto dept = new departmentDto();
                        dept.DepartmentId = Convert.ToInt32(sqlDataReader["DepartmentId"]);
                        dept.DepartmentName = Convert.ToString(sqlDataReader["DepartmentName"]);
                        dept.Location = Convert.ToString(sqlDataReader["Location"]);

                        response.Add(dept);
                    }
                }

                return Ok(response); 
            }
        }

        [HttpDelete]
        public ActionResult DeleteDepartmentData(int departmentId)

        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Server=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User Id=sa;Password=Amma@80;TrustServerCertificate=True;"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_DeleteDepartmentDetails",
                CommandType = CommandType.StoredProcedure,
                Connection = connection

            };

            command.Parameters.AddWithValue("@DepartmentId", departmentId);


            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

            return Ok();
        }
        [HttpPut]
        public ActionResult UpdateDepartmentData(departmentRequestDto departmentRequest)

        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = "Data Source=LAPTOP-JE2CGHI0;Database=EmployeeDirectoryDB;User ID=sa;Password=Amma@80;Trust Server Certificate=True"
            };

            SqlCommand command = new SqlCommand
            {
                CommandText = "sp_UpdateDepartmentData",
                CommandType = CommandType.StoredProcedure,
                Connection = connection

            };

            connection.Open();


            command.Parameters.AddWithValue("@departmentId", departmentRequest.DepartmentId);
            command.Parameters.AddWithValue("@departmentName", departmentRequest.DepartmentName);
            command.Parameters.AddWithValue("@Location", departmentRequest.Location);
            


            command.ExecuteNonQuery();

            connection.Close();
            return Ok();
        }
    }
}
