using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Authorization
{
    public class EventAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Services.Events.Models.EventDetails>
    {
        private readonly string[] _allowedOperationForOwner = new[] {
            EventOperations.Update.Name,
            EventOperations.Delete.Name
        };

        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Services.Events.Models.EventDetails resource)
        {
            if (context.User == null)
            {
                context.Fail();
                return Task.CompletedTask;
            }

            if (_allowedOperationForOwner.Contains(requirement.Name))
            {
                var userId = new Guid(context.User.FindFirstValue(ClaimTypes.NameIdentifier));
                if (userId == resource.CreatedByUserId)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public static class EventOperations
    {
        public static OperationAuthorizationRequirement Create =
          new OperationAuthorizationRequirement { Name = Constants.CreateOperationName };
        public static OperationAuthorizationRequirement Read =
          new OperationAuthorizationRequirement { Name = Constants.ReadOperationName };
        public static OperationAuthorizationRequirement Update =
          new OperationAuthorizationRequirement { Name = Constants.UpdateOperationName };
        public static OperationAuthorizationRequirement Delete =
          new OperationAuthorizationRequirement { Name = Constants.DeleteOperationName };
        public static OperationAuthorizationRequirement Approve =
          new OperationAuthorizationRequirement { Name = Constants.ApproveOperationName };
        public static OperationAuthorizationRequirement Reject =
          new OperationAuthorizationRequirement { Name = Constants.RejectOperationName };
    }
}
