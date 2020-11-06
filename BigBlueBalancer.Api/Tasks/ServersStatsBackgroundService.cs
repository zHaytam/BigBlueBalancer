using BigBlueBalancer.Api.Entities;
using BigBlueButton.Client;
using BigBlueButton.Client.Models.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BigBlueBalancer.Api.Tasks
{
    public class ServersStatsBackgroundService : BackgroundService
    {
        private const int _intervalInSeconds = 60;
        private readonly IServiceProvider _serviceProvider;

        public ServersStatsBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    await Run(scope);
                }

                await Task.Delay(_intervalInSeconds * 1000, stoppingToken);
            }
        }

        private static async Task Run(IServiceScope scope)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<ServersStatsBackgroundService>>();
            var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var bbbClient = scope.ServiceProvider.GetRequiredService<IBBBClient>();
            var servers = await appDbContext.Servers.ToListAsync();
            logger.LogDebug($"Refreshing stats of {servers.Count} servers.");
            
            foreach (var server in servers)
            {
                var up = true;
                var stats = new ServerStats();

                try
                {
                    var response = await bbbClient.GetMeetings(server.Url, server.Secret);
                    SetStats(stats, response);
                }
                catch (Exception ex) // Maybe be more specific?
                {
                    logger.LogError(ex, $"Exception while fetching meetings for '{server.Url}'.");
                    up = false;
                }

                stats.Up = up;
                server.Up = up;
                server.Load = stats.MeetingsCount * 0.9 + stats.ParticipantCount * 0.1;
                server.Stats.Add(stats);
            }

            await appDbContext.SaveChangesAsync();
        }

        private static void SetStats(ServerStats stats, GetMeetingsResponse response)
        {
            int meetingsCount = 0;
            var participantCount = 0;
            var listenerCount = 0; 
            var voiceParticipantCount = 0;
            var videoCount = 0;

            foreach (var meeting in response.Meetings.Items)
            {
                meetingsCount++;
                participantCount += meeting.ParticipantCount;
                listenerCount += meeting.ListenerCount;
                voiceParticipantCount += meeting.VoiceParticipantCount;
                videoCount += meeting.VideoCount;
            }

            stats.MeetingsCount = meetingsCount;
            stats.ParticipantCount = participantCount;
            stats.ListenerCount = listenerCount;
            stats.VoiceParticipantCount = voiceParticipantCount;
            stats.VideoCount = videoCount;
        }
    }
}
