using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ninject;
using WebApiDemo;
using WebApiDemo.Controllers;
using WebApiDemo.Models;

namespace WebApiDemo.Tests
{
    [TestClass]
    public class TagsControllerTests
    {
        [TestMethod]
        public void Get_all_should_return_tags_list()
        {
            // arrange
            var tagsRepository = new Mock<ITagRepository>();
            var tagsList = TestUtil.CreateTagsList();
            tagsRepository.Setup(c => c.GetAll()).Returns(tagsList);

            // act
            var controller = new TagsController(tagsRepository.Object);
            var result = controller.GetAll();

            // assert
            result.Count().Should().Be(8);
            result.First().Name.Should().Be("Ball Handling");
            tagsRepository.VerifyAll();
        }

        [TestMethod]
        public void Post_valid_tag_should_return_with_location_header()
        {
            // arrange
            var tagsRepository = new Mock<ITagRepository>();
            var tag = TestUtil.CreateValidTag();
            tagsRepository.Setup(r => r.InsertOrUpdate(tag)).Callback(() => tag.Id = 1);
            var kernel = new StandardKernel();
            kernel.Bind<ITagRepository>().ToMethod(context => tagsRepository.Object);

            var config = new HttpConfiguration();
            WebApiConfig.Configure(config, new NinjectDependencyResolver(kernel));
            var server = new HttpServer(config);
            var client = new HttpClient(server);

            // act
            var response = client.PostAsJsonAsync<Tag>("http://localhost/api/tags", tag).Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            // assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            var postedTag = response.Content.ReadAsAsync<Tag>().Result;
            postedTag.Id.Should().NotBe(0);
            postedTag.Name.Should().NotBeNull();
            Console.WriteLine(response.Headers.Location.AbsoluteUri);
            response.Headers.Location.AbsoluteUri.Should().NotBeNull();
        }
    }
}
