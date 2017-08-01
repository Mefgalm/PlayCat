namespace PlayCat.DataService.Mappers
{
    public static class AudioMapper
    {
        public static class ToApi
        {
            public static ApiModel.Audio Get(DataModel.Audio audio)
            {
                return audio == null ? null : new ApiModel.Audio
                {
                    AccessUrl = audio.AccessUrl,
                    Artist = audio.Artist,
                    DateCreated = audio.DateCreated,
                    Id = audio.Id,
                    Song = audio.Song,
                    Uploader = UserMapper.ToApi.Get(audio.Uploader),
                };
            }
        }
    }
}
