using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using zxm.SmsKit;

namespace zxm.MailKit.Tests
{
    public class ClickatellClientTest
    {
        [Fact]
        public async Task TestSendSms()
        {
            var client = new ClickatellClient(new SmsKit.Data.RESTCredentials("1NddRb4U6If7rSKAiVRkUXvLCUM31VTLloXNWAQBYyC1oDgVxfcTdmpHTzMcAduVE3mPhEPvnbiAX1"));
            var response = await client.SendMessageAsync(new SmsKit.Data.SendMessageRequest("test message", "8613921198852"));
            Assert.True(response.Success);
            
            await Assert.ThrowsAnyAsync<SmsKitException>(async ()=> await client.SendMessageAsync(new SmsKit.Data.SendMessageRequest("test message", "13921198852")));
        }
    }
}
