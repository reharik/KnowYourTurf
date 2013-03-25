using System;
using Agitation;
using StoryTeller.Engine;

namespace KnowYourTurf.StoryTeller
{
    public class KnowYourTurfSystem : AgitationSystem
    {
        public KnowYourTurfSystem() : base(new ApplicationSettings
            {
                RootUrl = @"localhost:888\"
            })
        {
        }
    }
}