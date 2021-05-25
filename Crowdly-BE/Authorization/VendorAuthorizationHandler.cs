using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Crowdly_BE.Authorization
{
    public class VendorAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Services.Vendors.Models.Vendor>
    {
        private readonly string[] _allowedOperationForOwner = new[] {
            VendorOperations.Update.Name,
            VendorOperations.Delete.Name
        };

        protected override Task HandleRequirementAsync(
                                              AuthorizationHandlerContext context,
                                    OperationAuthorizationRequirement requirement,
                                     Services.Vendors.Models.Vendor resource)
        {
            if (context.User == null)
            {
                return Task.CompletedTask;
            }

            if (_allowedOperationForOwner.Contains(requirement.Name))
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier).Value;
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

    public static class VendorOperations
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
