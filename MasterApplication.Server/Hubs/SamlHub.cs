using MasterApplication.DB.Services;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace EHL.Api.Hubs
{
    public class SamlHub: Hub
    {
        public static ConcurrentDictionary<long, string> ConnectionIds { get; } = new();
        public override Task OnConnectedAsync()
        {
            try
            {
                var id = long.Parse(Context.User.Claims.FirstOrDefault(c => c.Type == MasterConstant.UserId)?.Value ?? "0");
                MasterLogger.Info($"Saml Hub connected for user {id}", "SamlHub", "OnConnectedAsync");
                if (id > 0)
                {
                    ConnectionIds.AddOrUpdate(id, Context.ConnectionId, (_, _) => Context.ConnectionId);
                }
            }
            catch(Exception ex)
            {
                MasterLogger.Info($"{ex}", "User didn't find", "OnConnectedAsync");
            }
            
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var id = long.Parse(Context.User.Claims.FirstOrDefault(c => c.Type == MasterConstant.UserId)?.Value ?? "0");
            if(id > 0)
            {
                ConnectionIds.TryRemove(id, out _);
            }
            MasterLogger.Info($"Saml Hub disconnected for user {id}", "SamlHub", "OnDisconnectedAsync");
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendSamlLogoutRequestAsync(long userId,string samlRequest)
        {
            if(ConnectionIds.TryGetValue(userId,out var connectionId))
            {
                await Clients.Client(connectionId).SendAsync("RecieveSamlLogoutReuqest", samlRequest);
            }
            else
            {
                MasterLogger.Info($"Saml Hub disconnected for user {userId}", "SamlHub", "OnDisconnectedAsync");
            }
        }
    }
}
