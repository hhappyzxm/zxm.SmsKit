using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zxm.SmsKit.Data;

namespace zxm.SmsKit
{
    public interface ISmsClient
    {
        /// <summary>
        /// Sends a message to the phonenumber(s) supplied.
        /// </summary>
        /// <param name="SendMessageRequest"></param>
        /// <returns>
        /// SendMessageResponse:
        /// Success = If call was successfully made to Clickatell
        /// Result  = Service response
        /// Messages[] = Message object which will have the APIMessageID(Guid created for message for reference) and To(The phonenumber)
        /// </returns>
        Task<SendMessageResponse> SendMessageAsync(SendMessageRequest request);
    }
}
