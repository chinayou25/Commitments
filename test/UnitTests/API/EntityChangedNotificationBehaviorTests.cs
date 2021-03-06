﻿using Commitments.API.Behaviors;
using Commitments.API.Features.Notes;
using Commitments.API.Features.Tags;
using Commitments.API.Hubs;
using Commitments.Core.Entities;
using Commitments.Infrastructure.Data;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests.API
{    
    public class EntityChangedNotificationBehaviorTests
    {
        [Fact]
        public async Task ShouldSendNotificationAfterSaveNoteCommand()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSendNotificationAfterSaveNoteCommand")
                .Options;

            using (var context = new AppDbContext(options))
            {

                var mockClients = new Mock<IHubClients>();

                var mockGroups = new Mock<IClientProxy>();

                var mockContext = new Mock<IHubContext<AppHub>>();

                mockGroups.Setup(x => x.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>())).Returns(Task.CompletedTask).Verifiable();
                
                mockClients.Setup(x => x.All).Returns(mockGroups.Object);

                mockContext.Setup(x => x.Clients).Returns(mockClients.Object);
                
                var subject =
                new EntityChangedBehavior<SaveNoteCommand.Request, SaveNoteCommand.Response>
                (mockContext.Object, context);
                
                var response = await subject.Handle(new SaveNoteCommand.Request()
                {
                    Note = new NoteApiModel()
                    {
                        NoteId = 1,
                        Title = "Quinntyne"
                    }
                }, default(CancellationToken), () => {

                    context.Notes.Add(new Note()
                    {
                        NoteId = 1,
                        
                    });

                    context.SaveChanges();

                    return Task.FromResult(new SaveNoteCommand.Response()
                    {
                        NoteId = 1
                    });                    
                });

                mockGroups.Verify();

                Assert.NotNull(response);
            }
        }

        [Fact]
        public async Task ShouldSendNotificationAfterRemoveNoteCommand()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSendNotificationAfterRemoveNoteCommand")
                .Options;

            using (var context = new AppDbContext(options))
            {

                var mockClients = new Mock<IHubClients>();

                var mockGroups = new Mock<IClientProxy>();

                var mockContext = new Mock<IHubContext<AppHub>>();

                mockGroups.Setup(x => x.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>())).Returns(Task.CompletedTask).Verifiable();

                mockClients.Setup(x => x.All).Returns(mockGroups.Object);

                mockContext.Setup(x => x.Clients).Returns(mockClients.Object);
                
                var subject =
                new EntityChangedBehavior<RemoveNoteCommand.Request, RemoveNoteCommand.Response>
                (mockContext.Object, context);

                context.Notes.Add(new Note()
                {
                    NoteId = 1,
                    Title = "Angular"
                });

                context.SaveChanges();

                var response = await subject.Handle(new RemoveNoteCommand.Request()
                {
                    NoteId = 1

                }, default(CancellationToken), () => {

                    context.Notes.Remove(context.Notes.Find(1));

                    context.SaveChanges();

                    return Task.FromResult(new RemoveNoteCommand.Response() { });
                });

                mockGroups.Verify();

                Assert.NotNull(response);
            }

        }

        [Fact]
        public async Task ShouldSendNotificationAfterSaveTagCommand()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSendNotificationAfterSaveTagCommand")
                .Options;

            using (var context = new AppDbContext(options))
            {

                var mockClients = new Mock<IHubClients>();

                var mockGroups = new Mock<IClientProxy>();

                var mockContext = new Mock<IHubContext<AppHub>>();

                mockGroups.Setup(x => x.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>())).Returns(Task.CompletedTask).Verifiable();

                mockClients.Setup(x => x.All).Returns(mockGroups.Object);

                mockContext.Setup(x => x.Clients).Returns(mockClients.Object);

                

                var subject =
                new EntityChangedBehavior<SaveTagCommand.Request, SaveTagCommand.Response>
                (mockContext.Object, context);

                var response = await subject.Handle(new SaveTagCommand.Request()
                {
                    Tag = new TagApiModel()
                    {
                        TagId = 1,
                        Name = "Angular"
                    }
                }, default(CancellationToken), () => {

                    context.Tags.Add(new Tag()
                    {
                        TagId = 1,
                        
                    });

                    context.SaveChanges();

                    return Task.FromResult(new SaveTagCommand.Response()
                    {
                        TagId = 1
                    });
                });

                mockGroups.Verify();

                Assert.NotNull(response);
            }
        }

        [Fact]
        public async Task ShouldSendNotificationAfterRemoveTagCommand()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "ShouldSendNotificationAfterRemoveTagCommand")
                .Options;

            using (var context = new AppDbContext(options))
            {

                var mockClients = new Mock<IHubClients>();

                var mockGroups = new Mock<IClientProxy>();

                var mockContext = new Mock<IHubContext<AppHub>>();

                mockGroups.Setup(x => x.SendCoreAsync(It.IsAny<string>(), It.IsAny<object[]>())).Returns(Task.CompletedTask).Verifiable();

                mockClients.Setup(x => x.All).Returns(mockGroups.Object);

                mockContext.Setup(x => x.Clients).Returns(mockClients.Object);
                
                var subject =
                new EntityChangedBehavior<RemoveTagCommand.Request, RemoveTagCommand.Response>
                (mockContext.Object, context);

                context.Tags.Add(new Tag()
                {
                    TagId = 1,
                    Name = "Angular"
                });

                context.SaveChanges();

                var response = await subject.Handle(new RemoveTagCommand.Request()
                {
                    TagId = 1

                }, default(CancellationToken), () => {

                    context.Tags.Remove(context.Tags.Find(1));

                    context.SaveChanges();

                    return Task.FromResult(new RemoveTagCommand.Response() { });
                });

                mockGroups.Verify();

                Assert.NotNull(response);
            }

        }
    }
}
