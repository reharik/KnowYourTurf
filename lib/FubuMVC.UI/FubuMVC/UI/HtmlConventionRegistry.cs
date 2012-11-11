// Type: FubuMVC.UI.HtmlConventionRegistry
// Assembly: FubuMVC.UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null

using System;
using System.Collections.Generic;
using FubuMVC.Core.Util;
using FubuMVC.UI.Tags;

namespace FubuMVC.UI
{
    public class HtmlConventionRegistry : TagProfileExpression
    {
        private readonly Cache<string, TagProfile> _profiles =
            new Cache<string, TagProfile>((name => new TagProfile(name)));

        public HtmlConventionRegistry()
            : base(new TagProfile(TagProfile.DEFAULT))
        {
            _profiles[TagProfile.DEFAULT] = profile;
        }

        public IEnumerable<TagProfile> Profiles
        {
            get { return _profiles.GetAll(); }
        }

        public void Profile(string profileName, Action<TagProfileExpression> configure)
        {
            var profileExpression = new TagProfileExpression(_profiles[profileName]);
            configure(profileExpression);
        }
    }
}