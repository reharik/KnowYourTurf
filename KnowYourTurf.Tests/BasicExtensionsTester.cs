using AbstractTestProject;
using NUnit.Framework;

namespace KnowYourTurf.Tests
{
    public class BasicExtensionsTester
    {
    }

    [TestFixture]
    public class when_calling_getImageSize
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void should_return_the_url_with_the_size_in_the_right_place()
        {
            var url = @"someshite\someOtherShite\somePicture.jpg";
            Core.BasicExtentions.AddImageSizeToName(url, "thumb").ShouldEqual(@"someshite\someOtherShite\somePicture_thumb.jpg");
        }

    }
}