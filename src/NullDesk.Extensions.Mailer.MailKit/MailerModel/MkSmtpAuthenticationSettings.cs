﻿using System.Net;

// ReSharper disable once CheckNamespace
namespace NullDesk.Extensions.Mailer.MailKit
{
    /// <summary>
    ///     Authentication for MailKit SMTP mailers.
    /// </summary>
    public class MkSmtpAuthenticationSettings
    {
        /// <summary>
        ///     If provided, specifies the username used to authenticate with the SMTP server
        /// </summary>
        /// <returns>The username</returns>
        public string UserName { get; set; }

        /// <summary>
        ///     If provided, specifies the password used to authenticate with the SMTP server
        /// </summary>
        /// <returns></returns>
        public string Password { get; set; }

        /// <summary>
        ///     If provided, specifies the credentials used to autheticate with the SMTP server.
        /// </summary>
        /// <remarks>
        ///     Will be used instead of username and password if provided.
        /// </remarks>
        /// <returns></returns>
        public ICredentials Credentials { get; set; }
    }
}