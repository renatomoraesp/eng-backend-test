using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using UserManagementService.DTOs;
using UserManagementService.Models;
using UserManagementService.Repositories;
using UserManagementService.Services;
using Xunit;

namespace UserManagementService.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateAsync_ShouldSetActiveToTrueAndCallRepositoryCreateAsync()
        {
            var user = new User
            {
                name = "John Doe",
                birthdate = new DateTime(1990, 1, 1)
            };

            _userRepositoryMock.Setup(repo => repo.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(user);

            var createdUser = await _userService.CreateAsync(user);

            Assert.True(createdUser.active);
            _userRepositoryMock.Verify(repo => repo.CreateAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateUserAndCallRepositoryUpdateAsync()
        {
            var userId = Guid.NewGuid();
            var user = new User
            {
                id = userId,
                name = "John Doe",
                birthdate = new DateTime(1990, 1, 1),
                active = true
            };

            var userUpdateDto = new UserUpdateDto
            {
                name = "Updated Name",
                birthdate = new DateTime(1995, 5, 5),
                active = false
            };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(user);

            var updatedUser = await _userService.UpdateAsync(userId, userUpdateDto);

            Assert.Equal(userUpdateDto.name, updatedUser.name);
            Assert.Equal(userUpdateDto.birthdate, updatedUser.birthdate);
            Assert.Equal(userUpdateDto.active, updatedUser.active);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(user), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnNull_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userUpdateDto = new UserUpdateDto();

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync((User)null);

            var result = await _userService.UpdateAsync(userId, userUpdateDto);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldCallRepositoryDeleteAsync()
        {
            var userId = Guid.NewGuid();

            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId))
                .ReturnsAsync(true);

            var result = await _userService.DeleteAsync(userId);

            Assert.True(result);
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldCallRepositoryGetAllAsync()
        {
            var users = new List<User>
            {
                new User { id = Guid.NewGuid(), name = "John Doe" },
                new User { id = Guid.NewGuid(), name = "Jane Smith" }
            };

            var pagedResult = new PagedResult<User>
            {
                content = users,
                totalElements = users.Count,
                size = 10,
                totalPages = 1,
                number = 1
            };

            _userRepositoryMock.Setup(repo => repo.GetAllAsync(null, null, null, null, null, null, 1, 10))
                .ReturnsAsync(pagedResult);

            var result = await _userService.GetAllAsync(null, null, null, null, null, null, 1, 10);

            Assert.Equal(users, result.content);
            Assert.Equal(1, result.number);
            Assert.Equal(10, result.size);
            Assert.Equal(1, result.totalPages);
            Assert.Equal(users.Count, result.totalElements);
            _userRepositoryMock.Verify(repo => repo.GetAllAsync(null, null, null, null, null, null, 1, 10), Times.Once);
        }

        [Fact]
        public async Task GetOneAsync_ShouldCallRepositoryGetByIdAsync()
        {
            var userId = Guid.NewGuid();
            var user = new User { id = userId, name = "John Doe" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(user);

            var result = await _userService.GetOneAsync(userId);

            Assert.Equal(user, result);
            _userRepositoryMock.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
        }
    }
}