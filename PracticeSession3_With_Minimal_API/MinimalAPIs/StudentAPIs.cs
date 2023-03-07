using PracticeSession3_With_Minimal_API.Models.Repository;
using PracticeSession3_With_Minimal_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace PracticeSession3_With_Minimal_API.MinimalAPIs
{
    public static class StudentAPIs
    {
 

        public static void MapRoutes(WebApplication app)
        {
            app.MapGet("/students", GetAllStudents);
            app.MapGet("/students/{id}", GetStudent).WithName("Get");
            app.MapPost("/students", AddStudent);
            app.MapPut("/students/{id}", UpdateStudent);
        }

        private static async  Task<IResult>  GetAllStudents([FromServices] IDataRepository<Student> dataRepository)
        {
            try
            {
                IEnumerable<Student> students = dataRepository.GetAll();
                return TypedResults.Ok(students.ToList());
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Internal server error: {ex}");
            }
            
        }

        private static async Task<IResult> GetStudent(int id,[FromServices] IDataRepository<Student> dataRepository)
        {
            try
            {
                Student student= dataRepository.Get(id);
                if (student == null)
                {
                    return TypedResults.NotFound("This student record could not be found");
                }
                return TypedResults.Ok(student);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Internal server error: {ex}");
            }
        }

        private static async Task<IResult> AddStudent([FromBody] StudentDto studentdto, [FromServices] IDataRepository<Student> dataRepository)
        {
            try
            {
                if(studentdto==null)
                {
                    return TypedResults.BadRequest("Student is null");
                }
                var student = new Student()
                {
                    FirstName = studentdto.FirstName,
                    LastName = studentdto.LastName,
                    City = studentdto.City,
                };

                dataRepository.Add(student);

                return TypedResults.CreatedAtRoute(routeName:"Get",
                    routeValues: new { Id = student.StudentID },
                    value: student);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Internal server error: {ex}");
            }
        }

        private static async Task<IResult> UpdateStudent([FromBody] Student student, [FromServices] IDataRepository<Student> dataRepository)
        {
            try
            {
                if (student == null)
                {
                    return TypedResults.BadRequest("Student is null.");
                }



                dataRepository.Update(student);
                return TypedResults.NoContent();
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Internal server error: {ex}");
            }
        }
       
    }
}
