// Code concept from Fabrikam Shipping Demo by Microsoft Inc.
// View License: http://archive.msdn.microsoft.com/fshipsaassource/Project/License.aspx
// OR in LICENSE.txt in root of this project.

namespace SyntaxC4.WindowsAzure.ACSManagement.Mvc
{
    using System.Web;
    using System.Web.Util;
    using Microsoft.IdentityModel.Protocols.WSFederation;

    /// <summary>
    /// This WSFederationRequestValidator validates the wresult parameter of the
    /// WS-Federation passive protocol by checking for a SignInResponse message
    /// in the form post. The SignInResponse message contents are verified later by
    /// the WSFederationPassiveAuthenticationModule or the WIF signin controls.
    /// </summary>
    /// <see cref="http://archive.msdn.microsoft.com/fshipsaassource"/>
    /// <seealso cref="http://blogs.objectsharp.com/cs/blogs/steve/archive/2010/10/29/token-request-validation-in-asp-net.aspx"/>
    public class WsFederationRequestValidator : RequestValidator
    {
        protected override bool IsValidRequestString(HttpContext context, string value, RequestValidationSource requestValidationSource, string collectionKey, out int validationFailureIndex)
        {
            validationFailureIndex = 0;

            if (requestValidationSource == RequestValidationSource.Form &&
                collectionKey.Equals(WSFederationConstants.Parameters.Result))
            {
                var signinResponseMessage = WSFederationMessage.CreateFromFormPost(context.Request) as SignInRequestMessage;
                if (signinResponseMessage != null) return true;
            }

            return base.IsValidRequestString(context, value, requestValidationSource, collectionKey, out validationFailureIndex);
        }
    }
}
