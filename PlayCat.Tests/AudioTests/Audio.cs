using PlayCat.DataService;
using PlayCat.DataService.Response.AudioRequest;
using PlayCat.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PlayCat.Tests.AudioTests
{
    public class Audio : BaseTest
    {
        [Theory]
        [InlineData(0, 0, 0, 0)]
        [InlineData(0, 0, 10, 0)]
        [InlineData(10, 0, 10, 10)]
        [InlineData(20, 0, 10, 10)]
        [InlineData(5, 0, 10, 5)]
        [InlineData(10, 5, 10, 5)]
        [InlineData(10, -10, 10, 10)]
        [InlineData(10, 0, -10, 10)]
        [InlineData(10, -10, -10, 10)]
        public void ShouldReturnAudios(int count, int skip, int take, int actual)
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);
                    CreateAndAddAudio(context, playlist.Id, count);
                    context.SaveChanges();

                    AudioResult result = audioService.GetAudios(playlist.Id, skip, take);

                    CheckIfSuccess(result);

                    Assert.Equal(actual, result.Audios.Count());
                }
            });
        }

        [Fact]
        public void ShouldEmptyOnWrongId()
        {
            SqlLiteDatabaseTest(options =>
            {
                var audioService = _server.Host.Services.GetService(typeof(IAudioService)) as IAudioService;

                using (var context = new PlayCatDbContext(options))
                {
                    audioService.SetDbContext(context);

                    Guid userId = GetUserId(context);

                    DataModel.Playlist playlist = context.CreatePlaylist(true, userId, null);
                    CreateAndAddAudio(context, playlist.Id, 10);
                    context.SaveChanges();

                    AudioResult result = audioService.GetAudios(Guid.Empty, 0, 10);

                    CheckIfSuccess(result);
                    Assert.Equal(0, result.Audios.Count());
                }
            });
        }

        private Guid GetUserId(PlayCatDbContext context)
        {
            var inviteService = _server.Host.Services.GetService(typeof(IInviteService)) as IInviteService;

            string password = "123456abc";
            string email = "test@gmail.com";

            DataModel.User user = context.CreateUser(email, "test", "test", password, inviteService.GenerateInvite());
            DataModel.AuthToken authToken = context.CreateToken(DateTime.Now.AddDays(-1), false, user.Id);

            context.SaveChanges();

            return user.Id;
        }

        private void CreateAndAddAudio(PlayCatDbContext context, Guid playlistId, int count)
        {
            if (count < -0) return;

            for (int i = 0; i < count; i++)
            {
                DataModel.Audio audio = context.CreateAudio(DateTime.Now.AddMinutes(i), "access" + i, "artist" + i, "song" + i, BaseAudioExtension, i.ToString(), i.ToString(), null);

                DataModel.AudioPlaylist audioPlaylist = context.CreateAudioPlaylist(DateTime.Now.AddMinutes(i), audio.Id, playlistId);
            }
        }
    }
}
