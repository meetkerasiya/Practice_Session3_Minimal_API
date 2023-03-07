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
       
    }
}
