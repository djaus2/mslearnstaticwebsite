﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Api
{
    public class ActivitysDelete
    {
        private readonly IActivityData activityData;
        private readonly ActivityHelpersContext _context;

        public ActivitysDelete(ActivityHelpersContext context, IActivityData activityData)
        {
            this.activityData = activityData;
            _context = context;
        }

        [FunctionName("ActivitysDelete")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "activitys/{activityId:int}")] HttpRequest req,
            int activityId,
            ILogger log)
        {
            var result = await activityData.DeleteActivity(activityId);

            if (result)
            {
                return new OkResult();
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
