using System;
using System.Activities;

namespace Twilio.Workflow.Activities
{
    class InitiateOutboundCall : CodeActivity
    {
        /// <summary>
        /// Your Twilio Account SID.  The SID can be obtained from your Twilio dashboard: https://www.twilio.com/user/account
        /// </summary>
        public InArgument<string> AccountSid { get; set; }

        /// <summary>
        /// Your Twilio Authorization Token.  The token can be obtained from your Twilio dashboard: https://www.twilio.com/user/account
        /// </summary>
        public InArgument<string> AuthToken { get; set; }

        /// <summary>
        /// The phone number to send this message to
        /// </summary>
        public InArgument<string> To { get; set; }

        /// <summary>
        /// The phone number to send this message from
        /// </summary>
        public InArgument<string> From { get; set; }

        /// <summary>
        /// The URL Twilio should call when the call is answered
        /// </summary>
        public InArgument<string> CallbackUri { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var client = new TwilioRestClient(this.AccountSid.Get(context), this.AuthToken.Get(context));
            var result = client.InitiateOutboundCall(this.From.Get(context), this.To.Get(context), this.CallbackUri.Get(context));

            if (result.RestException != null)
            {
                throw new Exception(String.Format("Outbound call failed: {0}", result.RestException.Message));
            }
        }
    }
}
