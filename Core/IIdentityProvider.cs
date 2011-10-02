// Developer: Cory [SyntaxC4] Fowler
// Copyright (c) 2011 Cory Fowler
// License: http://www.opensource.org/licenses/mit-license.php

namespace SyntaxC4.WindowsAzure.ACSManagement
{
    public interface IIdentityProvider
    {
        string Name { get; }
        string LoginUrl { get; }
        string LogoutUrl { get; }
        string ImageUrl { get; }
        string[] EmailAddressSuffixes { get; }
    }
}
