using Xunit;

namespace D.Hosting
{
    public class DockerfileTests
    {
        /*
        [Fact]
        public void Test()
        {
            var dockerfile = new Dockerfile {
                Name = "debian",
                Tag = "stable"
            };

            dockerfile.Run("apt-get update && apt-get install -y --force-yes apache2");

            dockerfile.Expose(80, 433);

            dockerfile.AddVolumes("/var/www", "/var/log/apache2", "/etc/apache2");

            dockerfile.SetEntrypoint("/usr/sbin/apache2ctl", new[] { "-D", "FOREGROUND" });


            Assert.Equal(
 @"FROM debian:stable
RUN apt-get update && apt-get install -y --force-yes apache2
EXPOSE 80 433
VOLUME [""/var/www"",""/var/log/apache2"",""/etc/apache2""]
ENTRYPOINT [""/usr/sbin/apache2ctl"",""-D"",""FOREGROUND""]", dockerfile.ToString().Trim());
        }
        */

    }
}
