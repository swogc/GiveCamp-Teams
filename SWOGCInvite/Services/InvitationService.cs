using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Microsoft.Identity.Web;

namespace SWOGCInvite.Services
{
    public class InvitationService : IInvitationService
    {
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly WebOptions _webOptions;

        public InvitationService(ITokenAcquisition tokenAcquisition, IOptions<WebOptions> webOptionsValue)
        {
            _tokenAcquisition = tokenAcquisition;
            _webOptions = webOptionsValue.Value;
        }

        public async Task<InvitationResult> CreateInvitation(string emailAddress, CancellationToken token)
        {
            var graphClient = GetGraphServiceClient();

            var invitation = new Invitation
            {
                InvitedUserEmailAddress = emailAddress,
                InviteRedirectUrl = "https://southwestohiogivecamp.org",
                //InvitedUserDisplayName = "SWOGC Administrators",
                //SendInvitationMessage = true,
                //InvitedUserMessageInfo = new InvitedUserMessageInfo
                //{
                //    CustomizedMessageBody = "You have submitted a request to be a part of the" +
                //                            "Southwest Ohio GiveCamp.  If you did not make this request, please ignore this message",
                //}
            };

            try
            {
                var addResult = await graphClient.Invitations.Request().AddAsync(invitation, token);

                return new InvitationResult
                {
                    Id = addResult.Id,
                    Success = true,
                    Status = addResult.Status,
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private GraphServiceClient GetGraphServiceClient()
        {
            return GraphServiceClientFactory.GetAuthenticatedGraphClient(async () =>
            {
                string result = await _tokenAcquisition.GetAccessTokenForAppAsync("https://graph.microsoft.com/.default");
                return result;
            }, _webOptions.GraphApiUrl);
        }
    }

    public class InvitationResult
    {
        public string Id { get; set; }
        public bool Success { get; set; }
        public string Status { get; set; }
    }

    public interface IInvitationService
    {
        Task<InvitationResult> CreateInvitation(string emailAddress, CancellationToken token);
    }
}
