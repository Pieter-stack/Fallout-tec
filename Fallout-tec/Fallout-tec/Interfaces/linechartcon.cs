//using Microsoft.AspNetCore.Mvc;
//https://stackoverflow.com/questions/58816595/how-to-create-chartjs-chart-with-data-from-database-c-sharp
//https://stackoverflow.com/questions/25402894/return-jsonresult-with-list-of-objects-from-mvc-controller
//namespace Fallout_tec.Interfaces
//{
//   public class linechartcon : Controller
//   {

//           private readonly YourContext _context;

//           public linechartcon(string Itemname, context)
//            {
//               _context = context;
//           }

// GET: Jobs
//          public async Task<ActionResult> Index()
//          {
//               var Itemname = await _context.linechart.Select(j => j.JobCompletion).Distinct().ToListAsync();
//               var success = _context.Job
//                  .Where(j => j.JobStatus == "Success")
//                  .GroupBy(j => j.JobCompletion)
//                   .Select(group => new {
//                       JobCompletion = group.Key,
//                       Count = group.Count()
///                   });
//                var countSuccess = success.Select(a => a.Count).ToArray();
//               var exception = _context.Job
//                   .Where(j => j.JobStatus == "Exception")
//                   .GroupBy(j => j.JobCompletion)
//                   .Select(group => new {
//                        JobCompletion = group.Key,
//                      Count = group.Count()
//                  });
//              var countException = exception.Select(a => a.Count).ToArray();
//               return new JsonResult(new { ItemName = Itemname, mySuccess = countSuccess, myException = countException });
//           }
//      }
//   }


using Microsoft.AspNetCore.Mvc;


namespace Fallout_tec.Interfaces
{
  public class linechartcon : Controller
   {






}
}