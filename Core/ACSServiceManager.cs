// Developer: Cory [SyntaxC4] Fowler
// Copyright (c) 2011 Cory Fowler
// License: http://www.opensource.org/licenses/mit-license.php

namespace SyntaxC4.WindowsAzure.ACSManagement
{
    using System;
    using System.Diagnostics.Contracts;
    using System.IO;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Web;

    /// <summary>
    /// Management of ACS Services
    /// <seealso cref="http://msdn.microsoft.com/en-us/library/gg185963.aspx"/>
    /// </summary>
    public class ACSServiceManager
    {
        private Uri _managementUri;
        private const string IDENTITY_PROVIDER_URL_FORMAT = "https://{0}.accesscontrol.windows.net:443/v2/metadata/IdentityProviders.js?protocol={1}&realm={2}&reply_to={3}&context={4}&request_id=&version={5}&callback=";
        private const string PROTOCOL = "wsfederation";
        private const string VERSION = "1.0";

        public string Realm { get; private set; }
        public string Namespace { get; private set; }
        public string Protocol { get; private set; }
        public string ACSVersion { get; private set; }
        public string ReplyTo { get; private set; }
        public string Context { get; private set; }
        private Uri IdentityProviderUrl
        {
            get
            {
                if (_managementUri == null)
                {
                    string managementUri =
                        string.Format(IDENTITY_PROVIDER_URL_FORMAT,
                                            Namespace, Protocol, HttpUtility.UrlEncode(Realm), HttpUtility.UrlEncode(ReplyTo), Context, ACSVersion);
                    _managementUri = new Uri(managementUri);
                }

                return _managementUri;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace">Required. Set to the name of your Windows Azure AppFabric service namespace.</param>
        /// <param name="realm">Required. This is the Realm that you specified for your relying party application in the ACS Management Portal.</param>
        /// <param name="protocol">Required. This is the protocol that your relying party application uses to communicate with ACS. In ACS this value must be set to wsfederation.</param>
        /// <param name="version">Required. In ACS this value must be set to 1.0.</param>
        public ACSServiceManager(string @namespace, string realm, string protocol = PROTOCOL, string version = VERSION)
            : this(@namespace, realm, protocol, version, string.Empty) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace">Required. Set to the name of your Windows Azure AppFabric service namespace.</param>
        /// <param name="realm">Required. This is the Realm that you specified for your relying party application in the ACS Management Portal.</param>
        /// <param name="protocol">Required. This is the protocol that your relying party application uses to communicate with ACS. In ACS this value must be set to wsfederation.</param>
        /// <param name="version">Required. In ACS this value must be set to 1.0.</param>
        /// <param name="replyTo">Optional. This is the Return URL that you specified for your relying party application in the ACS Management Portal. If omitted, the Return URL is set to the default value that is configured for your relying party application in the ACS Management Portal.</param>
        public ACSServiceManager(string @namespace, string realm, string protocol, string version, string replyTo)
            : this(@namespace, realm, protocol, version, replyTo, string.Empty) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="namespace">Required. Set to the name of your Windows Azure AppFabric service namespace.</param>
        /// <param name="realm">Required. This is the Realm that you specified for your relying party application in the ACS Management Portal.</param>
        /// <param name="protocol">Required. This is the protocol that your relying party application uses to communicate with ACS. In ACS this value must be set to wsfederation.</param>
        /// <param name="version">Required. In ACS this value must be set to 1.0.</param>
        /// <param name="replyTo">Optional. This is the Return URL that you specified for your relying party application in the ACS Management Portal. If omitted, the Return URL is set to the default value that is configured for your relying party application in the ACS Management Portal.</param>
        /// <param name="context">Optional. This is any additional context that can be passed back to the relying party application in the token. ACS does not recognize these contents.</param>
        public ACSServiceManager(string @namespace, string realm, string protocol, string version, string replyTo, string context)
        {
            Contract.Requires(!string.IsNullOrWhiteSpace(@namespace), "The ACS Service Namespace is Required.");
            Contract.Requires(!string.IsNullOrWhiteSpace(realm), "The Realm for your Application is Required.");
            Contract.Requires(!string.IsNullOrWhiteSpace(version));
            Contract.Requires(!string.IsNullOrWhiteSpace(protocol));

            Namespace = @namespace;
            Realm = realm;
            Protocol = protocol;
            ACSVersion = version;
            ReplyTo = replyTo;
            Context = context;
        }

        /// <summary>
        /// Enumerates Identity Providers that have been configured for the namespace
        /// </summary>
        /// <returns>Configured Identity Providers from ACS</returns>
        public IIdentityProvider[] GetIdentityProviders<T>()
            where T : IIdentityProvider, new()
        {
            var request = HttpWebRequest.Create(IdentityProviderUrl.AbsoluteUri);
            var response = request.GetResponse().GetResponseStream();

            return GetIdentityProviders<T>(response);
        }

        /// <summary>
        /// Enumerates Identity Providers that have been configured for the namespace
        /// </summary>
        /// <param name="responseStream">Response Stream from ACS</param>
        /// <returns>Configured Identity Providers from ACS</returns>
        public IIdentityProvider[] GetIdentityProviders<T>(Stream response)
            where T : IIdentityProvider, new()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(T[]));
            var providers = jsonSerializer.ReadObject(response) as IIdentityProvider[];

            return providers;
        }

    }
}
