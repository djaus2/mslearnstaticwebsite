﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text.Json;
using Data;

namespace Api
{
    public class ActivitysPut
    {
        private readonly IActivityData activityData;

        public ActivitysPut(IActivityData activityData)
        {
            this.activityData = activityData;
        }

        [FunctionName("ActivitysPut")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "activitys")] HttpRequest req,
            ILogger log)
        {
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var activity = JsonSerializer.Deserialize<Activity>(body, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            var updatedActivity = await activityData.UpdateActivity(activity);
            return new OkObjectResult(updatedActivity);
        }
    }
}