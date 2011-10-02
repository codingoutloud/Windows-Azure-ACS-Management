// Developer: Cory [SyntaxC4] Fowler
// Copyright (c) 2011 Cory Fowler
// License: http://www.opensource.org/licenses/mit-license.php

namespace SyntaxC4.WindowsAzure.ACSManagement.Tests
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests the functionality of the ACSServiceManagement Class
    /// </summary>
    [TestClass]
    public class ACSServiceManagementTests
    {
        private ACSServiceManager _manager;
        private const string _json = "[{\"Name\":\"TestProvider\",\"LoginUrl\":\"http://bestProviderEvar.net/Login\",\"LogoutUrl\":\"http://bestProviderEvar.net/Logout\",\"ImageUrl\":\"http://bestProviderEvar.net/Images/Logo.png\",\"EmailAddressSuffixes\":[\"syntaxc4.net\"]}]";
        private byte[] _jsonBytes;
        private MemoryStream _jsonStream;

        public ACSServiceManagementTests()
        {
            //TODO: Provide valid Windows Azure ACS Namespace
            #warning Provide valid Windows Azure ACS Namespace
            _manager = new ACSServiceManager("", "http://localhost");
            _jsonBytes = Encoding.UTF8.GetBytes(_json);
            _jsonStream = new MemoryStream(_jsonBytes);
            _jsonStream.Position = 0;
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        public void Returns_Array_Of_Identity_Providers()
        {
            var providers = _manager.GetIdentityProviders<IdentityProvider>();
            Assert.IsNotNull(providers);
            Assert.IsInstanceOfType(providers, typeof(IIdentityProvider[]));
        }

        [TestMethod]
        public void Returns_The_Correct_Number_Of_Identity_Providers()
        {
            var providers = GetEntityProvider();
            var count = providers.Count();
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void Returns_Valid_Identity_Provider_Name()
        {
            var provider = GetEntityProvider().First();
            Assert.AreEqual("TestProvider", provider.Name);
        }

        [TestMethod]
        public void Returns_Valid_Identity_Provider_Login_Url()
        {
            var provider = GetEntityProvider().First();
            Assert.AreEqual("http://bestProviderEvar.net/Login", provider.LoginUrl);
        }

        [TestMethod]
        public void Returns_Valid_Identity_Provider_Logout_Url()
        {
            var provider = GetEntityProvider().First();
            Assert.AreEqual("http://bestProviderEvar.net/Logout", provider.LogoutUrl);
        }

        [TestMethod]
        public void Returns_Valid_Identity_Provider_Image_Url()
        {
            var provider = GetEntityProvider().First();
            Assert.AreEqual("http://bestProviderEvar.net/Images/Logo.png", provider.ImageUrl);
        }

        [TestMethod]
        public void Returns_Valid_Identity_Provider_Email_Address_Suffixes()
        {
            var provider = GetEntityProvider().First();
            var EmailSuffix = provider.EmailAddressSuffixes.First();
        }

        private IIdentityProvider[] GetEntityProvider()
        {
            var providers = _manager.GetIdentityProviders<IdentityProvider>(_jsonStream);
            return providers;
        }
    }
}
