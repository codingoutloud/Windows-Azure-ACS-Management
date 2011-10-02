//
//
//
//

namespace SyntaxC4.WindowsAzure.ACSManagement
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Home Realm Discovery: Identity Provider
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/gg185963.aspx"/>
    /// </summary>
    [DataContract]
    public class IdentityProvider : IIdentityProvider
    {
        public IdentityProvider() {}

        /// <summary>
        /// The human-readable display name for the identity provider.
        /// </summary>
        [DataMember]
        public string Name
        {
            get;
            private set;
        }
        /// <summary>
        /// A constructed login request URL.
        /// </summary>
        [DataMember]
        public string LoginUrl
        {
            get;
            private set;
        }
        /// <summary>
        /// This URL allows end users to sign out of the identity provider they signed in with. This is currently only supported for AD FS 2.0 and Windows Live ID and is empty for other identity providers.
        /// </summary>
        [DataMember]
        public string LogoutUrl
        {
            get;
            private set;
        }
        /// <summary>
        /// The image to display, as configured in the ACS Management Portal. Blank if there is no image.
        /// </summary>
        [DataMember]
        public string ImageUrl
        {
            get;
            private set;
        }
        /// <summary>
        /// An array of email address suffixes associated with the identity provider. In ACS, email address suffixes can only be configured for AD FS 2.0 identity providers via the ACS Management Portal. Returns an empty array if there are no suffixes configured.
        /// </summary>
        [DataMember]
        public string[] EmailAddressSuffixes
        {
            get;
            private set;
        }
    }
}
