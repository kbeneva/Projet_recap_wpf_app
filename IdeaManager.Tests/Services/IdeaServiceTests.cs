using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaManager.Core.Entities;
using IdeaManager.Core.Enums;
using IdeaManager.Core.Interfaces;
using Moq;

namespace IdeaManager.Tests.Services
{
    public class IdeaServiceTests
    {
        private readonly Mock<IRepository<Idea>> _ideaRepoMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly IdeaService _service;

        public IdeaServiceTests()
        {
            _unitOfWorkMock.Setup(u => u.IdeaRepository).Returns(_ideaRepoMock.Object);
            _service = new IdeaService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async Task SubmitIdeaAsync_ShouldAddIdea_WhenValid()
        {
            var idea = new Idea { Title = "Nouvelle idée" };
            await _service.SubmitIdeaAsync(idea);
            _ideaRepoMock.Verify(r => r.AddAsync(It.IsAny<Idea>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RejectIdeaAsync_ShouldSetStatusToRejected_AndSave()
        {
            var idea = new Idea { Id = 1, Title = "Test", Status = IdeaStatus.Pending };
            _ideaRepoMock.Setup(r => r.GetByIdAsync(idea.Id)).ReturnsAsync(idea);

            await _service.RejectIdeaAsync(idea);

            Assert.Equal(IdeaStatus.Rejected, idea.Status);
            Assert.Equal(0, idea.VoteCount);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task ApproveIdeaAsync_ShouldSetStatusToApproved_AndSave()
        {
            var idea = new Idea { Id = 1, Title = "Test", Status = IdeaStatus.Pending };
            _ideaRepoMock.Setup(r => r.GetByIdAsync(idea.Id)).ReturnsAsync(idea);

            await _service.ApproveIdeaAsync(idea);

            Assert.Equal(IdeaStatus.Approved, idea.Status);
            Assert.Equal(1, idea.VoteCount);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }


        [Fact]
        public async Task SubmitIdeaAsync_ShouldThrow_WhenTitleIsEmpty()
        {
            var idea = new Idea { Title = "" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.SubmitIdeaAsync(idea));
        }
    }
}
