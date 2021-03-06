﻿//--------------------------------------------------------------------------
//  <copyright file="SignIn.cs" company="Microsoft">
//      Copyright (c) 2015 Microsoft Corporation.
//
//      Permission is hereby granted, free of charge, to any person obtaining a copy
//      of this software and associated documentation files (the "Software"), to deal
//      in the Software without restriction, including without limitation the rights
//      to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//      copies of the Software, and to permit persons to whom the Software is
//      furnished to do so, subject to the following conditions:
//
//      The above copyright notice and this permission notice shall be included in
//
//      THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//      IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//      FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//      AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//      LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//      OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//      THE SOFTWARE.
//  </copyright>
//--------------------------------------------------------------------------

namespace Microsoft.Dynamics.Marketing.Powershell.OData.Commands
{
    using System;
    using System.Management.Automation;
    using System.Runtime.CompilerServices;

    using Client;

    /// <summary>
    /// The sign in.
    /// </summary>
    [Cmdlet(VerbsCommunications.Connect, "MDMOData")]
    public class SignIn : StopwatchCmdlet
    {
        /// <summary>
        /// Gets or sets the MDM OData Service Url
        /// </summary>
        [Parameter(Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string ServiceUrl { get; set; }

        /// <summary>
        /// Gets or sets the redirect URL of the Azure app
        /// </summary>
        [Parameter(Position = 1, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string RedirectUrl { get; set; }

        /// <summary>
        /// Gets or sets the ID of the Azure app that is permitted to access the MDM OData service of an org
        /// </summary>
        [Parameter(Position = 2, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string AzureClientAppId { get; set; }

        /// <summary>
        /// Gets or sets the User Id for prefilling the form
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the secure secret for direct sign in
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string SecureSecret { get; set; }

        /// <summary>
        /// Gets or sets the User Id for prefilling the form
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string OAuthUrl { get; set; }

        /// <summary>
        /// Resource that is protected
        /// </summary>
        [Parameter(ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string OAuthTokenResourceName { get; set; }

        /// <summary>
        /// ProcessRecord method.
        /// </summary>
        protected override void ProcessRecord()
        {
            try
            {
                var environment = ODataServiceEnvironment<DynamicDataServiceContext>.GetEnvironment();
                if (!string.IsNullOrEmpty(this.OAuthUrl))
                {
                    environment.OAuthUrl = this.OAuthUrl;
                }

                if (!string.IsNullOrEmpty(this.OAuthTokenResourceName))
                {
                    environment.OAuthTokenResourceName = this.OAuthTokenResourceName;
                }

                environment.ServiceUrl = this.ServiceUrl;

                if (!string.IsNullOrEmpty(this.RedirectUrl))
                {
                    environment.RedirectUrl = new Uri(this.RedirectUrl);
                }

                if (!string.IsNullOrEmpty(this.AzureClientAppId))
                {
                    environment.AzureClientAppId = this.AzureClientAppId;
                }

                var authResult = environment.SignIn(this.UserId, this.SecureSecret);
                this.WriteVerbose("Authorization token: " + authResult.AccessToken);
                this.WriteVerbose("Expires: " + authResult.ExpiresOn.ToLocalTime().ToString());
                this.WriteVerbose("Refresh Token: " + authResult.RefreshToken);
                this.WriteObject(environment.IsSignedIn);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    this.WriteVerbose(ex.InnerException.Message);
                }

                this.WriteVerbose(ex.Message);
                throw;
            }
        }
    }
}
