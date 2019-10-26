using System;
using HangfireExample.Services.Interfaces;

namespace HangfireExample.Services.Implementations
{
    public class VideoService : IVideoService
    {
        public void Play()
        {
            Console.WriteLine("Playing video...");
        }
    }
}
